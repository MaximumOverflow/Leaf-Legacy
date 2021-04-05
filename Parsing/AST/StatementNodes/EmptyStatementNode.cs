using System.CodeDom.Compiler;

namespace Leaf.Parsing.AST.StatementNodes
{
	public class EmptyStatementNode : StatementNode
	{
		internal static readonly EmptyStatementNode Instance = new EmptyStatementNode();
		public override void AppendToSource(IndentedTextWriter src) {}
	}
}