using Leaf.Parsing.AST.StatementNodes;
using Leaf.Parsing.AST.ValueNodes;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Leaf.Parsing.Grammar;

namespace Leaf.Parsing.AST.FunctionNodes
{
	public class ScopeNode : LeafAstNode
	{
		private List<IStatementNode> _statements = new List<IStatementNode>();
		
		public ScopeNode() {}

		public ScopeNode(LeafParser.Function_scopeContext ctx)
		{
			Line = ctx.Start.Line;
			if (ctx.value() != null)
			{
				AppendStatement(new ReturnNode {Value = ValueNode.Create(ctx.value())});
				return;
			}

			foreach (var statement in ctx.statement())
				AppendStatement(StatementNode.Create(statement));
		}
		
		public ScopeNode(LeafParser.Conditional_scopeContext ctx)
		{
			Line = ctx.Start.Line;
			foreach (var statement in ctx.statement())
				AppendStatement(StatementNode.Create(statement));
		}

		public IStatementNode AppendStatement(IStatementNode statement)
		{
			statement.Parent = this;
			_statements.Add(statement);
			return statement;
		}
		
		public void AppendNewLine() 
			=> _statements.Add(EmptyStatementNode.Instance);

		public IReadOnlyList<IStatementNode> StatementNodes
		{
			get => _statements;
			set
			{
				_statements.Clear();
				_statements.AddRange(value);
				foreach (var statement in _statements)
					statement.Parent = this;
			}
		}
		
		public override void AppendToSource(IndentedTextWriter src)
		{
			src.WriteLine(" {");
			src.Indent++;
			foreach (var statement in _statements)
			{
				statement.AppendToSource(src);
				src.WriteLine();
			}
			src.Indent--;
			src.Write('}');
		}
	}
}