using System;
using System.Collections.Generic;
using Leaf.Compilation.CompilationUnits;
using Leaf.Compilation.Exceptions;
using Leaf.Compilation.Grammar;
using Leaf.Compilation.Values;
using LLVMSharp.Interop;

namespace Leaf.Compilation.Types.Attributes
{
	[Flags]
	public enum AttributeTarget : ushort
	{
		None			= 0b00000000000, 
		
		Method			= 0b00000000011,
		StaticMethod	= 0b00000000001,
		InstanceMethod	= 0b00000000010,
		
		Property		= 0b00000000100,
		MemberVariable	= 0b00000001000,
		
		Type			= 0b11111110000,
		Class			= 0b00000010000,
		Struct			= 0b00000100000,
		Attribute		= 0b00001000000,
		Interface		= 0b00010000000,
		Allocator		= 0b00100000000,
		PointerType		= 0b01000000000,
		FunctionType	= 0b10000000000,
	}

	public interface IAttributeTarget
	{
		public AttributeTarget AttributeTargetType { get; }
	}
	
    public class AttributeType : StructType
	{
		public readonly Flags SubsequentFlags;

		public AttributeType(string name, Fragment frag, LeafParser.Attribute_addContext[]? attributes, Flags subsequentFlags)
			: base(name, frag, attributes) => SubsequentFlags = subsequentFlags;
		
		protected AttributeType(string name, Namespace ns, Flags subsequentFlags, LLVMTypeRef? llvmType = null)
			: base(name, ns, llvmType) => SubsequentFlags = subsequentFlags;

		public static IReadOnlyDictionary<AttributeType, Value> GetAttributes(IAttributeTarget target, Fragment frag, Span<LeafParser.Attribute_addContext> defs)
		{
			var attribs = new Dictionary<AttributeType, Value>(defs.Length);
			foreach (var def in defs)
			{
				var type = frag.GetAttribute(def);
				var value = type.Members.Count == 0 
					? frag.Module.CreateAttributeSingleton(type)
					: throw new NotImplementedException();

				if (!attribs.TryAdd(type, value))
					throw new CompilationException($"'{target}' already defines attribute '{type}'.", frag, def.Start.Line);
			}

			return attribs;
		}
		
		public override string GetMangledName() => (Flags & TypeFlags.Primitive) == 0
			? $"attrib_{Namespace.GetMangledName()}_{Name}"
			: Name;
	}
}