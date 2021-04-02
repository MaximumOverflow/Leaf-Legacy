using Leaf.Compilation.Types.Allocators;
using Leaf.Compilation.Exceptions;
using Leaf.Compilation.Grammar;
using Leaf.Compilation.Values;
using System;
using LLVMSharp.Interop;

namespace Leaf.Compilation.Statements
{
	public static partial class Statement
	{
		public static Value Compile(this LeafParser.Var_defContext d, in LocalCompilationContext ctx)
			=> d.var_def_t()?.Compile(in ctx) ?? d.var_def_v()!.Compile(in ctx);

		public static Value Compile(this LeafParser.Var_def_tContext d, in LocalCompilationContext ctx)
		{
			var builder = ctx.Builder;
			var name = d.Id().GetText();
			var type = ctx.CurrentFragment.GetType(d.t);
			
			var allocator = d.alloc != null
				? ctx.CurrentFragment.GetType(d.alloc) as Allocator
				: ctx.GlobalContext.StackAllocator;
			
			if (allocator == null)
				throw new InvalidTypeException(ctx.CurrentFragment.GetType(d.alloc), ctx.CurrentFragment, d.alloc!.Start.Line);
			
			var variable = allocator.Allocate(type, d.Var() != null, in ctx, name);
			builder.BuildStore(type.DefaultInitializer, variable.LlvmValue);

			if (!ctx.CurrentScope.Variables.TryAdd(name, variable))
				throw new DuplicateSymbolException(name, ctx.CurrentFragment, d.Start.Line);
			
			return variable;
		}

		public static Value Compile(this LeafParser.Var_def_vContext d, in LocalCompilationContext ctx)
		{
			var builder = ctx.Builder;
			var name = d.Id().GetText();
			var type = d.t != null 
				? ctx.CurrentFragment.GetType(d.t) 
				: null;
			
			Value variable;
			var value = Value.Get(d.value(), in ctx, new ValueRetrievalOptions{ExpectedType = type, Flags = ValueRetrievalFlags.AllowRefAliasing});
			
			type ??= value.Type;
			if (value.Type != type)
				throw new NotImplementedException();
			
			if (d.Ref() != null)
			{
				var flags = value.Flags;
				if (d.Var() != null) flags |= ValueFlags.Mutable;

				if ((flags & ValueFlags.LValue) == 0)
					throw new CompilationException("Cannot create a mutable reference to an immutable value. Consider using the 'ref' keyword.", ctx.CurrentFragment, d.Start.Line);
				
				if ((flags & ValueFlags.Mutable) != 0 && (value.Flags & ValueFlags.Mutable) == 0)
					throw new CompilationException("Cannot create a mutable reference to an immutable value.", ctx.CurrentFragment, d.Start.Line);

				variable = new Value
				{
					Flags = flags,
					Type = value.Type,
					LlvmValue = value.LlvmValue,
				};
			}
			else
			{
				var allocator = d.alloc != null
					? ctx.CurrentFragment.GetType(d.alloc) as Allocator
					: ctx.GlobalContext.StackAllocator;

				if (allocator == null)
					throw new InvalidTypeException(ctx.CurrentFragment.GetType(d.alloc), ctx.CurrentFragment, d.alloc!.Start.Line);

				//TODO Implement constant folding for stack based values
				variable = allocator.Allocate(type, d.Var() != null, in ctx, name);

				builder.BuildStore(value.AsRValue(in ctx).LlvmValue, variable.LlvmValue);
			}
			
			if (!ctx.CurrentScope.Variables.TryAdd(name, variable))
				throw new DuplicateSymbolException(name, ctx.CurrentFragment, d.Start.Line);

			return variable;
		}

		public static unsafe Value Compile(this LeafParser.FreeContext f, in LocalCompilationContext ctx)
		{
			var builder = ctx.Builder;
			var value = Value.Get(f.value(), ctx);
			
			if (!value.Allocator.HasValue)
				throw new CompilationException("Stack allocated values cannot be freed.", ctx.CurrentFragment, f.Start.Line);

			var vTable = builder.BuildLoad(value.Allocator.Value);
			var fn = builder.BuildLoad(builder.BuildStructGEP(vTable, 1, "free_fn"));
			builder.BuildCall(fn, stackalloc LLVMValueRef[] {value.AsPlainLValue(in ctx).LlvmValue}, "");

			return new Value {Type = ctx.GlobalContext.GlobalNamespace.Types["void"]};
		}
	}
}