using System.Collections.Generic;
using Leaf.Compilation.Grammar;
using Antlr4.Runtime;
using Extensions;
using System.IO;

namespace Leaf.Compilation.CompilationUnits
{
	public partial class Fragment
	{
		public readonly FileInfo File;
		public readonly Namespace Namespace;
		public readonly List<Namespace> ImportedNamespaces;
		public readonly Dictionary<string, Namespace> NamespaceAliases;

		public Module Module => Namespace.Module;

		public Fragment(FileInfo file, Namespace ns)
		{
			File = file;
			Namespace = ns;
			ImportedNamespaces = new List<Namespace>();
			NamespaceAliases = new Dictionary<string, Namespace>();
		}

		public void Compile()
		{
			var stream = new AntlrFileStream(File.FullName);
			var lexer = new LeafLexer(stream);
			var tokens = new CommonTokenStream(lexer);
			var parser = new LeafParser(tokens);

			var entryPoint = parser.entry_point();
			var imports = entryPoint.ns_import();
			var defs = entryPoint.def();

			if (!imports.IsNullOrEmpty())
			{
				foreach (var import in imports)
				{
					var alias = import.alias?.Text;
					var nsName = import.@namespace().GetText();
					var ns = Namespace.Context.GetNamespace(nsName);
					
					if(alias == null)
						ImportedNamespaces.Add(ns);
					else NamespaceAliases.Add(alias, ns);
				}
			}

			var typeDefs = new List<(LeafParser.Def_typeContext, LeafParser.Attribute_addContext[])>(defs.Length);
			var funcDefs = new List<(LeafParser.Def_funcContext, LeafParser.Attribute_addContext[])>(defs.Length);
			var operDefs = new List<(LeafParser.Def_operatorContext, LeafParser.Attribute_addContext[])>(defs.Length);
			
			foreach (var def in defs)
			{
				var tDef = def.def_type();
				var fDef = def.def_func();
				var oDef = def.def_operator();
				if(tDef != null) typeDefs.Add((tDef, def.attribute_add()));
				if(fDef != null) funcDefs.Add((fDef, def.attribute_add()));
				if(oDef != null) operDefs.Add((oDef, def.attribute_add()));
			}
			
			foreach (var (def, attribs) in typeDefs)
				DefineType(def, attribs);
			
			foreach (var (def, attribs) in funcDefs)
				DefineFunction(def, attribs);
			
			foreach (var (def, attribs) in operDefs)
				DefineOperator(def, attribs);
		}
	}
}