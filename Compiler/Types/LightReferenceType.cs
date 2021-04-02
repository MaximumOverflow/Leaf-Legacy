using Leaf.Compilation.Types.Attributes;
using System.Runtime.CompilerServices;
using Leaf.Compilation.Exceptions;
using LLVMSharp.Interop;
using System;

namespace Leaf.Compilation.Types
{
	public class LightReferenceType : Type
	{
		public readonly Type Base;
		public override AttributeTarget AttributeTargetType => AttributeTarget.PointerType;

		private LightReferenceType(string name, Type @base, LLVMTypeRef llvmType, TypeFlags flags = default)
			: base(name, @base.Namespace, llvmType, flags & ~TypeFlags.Primitive) => Base = @base;

		protected override LLVMTypeRef Compile()
			=> LlvmTypeRef!.Value;

		public static LightReferenceType Create(Type @base)
		{
			var name = $"lref {@base}";

			if (@base.Namespace!.Types.TryGetValue(name, out var type))
				return (LightReferenceType) type;

			if (type is ReferenceType or LightReferenceType or FunctionType)
				throw new CompilationException($"Type {@base} cannot be referenced.", null);

			var llvmType = LLVMTypeRef.CreatePointer(@base, 0);

			var refT = new LightReferenceType(name, @base, llvmType, @base.Flags);
			@base.Namespace!.Types.Add(name, refT);
			return refT;
		}

		public override string GetMangledName()
			=> GetMangledName(Base);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static string GetMangledName(Type @base)
			=> $"ref_{@base}";

		public override LLVMValueRef DefaultInitializer 
			=> throw new InvalidTypeException(this, null);
	}
}