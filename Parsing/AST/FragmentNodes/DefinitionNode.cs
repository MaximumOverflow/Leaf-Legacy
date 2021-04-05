using Leaf.Parsing.AST.FunctionNodes;
using Leaf.Parsing.AST.TypeNodes;
using System.CodeDom.Compiler;
using Leaf.Parsing.Grammar;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using DotNetCoreUtilities.Extensions;

namespace Leaf.Parsing.AST.FragmentNodes
{
	public class DefinitionNode : LeafAstNode
	{
		private static readonly PlaceHolderNode PlaceHolder = new PlaceHolderNode();
		private class PlaceHolderNode : LeafAstNode
		{
			public override void AppendToSource(IndentedTextWriter src)
				=> src.Write("???");
		}

		public string Name = "???";
		private LeafAstNode _definedNode = PlaceHolder;
		private IReadOnlyList<string> _templateParameters = ImmutableList<string>.Empty;
		
		public LeafAstNode DefinedNode
		{
			get => _definedNode;
			set
			{
				_definedNode = value switch
				{
					StructTypeNode or FunctionNode => value,
					_ => throw new ArgumentException($"Node type {value.GetType().Name} is not valid in this context.")
				};

				_definedNode.Parent = this;
			}
		}

		public IReadOnlyList<string> TemplateParameters
		{
			get => _templateParameters;
			set
			{
				_templateParameters = value;
				foreach (var parameter in _templateParameters)
					if (!AstRegex.IdRegex.IsPerfectMatch(parameter))
						throw new ArgumentException($"'{parameter}' is not a valid template parameter name.");
			}
		}

		public override void AppendToSource(IndentedTextWriter src)
		{
			src.Write("def ");

			var func = _definedNode as FunctionNode;
			if (func is OperatorNode) src.Write("operator ");
			
			src.Write(Name);

			if (_templateParameters.Count != 0)
			{
				src.Write('<');
				for (var i = 0; i < _templateParameters.Count; i++)
				{
					src.Write(_templateParameters[i]);
					if(i < _templateParameters.Count - 1) src.Write(", ");
				}
				src.Write('>');
			}
			
			src.Write(": ");
			_definedNode.AppendToSource(src);
			
			if(func != null && func.Scope == null)
				src.Write(';');
		}

		public static DefinitionNode Create(LeafParser.DefContext ctx)
		{
			var definition = new DefinitionNode{Line = ctx.Start.Line};

			var tp = ctx.def_type();
			var fn = ctx.def_func();
			var op = ctx.def_operator();
			
			if (tp != null)
			{
				definition.Name = tp.Id().GetText();
				definition.DefinedNode = TypeNode.Create(tp);
				return definition;
			}

			if (fn != null)
			{
				var decl = fn.function_decl();
				var impl = fn.function_impl();
				definition.Name = fn.Id().GetText();
				definition.DefinedNode = decl != null
					? new FunctionNode(decl)
					: new FunctionNode(impl);

				var generics = fn.generic_def_list();
				if (generics != null) definition.TemplateParameters = generics.Id()
					.ArraySelect(id => id.GetText());

				return definition;
			}

			if (op != null)
			{
				var impl = op.function_impl();
				definition.Name = op.operator_id().GetText();
				definition.DefinedNode = new OperatorNode(impl);

				var generics = op.generic_def_list();
				if (generics != null) definition.TemplateParameters = generics.Id()
					.ArraySelect(id => id.GetText());

				return definition;
			}

			throw new NotImplementedException();
		}
	}
}