using Leaf.Parsing.AST.TypeNodes;
using System.CodeDom.Compiler;
using System;

namespace Leaf.Parsing.AST.FunctionNodes
{
	public class ParameterNode : LeafAstNode
	{
		private string _name = "???";
		private TypeNode _type = TypeNode.PlaceHolder;
		
		public ParameterNode() {}
		public ParameterNode(string name, TypeNode type)
			=> (Name, Type) = (name, type);
		
		public ParameterNode(string name, string type)
			=> (Name, Type) = (name, new PlainTypeNode(type));

		public string Name
		{
			get => _name;
			set
			{
				if(!AstRegex.IdRegex.IsPerfectMatch(value))
					throw new ArgumentException($"'{value}' is not a valid member name.");

				_name = value;
			}
		}

		public TypeNode Type
		{
			get => _type;
			set
			{
				_type = value;
				_type.Parent = this;
			}
		}

		public override void AppendToSource(IndentedTextWriter src)
		{
			src.Write(_name);
			src.Write(": ");
			_type.AppendToSource(src);
		}
	}
}