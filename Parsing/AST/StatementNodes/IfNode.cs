using Leaf.Parsing.AST.FunctionNodes;
using Leaf.Parsing.AST.ValueNodes;
using System.CodeDom.Compiler;
using Leaf.Parsing.Grammar;

namespace Leaf.Parsing.AST.StatementNodes
{
	public class IfNode : StatementNode
	{
		private ElseNode? _else;
		private ElseIfNode? _elseIf;
		private ValueNode _check = ValueNode.PlaceHolder;
		public ScopeNode Scope { get; private set; }

		public IfNode() => Scope = new ScopeNode();

		public IfNode(LeafParser.IfContext ctx)
		{
			Line = ctx.Start.Line;
			Check = ValueNode.Create(ctx.value());
			Scope = new ScopeNode(ctx.conditional_scope(0));

			if (ctx.Else() != null)
			{
				if (ctx.conditional_scope(1) != null)
					Else = new ElseNode(ctx.conditional_scope(1));
				else ElseIf = new ElseIfNode(ctx.@if());
			}
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
		
		public ElseNode? Else
		{
			get => _else;
			set
			{
				_else = value;
				if (_else != null)
					_else.Parent = this;
			}
		}

		public ElseIfNode? ElseIf
		{
			get => _elseIf;
			set
			{
				_elseIf = value;
				if (_elseIf != null)
					_elseIf.Parent = this;
			}
		}

		public override void AppendToSource(IndentedTextWriter src)
		{
			src.Write("if ");
			_check.AppendToSource(src);
			Scope.AppendToSource(src);

			if (_else != null)
			{
				src.WriteLine();
				_else.AppendToSource(src);
			}
			
			if (_elseIf != null)
			{
				src.WriteLine();
				_elseIf.AppendToSource(src);
			}
		}
	}
	
	public class ElseNode : StatementNode
	{
		public ScopeNode Scope { get; private set; }

		public ElseNode() => Scope = new ScopeNode();

		public ElseNode(LeafParser.Conditional_scopeContext ctx)
		{
			Line = ctx.Start.Line;
			Scope = new ScopeNode(ctx);
		}

		public override void AppendToSource(IndentedTextWriter src)
		{
			src.Write("else");
			Scope.AppendToSource(src);
		}
	}
	
	public class ElseIfNode : IfNode
	{
		public ElseIfNode() {}

		public ElseIfNode(LeafParser.IfContext ctx) : base(ctx) {}

		public override void AppendToSource(IndentedTextWriter src)
		{
			src.Write("else if ");
			Check.AppendToSource(src);
			Scope.AppendToSource(src);

			if (Else != null)
			{
				src.WriteLine();
				Else.AppendToSource(src);
			}
			
			if (ElseIf != null)
			{
				src.WriteLine();
				ElseIf.AppendToSource(src);
			}
		}
	}
}