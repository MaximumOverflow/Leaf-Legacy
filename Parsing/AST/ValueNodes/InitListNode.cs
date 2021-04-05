using Leaf.Parsing.AST.TypeNodes;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Leaf.Parsing.Grammar;

namespace Leaf.Parsing.AST.ValueNodes
{
	public class InitListNode : ValueNode
	{
		private TypeNode _type = TypeNode.PlaceHolder;
		private List<InitListMemberAssignmentNode> _values = new List<InitListMemberAssignmentNode>();
		
		public InitListNode() {}

		public InitListNode(LeafParser.Initialization_listContext ctx)
		{
			Line = ctx.Start.Line;
			_values.Capacity = ctx.Id().Length;
			Type = TypeNode.Create(ctx.type());
			for (var i = 0; i < _values.Capacity; i++)
				AddMemberAssignment(new InitListMemberAssignmentNode(ctx, i));
		}

		public InitListMemberAssignmentNode AddMemberAssignment(InitListMemberAssignmentNode member)
		{
			_values.Add(member);
			member.Parent = this;
			return member;
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

		public IReadOnlyList<InitListMemberAssignmentNode> Values
		{
			get => _values;
			set
			{
				_values.Clear();
				_values.AddRange(value);
				foreach (var member in value)
					member.Parent = this;
			}
		}

		protected override void InnerAppendToSource(IndentedTextWriter src)
		{
			_type.AppendToSource(src);
			src.WriteLine(" {");
			src.Indent++;
			foreach (var member in _values)
			{
				member.AppendToSource(src);
				src.WriteLine(',');
			}
			src.Indent--;
			src.Write('}');
		}
	}

	public class InitListMemberAssignmentNode : LeafAstNode
	{
		public string Name = "???";
		private ValueNode _value = ValueNode.PlaceHolder;
		
		public InitListMemberAssignmentNode() {}

		public InitListMemberAssignmentNode(LeafParser.Initialization_listContext ctx, int i)
		{
			Name = ctx.Id(i).GetText();
			Value = ValueNode.Create(ctx.value(i));
		}

		public ValueNode Value
		{
			get => _value;
			set
			{
				_value = value;
				_value.Parent = this;
			}
		}

		public override void AppendToSource(IndentedTextWriter src)
		{
			src.Write(Name);
			src.Write(" = ");
			_value.AppendToSource(src);
		}
	}
}