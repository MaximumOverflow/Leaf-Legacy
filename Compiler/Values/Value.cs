using Type = Leaf.Compilation.Types.Type;
using Leaf.Compilation.Exceptions;
using Leaf.Compilation.Functions;
using Leaf.Compilation.Types;
using LLVMSharp.Interop;
using System;

namespace Leaf.Compilation.Values
{
	[Flags]
	public enum ValueFlags : byte
	{
		None = 0,
		LValue		= 0b00000001,
		Mutable		= 0b00000010,
		Callable	= 0b00000100,
		Constant	= 0b00001000,
		Alias		= 0b00010000,
		Parameter	= 0b00100000,
	}
	
	public readonly partial struct Value : ICallable, IEquatable<Value>
	{
		public Type Type { get; init; }
		public ValueFlags Flags { get; init; }
		public LLVMValueRef? Allocator { get; init; }
		public LLVMValueRef LlvmValue { get; init; }
		public Type ReturnType => (Type as FunctionType)?.ReturnType!;

		public void Deconstruct(out Type type, out LLVMValueRef llvmValue)
			=> (type, llvmValue) = (Type, LlvmValue);

		public Value Call(ReadOnlySpan<LLVMValueRef> args, in LocalCompilationContext ctx)
			=> ICallable.Call(this, args, in ctx);

		public Value AsRValue(in LocalCompilationContext ctx)
		{
			var builder = ctx.Builder;
			if ((Flags & ValueFlags.LValue) == 0)
				return this;
			
			if (Type is ReferenceType refT) return new Value
			{
				Type = refT.Base,
				Allocator = Allocator,
				Flags = Flags & ~ValueFlags.Alias,
				LlvmValue = builder.BuildLoad(builder.BuildLoad(builder.BuildStructGEP(LlvmValue, 0)))
			};

			return new Value
			{
				Type = Type,
				Flags = ValueFlags.None,
				LlvmValue = builder.BuildLoad(LlvmValue),
			};
		}
		
		public Value AsPlainLValue(in LocalCompilationContext ctx)
		{
			if (Type is ReferenceType refT) return new Value
			{
				Type = refT.Base,
				Allocator = Allocator,
				Flags = Flags & ~ValueFlags.Alias,
				LlvmValue = ctx.Builder.BuildLoad(ctx.Builder.BuildStructGEP(LlvmValue, 0))
			};
			
			if (Type is LightReferenceType lrefT) return new Value
			{
				Type = lrefT.Base,
				Allocator = Allocator,
				Flags = Flags & ~ValueFlags.Alias,
				LlvmValue = ctx.Builder.BuildLoad(LlvmValue)
			};

			if ((Flags & ValueFlags.LValue) == 0)
				throw new CompilationException("Value is not an LValue.", ctx.CurrentFragment);

			return this;
		}
		
		public bool Equals(Value other)
			=> LlvmValue == other.LlvmValue;
	}
}