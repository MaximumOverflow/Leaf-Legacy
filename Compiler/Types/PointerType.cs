using Leaf.Compilation.Exceptions;
using Leaf.Compilation.Types.Attributes;
using LLVMSharp.Interop;

namespace Leaf.Compilation.Types
{
	public class PointerType : Type
	{
		public readonly Type Base;
		public override AttributeTarget AttributeTargetType => AttributeTarget.PointerType;

		public PointerType(string name, Type @base, LLVMTypeRef llvmType, TypeFlags flags = default)
			: base(name, @base.Namespace, llvmType, flags & ~TypeFlags.Primitive) => Base = @base;

		protected override LLVMTypeRef Compile()
			=> LlvmTypeRef!.Value;

		public static PointerType Create(Type @base)
		{
			var name = @base.Name + '*';

			if (@base.Namespace!.Types.TryGetValue(name, out var type))
				return (PointerType) type;

			var llvmType = @base switch
			{
				ReferenceType
					=> throw new CompilationException($"Cannot create a pointer to type \"{@base}\".", null),

				_ => LLVMTypeRef.CreatePointer(@base.LlvmType, 0)
			};

			var ptrT = new PointerType(name, @base, llvmType, @base.Flags);
			@base.Namespace!.Types.Add(name, ptrT);
			return ptrT;
		}
		
		public override string GetMangledName()
			=> $"ptr_{Base}";

		public override LLVMValueRef DefaultInitializer	
			=> LLVMValueRef.CreateConstPointerNull(this);
	}
}