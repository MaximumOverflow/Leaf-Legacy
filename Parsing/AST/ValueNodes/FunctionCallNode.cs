using System;
using Leaf.Parsing.AST.StatementNodes;
using Leaf.Parsing.AST.FunctionNodes;
using System.Collections.Immutable;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Linq;
using Leaf.Parsing.Grammar;

namespace Leaf.Parsing.AST.ValueNodes
{
	public class FunctionCallNode : ValueNode, IStatementNode
	{
		private ValueNode _lValue = PlaceHolder;
		private IReadOnlyList<ValueNode> _parameters = ImmutableList<ValueNode>.Empty;
		
		public FunctionCallNode() {}

		public FunctionCallNode(LeafParser.ValueContext ctx)
		{
			if (ctx.call == null)
				throw new ArgumentException("Context does not define a function call.");

			var values = ctx.value();
			
			Line = ctx.Start.Line;
			LValue = Create(values[0]);
			
			var parameters = new ValueNode[values.Length - 1];
			for (var i = 0; i < parameters.Length; i++)
				parameters[i] = Create(values[i + 1]);
			
			Parameters = parameters;
		}
		
		public FunctionCallNode(LeafParser.Function_callContext ctx)
		{
			var values = ctx.value();
			
			Line = ctx.Start.Line;
			LValue = Create(values[0]);
			
			var parameters = new ValueNode[values.Length - 1];
			for (var i = 0; i < parameters.Length; i++)
				parameters[i] = Create(values[i + 1]);
			
			Parameters = parameters;
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

		public IReadOnlyList<ValueNode> Parameters
		{
			get => _parameters;
			set
			{
				_parameters = value;
				foreach (var parameter in _parameters)
					parameter.Parent = this;
			}
		}

		LeafAstNode? IStatementNode.Parent
		{
			get => Parent;
			set => Parent = value;
		}

		protected override void InnerAppendToSource(IndentedTextWriter src)
		{
			_lValue.AppendToSource(src);
			src.Write('(');
			for (var i = 0; i < _parameters.Count; i++)
			{
				_parameters[i].AppendToSource(src);
				if(i < _parameters.Count - 1) src.Write(", ");
			}
			src.Write(')');
			
			if(Parent is ScopeNode)
				src.Write(';');
		}
	}
}