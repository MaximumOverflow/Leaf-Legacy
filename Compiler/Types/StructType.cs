using Leaf.Compilation.Types.Attributes;
using Leaf.Compilation.CompilationUnits;
using System.Collections.Immutable;
using Leaf.Compilation.Exceptions;
using System.Collections.Generic;
using Leaf.Compilation.Grammar;
using LLVMSharp.Interop;
using System;

namespace Leaf.Compilation.Types
{
    public class StructType : Type
	{
		public readonly IReadOnlyDictionary<string, TypeMember> Members;
		public override AttributeTarget AttributeTargetType => AttributeTarget.Struct;

		public StructType(string name, Namespace ns, LLVMTypeRef? llvmType = null, 
			IReadOnlyDictionary<string, TypeMember>? members = null, TypeFlags flags = default)
			: base(name, ns, llvmType, flags) => Members = members ?? ImmutableDictionary<string, TypeMember>.Empty;
		
		public StructType(string name, Fragment ns,LeafParser.Attribute_addContext[]? attributes, LLVMTypeRef? llvmType = null, 
			IReadOnlyDictionary<string, TypeMember>? members = null, TypeFlags flags = default)
			: base(name, ns, attributes, llvmType, flags) => Members = members ?? ImmutableDictionary<string, TypeMember>.Empty;

		public StructType(string name, Fragment frag, LeafParser.StructContext def, LeafParser.Attribute_addContext[] attribs) 
		: base(name, frag, attribs)
		{
			Members = GetMembers(frag, def.type_member());
		}

		protected override LLVMTypeRef Compile()
        {
            var i = 0;
			var name = (Flags & TypeFlags.CCompat) == 0
				? GetMangledName()
				: Name;
			
            LlvmTypeRef = Namespace!.Module.Context.LlvmContext.CreateNamedStruct(name);
            Span<LLVMTypeRef> types = stackalloc LLVMTypeRef[Members.Count];
            foreach (var (_, member) in Members) types[i++] = member.Type.LlvmType;
            LlvmTypeRef.Value.StructSetBody(types, false);
            return LlvmTypeRef.Value;
        }

		public static IReadOnlyDictionary<string, TypeMember> GetMembers(Fragment fragment, Span<LeafParser.Type_memberContext> defs)
		{
			var members = new Dictionary<string, TypeMember>();
			foreach (var def in defs)
			{
				var name = def.Id().GetText();
				var type = fragment.GetType(def.type());
				var member = new TypeMember((uint) members.Count, name, type);

				if (!members.TryAdd(name, member))
					throw new DuplicateSymbolException(name, fragment, def.Start.Line);
			}

			return members;
		}

		public override string GetMangledName() => (Flags & TypeFlags.Primitive) == 0
			? $"struct_{Namespace.GetMangledName()}_{Name}"
			: Name;

		public override LLVMValueRef DefaultInitializer 
			=> LLVMValueRef.CreateConstNamedStruct(LlvmType, ReadOnlySpan<LLVMValueRef>.Empty);
	}
}