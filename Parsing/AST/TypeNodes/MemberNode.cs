using System.CodeDom.Compiler;
using Leaf.Parsing.Grammar;
using System;

namespace Leaf.Parsing.AST.TypeNodes
{
	public class MemberNode : LeafAstNode
	{
		public bool Public;
		private string _name = "???";
		private TypeNode _type = TypeNode.PlaceHolder;
		
		public MemberNode(string name, TypeNode type)
			=> (Name, Type) = (name, type);
		
		public MemberNode(string name, string type)
			=> (Name, Type) = (name, new PlainTypeNode(type));

		public MemberNode(LeafParser.Type_memberContext ctx)
			: this(ctx.Id().GetText(), TypeNode.Create(ctx.type()))
			=> (Public, Line) = (ctx.Pub() != null, ctx.Start.Line);

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
			if(Public) src.Write("pub ");
			_type.AppendToSource(src);
			src.Write(';');
		}
	}
}