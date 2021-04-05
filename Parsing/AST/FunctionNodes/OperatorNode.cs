using Leaf.Parsing.Grammar;

namespace Leaf.Parsing.AST.FunctionNodes
{
	public class OperatorNode : FunctionNode
	{
		public OperatorNode() {}
		public OperatorNode(LeafParser.Function_implContext ctx) : base(ctx) {}
	}
}