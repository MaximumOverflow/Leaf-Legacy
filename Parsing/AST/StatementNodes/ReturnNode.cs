using System.CodeDom.Compiler;
using Leaf.Parsing.AST.ValueNodes;
using Leaf.Parsing.Grammar;

namespace Leaf.Parsing.AST.StatementNodes
{
	public class ReturnNode : StatementNode
	{
		private ValueNode? _value;
		
		public ReturnNode(){}

		public ReturnNode(LeafParser.ReturnContext ctx)
		{
			Line = ctx.Start.Line;
			Value = ValueNode.Create(ctx.value());
		}

		public ValueNode? Value
		{
			get => _value;
			set
			{
				_value = value;
				if(_value != null)
					_value.Parent = this;
			}
		}
		
		public override void AppendToSource(IndentedTextWriter src)
		{
			if (_value != null)
			{
				src.Write("return ");
				_value.AppendToSource(src);
			}
			else src.Write("return");
			src.Write(';');
		}
	}
}