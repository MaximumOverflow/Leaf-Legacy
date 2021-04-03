using Leaf.Compilation.Types.Attributes;
using System.Runtime.CompilerServices;
using Leaf.Compilation.Exceptions;
using LLVMSharp.Interop;
using System;

namespace Leaf.Compilation.Types
{
	public class ReferenceType : Type
	{
		public readonly Type Base;
		public override AttributeTarget AttributeTargetType => AttributeTarget.PointerType;

		private ReferenceType(string name, Type @base, LLVMTypeRef llvmType, TypeFlags flags = default)
			: base(name, @base.Namespace, llvmType, flags & ~TypeFlags.Primitive) => Base = @base;

		protected override LLVMTypeRef Compile()
			=> LlvmTypeRef!.Value;

		public static ReferenceType Create(Type @base)
		{
			var name = $"ref {@base}";

			if (@base.Namespace!.Types.TryGetValue(name, out var type))
				return (ReferenceType) type;

			Span<LLVMTypeRef> members = stackalloc LLVMTypeRef[]
			{
				LLVMTypeRef.CreatePointer(@base.LlvmType, 0),
				LLVMTypeRef.CreatePointer(@base.Namespace.Context.AllocatorVTableType.LlvmType, 0),
			};

			if (type is ReferenceType or LightReferenceType or FunctionType)
				throw new CompilationException($"Type {@base} cannot be referenced.", null);

			var llvmType = @base.LlvmType.Context.CreateNamedStruct(GetMangledName(@base));
			llvmType.StructSetBody(members, false);

			var refT = new ReferenceType(name, @base, llvmType, @base.Flags);
			@base.Namespace!.Types.Add(name, refT);
			return refT;
		}

		public override string GetMangledName()
			=> GetMangledName(Base);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static string GetMangledName(Type @base)
			=> $"ref_{@base.GetMangledName()}";

		public override string ToString() => $"ref {Base}";

		public override LLVMValueRef DefaultInitializer 
			=> throw new InvalidTypeException(this, null);

		public LightReferenceType AsLightRef()
			=> LightReferenceType.Create(Base);

		public override bool Equals(Type? other)
			=> base.Equals(other) || other is LightReferenceType lrefT && lrefT.Base.Equals(Base);
	}
}