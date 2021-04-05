using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.IO;
using Leaf.Parsing.Grammar;

namespace Leaf.Parsing.AST.FragmentNodes
{
	public class FragmentNode : LeafAstNode
	{
		public IReadOnlyList<ImportNode> Imports => _imports;
		public IReadOnlyList<DefinitionNode> Definitions => _definitions;
		private readonly List<ImportNode> _imports = new List<ImportNode>();
		private readonly List<DefinitionNode> _definitions = new List<DefinitionNode>();
		
		public FragmentNode() {}

		public FragmentNode(LeafParser.Entry_pointContext ctx)
		{
			var imports = ctx.ns_import();
			_imports.Capacity = imports.Length;
			foreach (var import in imports)
				AddImport(new ImportNode(import));

			var defs = ctx.def();
			_definitions.Capacity = defs.Length;
			foreach (var def in defs)
				AddDefinition(DefinitionNode.Create(def));
		}

		public ImportNode AddImport(ImportNode import)
		{
			import.Parent = this;
			_imports.Add(import);
			return import;
		}

		public DefinitionNode AddDefinition(string name)
			=> AddDefinition(new DefinitionNode{Name = name});
		
		public DefinitionNode AddDefinition(DefinitionNode definition)
		{
			definition.Parent = this;
			_definitions.Add(definition);
			return definition;
		}

		public override void AppendToSource(IndentedTextWriter src)
		{
			_imports.Sort((i0, i1) => i0.Length == i1.Length ? 0 : i0.Length < i1.Length ? 1 : 0);
			foreach (var import in Imports)
			{
				import.AppendToSource(src);
				src.WriteLine();
			}

			src.WriteLine();
			foreach (var definition in _definitions)
			{
				definition.AppendToSource(src);
				src.WriteLine(); src.WriteLine();
			}
		}
	}
}