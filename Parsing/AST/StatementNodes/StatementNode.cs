using System;
using System.CodeDom.Compiler;
using Leaf.Parsing.AST.ValueNodes;
using Leaf.Parsing.Grammar;

namespace Leaf.Parsing.AST.StatementNodes
{
	public interface IStatementNode
	{
		public LeafAstNode? Parent { get; internal set; }
		public void AppendToSource(IndentedTextWriter src);
	}
	
	public abstract class StatementNode : LeafAstNode, IStatementNode
	{
		internal static readonly PlaceHolderStatement PlaceHolder = new PlaceHolderStatement();
		internal class PlaceHolderStatement : StatementNode
		{
			public override void AppendToSource(IndentedTextWriter src)
			{
				src.Write("???");
				src.Write(';');
			}
		}
		
		LeafAstNode? IStatementNode.Parent
		{
			get => Parent;
			set => Parent = value;
		}

		public static IStatementNode Create(LeafParser.StatementContext ctx)
		{
			var r = ctx.@return();
			if (r != null) return new ReturnNode(r);

			var w = ctx.@while();
			if (w != null) return new WhileNode(w);

			var i = ctx.@if();
			if (i != null) return new IfNode(i);

			var a = ctx.var_ass();
			if (a != null) return new AssignmentNode(a);

			var c = ctx.function_call();
			if (c != null) return new FunctionCallNode(c);

			var d = ctx.var_def();
			if (d != null)
			{
				if (d.var_def_t() != null)
					return new DeclarationNode(d.var_def_t());
				
				if (d.var_def_v() != null)
					return new DeclarationNode(d.var_def_v());
			}

			throw new NotImplementedException();
		}
	}
}