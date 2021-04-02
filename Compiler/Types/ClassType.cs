using System;
using Leaf.Compilation.CompilationUnits;
using System.Collections.Generic;
using System.Collections.Immutable;
using Leaf.Compilation.Functions;
using Leaf.Compilation.Grammar;
using Leaf.Compilation.Types.Attributes;
using Leaf.Compilation.Values;
using LLVMSharp.Interop;

namespace Leaf.Compilation.Types
{
	public class ClassType : Type
	{
		public readonly IReadOnlyDictionary<string, TypeMember> Members;
		public readonly IReadOnlyDictionary<string, OverloadGroup> Methods;
		public override AttributeTarget AttributeTargetType => AttributeTarget.Class;

		public ClassType(string name, Fragment frag, LeafParser.Attribute_addContext[]? attributes, LLVMTypeRef? llvmType = null, TypeFlags flags = default)
			: base(name, frag, attributes, llvmType, flags) {}

		public ClassType(string name, Namespace ns, IReadOnlyDictionary<AttributeType, Value>? attributes, LLVMTypeRef? llvmType = null, TypeFlags flags = default) 
			: base(name, ns, llvmType, flags) => Attributes = attributes ?? ImmutableDictionary<AttributeType, Value>.Empty;

		public bool CheckImplementation(string name, Type[] paramTypes)
			=> Methods.TryGetValue(name, out var overloads) 
			&& overloads.ImplementsOverload(paramTypes);

		protected override LLVMTypeRef Compile()
		{
			throw new System.NotImplementedException();
		}
		
		public override LLVMValueRef DefaultInitializer 
			=> LLVMValueRef.CreateConstNamedStruct(LlvmType, ReadOnlySpan<LLVMValueRef>.Empty);
	}
}