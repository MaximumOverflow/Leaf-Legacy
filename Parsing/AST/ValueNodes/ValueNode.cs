using DotNetCoreUtilities.Extensions;
using System.CodeDom.Compiler;
using Leaf.Parsing.Grammar;
using System;

namespace Leaf.Parsing.AST.ValueNodes
{
	public abstract class ValueNode : LeafAstNode
	{
		internal static readonly PlaceHolderValue PlaceHolder = new PlaceHolderValue();
		internal class PlaceHolderValue : ValueNode
		{
			protected override void InnerAppendToSource(IndentedTextWriter src)
				=> src.Write("???");
		}

		private ValueNode? _child;

		public ValueNode? Child
		{
			get => _child;
			set
			{
				_child = value;
				if (_child != null)
					_child.Parent = this;
			}
		}

		protected abstract void InnerAppendToSource(IndentedTextWriter src);

		
		public sealed override void AppendToSource(IndentedTextWriter src)
		{
			InnerAppendToSource(src);
			if (_child != null)
			{
				src.Write('.');
				_child.AppendToSource(src);
			}
		}

		public static ValueNode[] Create(LeafParser.ValueContext[]? ctx) 
			=> ctx.ThisOrEmpty().ArraySelect(Create);
		
		public static ValueNode Create(LeafParser.ValueContext ctx)
		{
			var id = ctx.Id()?.GetText();
			if (id != null) return new IdNode(id);

			if (ctx.Ref() != null) 
				return new ReferenceNode(ctx);

			if (ctx.call != null)
				return new FunctionCallNode(ctx);

			var str = ctx.String();
			if (str != null) return new StringNode(str.GetText()[1..^1]);
			
			var cstr = ctx.CString();
			if (cstr != null) return new StringNode(cstr.GetText()[2..^1]){CString = true};

			if (ctx.As() != null)
				return new CastNode(ctx.value(0), ctx.type());

			if(ctx.nested != null)
			{
				var value = Create(ctx.value(0));
				var child = Create(ctx.value(1));
				
				var parent = value;
				while (parent.Child != null) parent = parent.Child;
				parent.Child = child;

				return value;
			}
			
			var i = ctx.integer();
			if (i != null) return IntegerNode.Create(i);

			var init = ctx.initialization_list();
			if (init != null) return new InitListNode(init);

			if (ctx.Eq() != null)  return new OperationNode(Operator.Eq , ctx.value(0), ctx.value(1));
			if (ctx.Neq() != null) return new OperationNode(Operator.Neq, ctx.value(0), ctx.value(1));
			if (ctx.Add() != null) return new OperationNode(Operator.Add, ctx.value(0), ctx.value(1));
			if (ctx.Sub() != null) return new OperationNode(Operator.Sub, ctx.value(0), ctx.value(1));
			if (ctx.Mul() != null) return new OperationNode(Operator.Mul, ctx.value(0), ctx.value(1));
			if (ctx.Div() != null) return new OperationNode(Operator.Div, ctx.value(0), ctx.value(1));
			if (ctx.Mod() != null) return new OperationNode(Operator.Mod, ctx.value(0), ctx.value(1));

			throw new NotImplementedException();
		}
	}
}