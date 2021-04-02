using Leaf.Compilation.Types.Attributes;
using Leaf.Compilation.CompilationUnits;
using System.Collections.Immutable;
using System.Collections.Generic;
using Leaf.Compilation.Values;
using LLVMSharp.Interop;
using System;
using Leaf.Compilation.Exceptions;
using Leaf.Compilation.Functions;
using Leaf.Compilation.Grammar;

namespace Leaf.Compilation.Types
{
	[Flags]
	public enum TypeFlags
	{
		None = 0,
		Unsafe		= Flags.Unsafe,
		CCompat		= Flags.CCompat,
		Generic		= Flags.Generic,
		Unsigned	= Flags.Unsigned,
		Primitive	= Flags.Primitive,
	}
	
    public abstract class Type : IAttributeTarget, IEquatable<Type>
    {
        public readonly string Name;

		protected LLVMTypeRef? LlvmTypeRef;
		public readonly Fragment? Fragment;
		public readonly Namespace Namespace;
		public TypeFlags Flags { get; protected init; }
		public abstract AttributeTarget AttributeTargetType { get; }
		public IReadOnlyDictionary<AttributeType, Value> Attributes { get; protected init; }

		private Dictionary<string, OverloadGroup> _methods;
		public IReadOnlyDictionary<string, OverloadGroup> Methods => _methods;

		private Dictionary<Type, TypeConversion> _conversions;
		public IReadOnlyDictionary<Type, TypeConversion> Conversions => _conversions;

		public abstract LLVMValueRef DefaultInitializer { get; }


		protected Type(string name, Fragment frag, LeafParser.Attribute_addContext[]? attribs, LLVMTypeRef? llvmType = null, TypeFlags flags = default)
		{
			Namespace = frag.Namespace;
			Attributes = attribs != null
				? AttributeType.GetAttributes(this, frag, attribs)
				: ImmutableDictionary<AttributeType, Value>.Empty;

			_methods = new Dictionary<string, OverloadGroup>();
			_conversions = new Dictionary<Type, TypeConversion>();
			(Name, Fragment, LlvmTypeRef, Flags) = (name, frag, llvmType, flags);

			foreach (var attrib in Attributes.Keys)
				Flags |= (TypeFlags) attrib.SubsequentFlags;
		}
		
		protected Type(string name, Namespace ns, LLVMTypeRef? llvmType = null, TypeFlags flags = default)
		{
			_methods = new Dictionary<string, OverloadGroup>();
			_conversions = new Dictionary<Type, TypeConversion>();
			Attributes = ImmutableDictionary<AttributeType, Value>.Empty;
			(Name, Namespace, LlvmTypeRef, Flags) = (name, ns, llvmType, flags);
		}


		public LLVMTypeRef LlvmType
        {
            get
            {
                if (LlvmTypeRef.HasValue)
                    return LlvmTypeRef.Value;

                LlvmTypeRef = Compile();
                return LlvmTypeRef.Value;
            }
        }

		protected abstract LLVMTypeRef Compile();

		public void AddMethod(Function func)
		{
			if (!_methods.TryGetValue(func.Name, out var overloads))
			{
				overloads = new OverloadGroup(func.Name, Namespace);
				_methods.Add(func.Name, overloads);
			}
			
			overloads.AddImplementation(func);
		}
		

		public virtual string GetMangledName() => Namespace != Namespace.Context.GlobalNamespace
			? $"type_{Namespace.GetMangledName()}_{Name}"
			: Name;

		public override string ToString() => Namespace != Namespace.Context.GlobalNamespace
			? $"{Namespace.FullName}.{Name}"
			: Name;

		public static implicit operator LLVMTypeRef(in Type type) => type.LlvmType;

		public static Value SizeOf(Type type, GlobalCompilationContext ctx) => new Value
		{
			LlvmValue = type.LlvmType.SizeOf,
			Type = ctx.GlobalNamespace.Types["i64"]
		};

		public bool Equals(Type? other)
			=> ReferenceEquals(this, other);

		public override bool Equals(object? other)
			=> ReferenceEquals(this, other);
	}
}