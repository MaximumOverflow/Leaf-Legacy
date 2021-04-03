using Leaf.Compilation.Exceptions;
using Leaf.Compilation.Grammar;
using Leaf.Compilation.Values;
using Leaf.Compilation.Types;
using LLVMSharp.Interop;
using System;
using System.Runtime.CompilerServices;
using DotNetCoreUtilities.CodeGeneration;
using Leaf.Compilation.Functions;

namespace Leaf.Compilation.Statements
{
	public static partial class Statement
	{
		public static Value Compile(this LeafParser.Function_callContext c, in LocalCompilationContext ctx)
		{
			var values = c.value();

			try
			{
				var args = new Value[values.Length - 1];
				for (var i = 0; i < args.Length; i++)
					args[i] = Value.Get(values[i + 1], in ctx);

				var options = new ValueRetrievalOptions {Parameters = args};
				var function = Value.Get(values[0], in ctx, in options, out var parent);

				if (parent.HasValue)
				{
					var argsParent = new Value[values.Length]; 
					
					argsParent[0] = Value.CreateReference(parent.Value, in ctx,
						((FunctionType) function.Type).ParamTypes[0] is LightReferenceType);
					
					for (var i = 1; i < argsParent.Length; i++) argsParent[i] = args[i - 1];
					return CompileCall(function, argsParent, in ctx);
				}
				
				return CompileCall(function, args, in ctx);
			}
			catch (CompilationException e)
			{
				throw new CompilationException($"Could not compile call to '{values[0].GetText()}'.", 
					ctx.CurrentFragment, values[0].Start.Line, e);
			}
		}

		public static Value CompileCall(Value value, Span<Value> args, in LocalCompilationContext ctx)
		{
			var builder = ctx.Builder;
			if (value.Type is not FunctionType funcT)
				throw new InvalidTypeException(value.Type, ctx.CurrentFragment);
			
			if (!OverloadGroup.CompareTypes(funcT.ParamTypes, args, funcT.LlvmType.IsFunctionVarArg))
				throw new CompilerBugException("Invalid parameters supplied to function call.");

			Span<LLVMValueRef> argValues = stackalloc LLVMValueRef[args.Length];
			for (var i = 0; i < args.Length; i++)
			{
				if (i >= funcT.ParamTypes.Length)
				{
					argValues[i] = args[i].AsRValue(in ctx).LlvmValue;
					continue;
				}

				argValues[i] = args[i].CastTo(funcT.ParamTypes[i], in ctx).LlvmValue;
			}

			var ret = value.Call(argValues, in ctx);
			
			var flags = ValueFlags.None;
			if (funcT.ReturnType is ReferenceType) flags |= ValueFlags.LValue;
			if (funcT.ReturnType is PointerType {Base: FunctionType}) flags |= ValueFlags.Callable;
			
			return new Value
			{
				Flags = flags,
				Type = funcT.ReturnType,
				LlvmValue = ret.LlvmValue,
				Allocator = ret.Allocator,
			};
		}
	}
}