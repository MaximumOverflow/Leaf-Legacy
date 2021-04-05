using System.CodeDom.Compiler;
using System;

namespace Leaf.Parsing.AST.ValueNodes
{
	public class NamespaceAliasNode : ValueNode
	{
		private string _id = "???";
		
		public NamespaceAliasNode() {}
		public NamespaceAliasNode(string id) => Id = id; 

		public string Id
		{
			get => _id;
			set
			{
				if(!AstRegex.IdRegex.IsPerfectMatch(value))
					throw new ArgumentException($"'{value}' is not a valid member name.");

				_id = value;
			}
		}

		protected override void InnerAppendToSource(IndentedTextWriter src)
			=> src.Write(_id);
	}
}