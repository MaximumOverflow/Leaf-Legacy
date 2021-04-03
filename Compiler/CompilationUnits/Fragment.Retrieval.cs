using Type = Leaf.Compilation.Types.Type;
using Leaf.Compilation.Reflection.Static;
using Leaf.Compilation.Types.Attributes;
using Leaf.Compilation.Exceptions;
using Leaf.Compilation.Functions;
using Leaf.Compilation.Grammar;
using Leaf.Compilation.Values;
using Leaf.Compilation.Types;
using System;

namespace Leaf.Compilation.CompilationUnits
{
	public partial class Fragment
	{
		public Type GetType(LeafParser.TypeContext? t, in LocalCompilationContext? ctx = null)
		{
			if (t == null) return Module.Context.GlobalNamespace.Types["void"];

			var ns = Namespace;
			var name = t.name?.Text;
			if (name != null)
			{
				if (t.ns != null && !NamespaceAliases.TryGetValue(t.ns.Text, out ns))
					throw new CompilationException($"Namespace alias '{t.ns.Text}' is not defined.", null);

				if (ns == Namespace && Module.Context.GlobalNamespace.Types.TryGetValue(name, out var type))
					return type;

				while (ns != null)
				{
					if (ns.Types.TryGetValue(name, out type)) 
						return type;
					
					ns = ns.Parent;
				}
				
				foreach (var imported in ImportedNamespaces)
					if (imported.Types.TryGetValue(name, out type))
						return type;

				throw new TypeNotFoundException(name, this, t.Start.Line);
			}
			
			if(t.ptr != null)
				return PointerType.Create(GetType(t.type(0)));
			
			if(t.Ref() != null) 
				return ReferenceType.Create(GetType(t.type(0)));

			if (t.StaticAccessor() != null)
				return TypeInfo.QueryType(GetType(t.type(0)), t.Id(0).GetText(), t.value(), in ctx);

			if (t.TypeOf() != null)
			{
				if (!ctx.HasValue) throw new CompilerBugException();
				return Value.Get(t.value(0), ctx.Value, new ValueRetrievalOptions {Flags = ValueRetrievalFlags.GetTypeOnly}).Type;
			}

			throw new NotImplementedException();
		}

		public AttributeType GetAttribute(LeafParser.Attribute_addContext a)
		{
			var ns = Namespace;
			var name = a.name.Text;
			
			if (a.ns != null && !NamespaceAliases.TryGetValue(a.ns.Text, out ns))
				throw new CompilationException($"Namespace alias '{a.ns.Text}' is not defined.", null);
			
			if (ns == Namespace && Module.Context.GlobalNamespace.Attributes.TryGetValue(name, out var attr))
				return attr;

			while (ns != null)
			{
				if (ns.Attributes.TryGetValue(name, out attr)) 
					return attr;
					
				ns = ns.Parent;
			}

			throw new TypeNotFoundException(name, this, a.Start.Line);
		}

		public bool TryGetOverloadGroup(string name, out OverloadGroup? overloads)
		{
			if (Namespace.Functions.TryGetValue(name, out overloads))
				return true;

			foreach (var ns in ImportedNamespaces)
				if (ns.Functions.TryGetValue(name, out overloads))
					return true;

			return false;
		}
	}
}