using System.CodeDom.Compiler;
using Leaf.Parsing.Grammar;
using System;

namespace Leaf.Parsing.AST.ValueNodes
{
	public class ReferenceNode : ValueNode
	{
		private ValueNode _value = PlaceHolder;

		public ReferenceNode() {}

		public ReferenceNode(LeafParser.ValueContext ctx)
		{
			if (ctx.Ref() == null)
				throw new ArgumentException("Context does not define a reference value.");

			Line = ctx.Start.Line;
			Value = Create(ctx.value(0));
		}

		public ValueNode Value
		{
			get => _value;
			set
			{
				_value = value;
				_value.Parent = this;
			}
		}

		protected override void InnerAppendToSource(IndentedTextWriter src)
		{
			src.Write("ref ");
			_value.AppendToSource(src);
		}
	}
}