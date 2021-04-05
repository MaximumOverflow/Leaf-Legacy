using System;
using System.CodeDom.Compiler;

namespace Leaf.Parsing.AST.TypeNodes
{
	public class PlainTypeNode : TypeNode
	{
		private string _name = null!;
		private string? _namespace;

		public PlainTypeNode(string name, string? ns = null)
			=> (Name, Namespace) = (name, ns);

		public string Name
		{
			get => _name;
			set
			{
				if(!AstRegex.IdRegex.IsPerfectMatch(value))
					throw new ArgumentException($"'{value}' is not a valid name.");

				_name = value;
			}
		}


		public string? Namespace
		{
			get => _namespace;
			set
			{
				if (value != null)
				{
					if(!AstRegex.IdRegex.IsPerfectMatch(value))
						throw new ArgumentException($"'{value}' is not a valid namespace alias.");
				}

				_namespace = value;
			}
		}
		
		public override void AppendToSource(IndentedTextWriter src)
		{
			if (_namespace != null)
			{
				src.Write(_namespace);
				src.Write('.');
			}
			
			src.Write(_name);
		}
	}
}