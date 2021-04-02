using Value = Leaf.Compilation.Values.Value;
using Type = Leaf.Compilation.Types.Type;
using LLVMSharp.Interop;
using System;

namespace Leaf.Compilation.Functions
{
	public interface ICallable
	{
		public Type ReturnType { get; }
		public LLVMValueRef LlvmValue { get; }

		public static Value Call<T>(T value, ReadOnlySpan<LLVMValueRef> args, in LocalCompilationContext ctx) where T : ICallable
		{
			var result = ctx.Builder.BuildCall(value.LlvmValue, args, "");
			return new Value {Type = value.ReturnType, LlvmValue = result};
		}
	}
}