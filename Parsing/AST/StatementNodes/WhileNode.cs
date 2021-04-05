using Leaf.Parsing.AST.FunctionNodes;
using Leaf.Parsing.AST.ValueNodes;
using System.CodeDom.Compiler;
using Leaf.Parsing.Grammar;

namespace Leaf.Parsing.AST.StatementNodes
{
	public class WhileNode : StatementNode
	{
		public ScopeNode Scope { get; private set; }
		private ValueNode _check = ValueNode.PlaceHolder;

		public WhileNode() => Scope = new ScopeNode();

		public WhileNode(LeafParser.WhileContext ctx)
		{
			Line = ctx.Start.Line;
			Check = ValueNode.Create(ctx.value());
			Scope = new ScopeNode(ctx.conditional_scope());
		}

		public ValueNode Check
		{
			get => _check;
			set
			{
				_check = value;
				_check.Parent = this;
			}
		}

		public override void AppendToSource(IndentedTextWriter src)
		{
			src.Write("while ");
			_check.AppendToSource(src);
			Scope.AppendToSource(src);
		}
	}
}