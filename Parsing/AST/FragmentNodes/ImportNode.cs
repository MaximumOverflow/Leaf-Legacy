using System;
using System.CodeDom.Compiler;
using System.Text;
using System.Text.RegularExpressions;
using Leaf.Parsing.Grammar;

namespace Leaf.Parsing.AST.FragmentNodes
{
	public class ImportNode : LeafAstNode
	{
		private string? _alias;
		private string _namespace;

		public ImportNode(string ns, string? alias = null)
		{
			_namespace = null!;
			
			Alias = alias;
			Namespace = ns;
		}

		public ImportNode(LeafParser.Ns_importContext ctx)
		{
			Line = ctx.Start.Line;
			_alias = ctx.alias?.Text;
			_namespace = ctx.@namespace().GetText();
		}

		public string Namespace
		{
			get => _namespace;
			set
			{
				if(!AstRegex.NamespaceRegex.IsPerfectMatch(value))
					throw new ArgumentException($"'{value}' is not a valid namespace.");

				_namespace = value;
			}
		}
		
		public string? Alias
		{
			get => _alias;
			set
			{
				if (value != null)
				{
					if(!AstRegex.IdRegex.IsPerfectMatch(value))
						throw new ArgumentException($"'{value}' is not a valid namespace alias.");
				}

				_alias = value;
			}
		}

		public int Length => 7 + _namespace.Length + (4 + _alias?.Length ?? 0);

		public override void AppendToSource(IndentedTextWriter src)
		{
			src.Write("import ");
			src.Write(_namespace);

			if (_alias != null)
			{
				src.Write(" as ");
				src.Write(_alias);
			}
			
			src.Write(';');
		}
	}
}