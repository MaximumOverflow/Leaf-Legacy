using System.Collections.Immutable;
using System.Collections.Generic;
using Leaf.Parsing.AST.TypeNodes;
using System.CodeDom.Compiler;
using DotNetCoreUtilities.Extensions;
using Leaf.Parsing.Grammar;

namespace Leaf.Parsing.AST.FunctionNodes
{
	public class FunctionNode : LeafAstNode
	{
		private ScopeNode? _scope;
		private TypeNode? _returnType;
		private IReadOnlyList<ParameterNode> _parameters = ImmutableList<ParameterNode>.Empty;
		
		public FunctionNode() {}

		public FunctionNode(LeafParser.Function_declContext ctx)
		{
			Line = ctx.Start.Line;
			_returnType = TypeNode.Create(ctx.type());
			Parameters = ctx.parameter_def().ArraySelect(p
				=> new ParameterNode(p.Id().GetText(), TypeNode.Create(p.type())));
		}
		
		public FunctionNode(LeafParser.Function_implContext ctx)
		{
			Line = ctx.Start.Line;
			_returnType = TypeNode.Create(ctx.type());
			Parameters = ctx.parameter_def().ArraySelect(p
				=> new ParameterNode(p.Id().GetText(), TypeNode.Create(p.type())));

			Scope = new ScopeNode(ctx.function_scope());
		}

		public ScopeNode? Scope
		{
			get => _scope;
			set
			{
				_scope = value;
				if (_scope != null)
					_scope.Parent = this;
			}
		}

		public TypeNode? ReturnType
		{
			get => _returnType;
			set
			{
				_returnType = value;
				if (_returnType != null)
					_returnType.Parent = this;
			}
		}

		public IReadOnlyList<ParameterNode> Parameters
		{
			get => _parameters;
			set
			{
				_parameters = value;
				foreach (var parameter in _parameters)
					parameter.Parent = this;
			}
		}

		public override void AppendToSource(IndentedTextWriter src)
		{
			src.Write('(');
			for (var i = 0; i < _parameters.Count; i++)
			{
				_parameters[i].AppendToSource(src);
				if(i < _parameters.Count - 1) src.Write(", ");
			}
			src.Write(')');

			if (_returnType != null)
			{
				src.Write(" -> ");
				_returnType.AppendToSource(src);
			}
			else src.Write(" -> void");

			_scope?.AppendToSource(src);
		}
	}
}