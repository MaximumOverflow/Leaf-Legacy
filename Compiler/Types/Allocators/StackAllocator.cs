using Leaf.Compilation.Exceptions;
using Leaf.Compilation.Values;
using LLVMSharp.Interop;
using System;
using Leaf.Compilation.Types.Attributes;

namespace Leaf.Compilation.Types.Allocators
{
	public sealed class StackAllocator : Allocator
	{
		public StackAllocator(GlobalCompilationContext ctx) 
			: base("Stack", ctx, ctx.GlobalNamespace) {}

		public override Value Allocate(Type type, bool mutable, in LocalCompilationContext ctx, string name = "")
		{
			LLVMValueRef llvmValue;
			
			switch (type)
			{
				case ReferenceType or FunctionType or AttributeType:
					throw new TypeInstantiationException(type, ctx.CurrentFragment);
				
				case StructType or PointerType:
					llvmValue = ctx.Builder.BuildAlloca(type, (ReadOnlySpan<char>) name);
					break;
				
				default: throw new NotImplementedException();
			}

			return new Value
			{
				Type = type,
				LlvmValue = llvmValue,
				Flags = ValueFlags.LValue | (mutable ? ValueFlags.Mutable : ValueFlags.None)
			};
		}

		public override void Free(Span<Value> args, in LocalCompilationContext ctx)
			=> throw new CompilationException("Stack allocated values cannot be freed.", ctx.CurrentFragment);
	}
}