using Leaf.Parsing.AST.ValueNodes;
using System.CodeDom.Compiler;
using Leaf.Parsing.Grammar;

namespace Leaf.Parsing.AST.StatementNodes
{
	public class AssignmentNode : StatementNode
	{
		private ValueNode _lValue = ValueNode.PlaceHolder;
		private ValueNode _rValue = ValueNode.PlaceHolder;
		
		public AssignmentNode() {}

		public AssignmentNode(LeafParser.Var_assContext ctx)
		{
			Line = ctx.Start.Line;
			_lValue = ValueNode.Create(ctx.value(0));
			_rValue = ValueNode.Create(ctx.value(1));
		}

		public ValueNode LValue
		{
			get => _lValue;
			set
			{
				_lValue = value;
				_lValue.Parent = this;
			}
		}
		
		public ValueNode RValue
		{
			get => _rValue;
			set
			{
				_rValue = value;
				_rValue.Parent = this;
			}
		}

		public override void AppendToSource(IndentedTextWriter src)
		{
			_lValue.AppendToSource(src);
			src.Write(" = ");
			_rValue.AppendToSource(src);
			src.Write(';');
		}
	}
}