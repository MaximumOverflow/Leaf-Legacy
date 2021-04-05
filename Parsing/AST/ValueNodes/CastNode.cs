using Leaf.Parsing.AST.TypeNodes;
using System.CodeDom.Compiler;
using Leaf.Parsing.Grammar;

namespace Leaf.Parsing.AST.ValueNodes
{
	public class CastNode : ValueNode
	{
		private ValueNode _value = PlaceHolder;
		private TypeNode _type = TypeNode.PlaceHolder;
		
		public CastNode() {}

		public CastNode(LeafParser.ValueContext v, LeafParser.TypeContext t)
		{
			Line = v.Start.Line;
			Value = Create(v);
			Type = TypeNode.Create(t);
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

		public TypeNode Type
		{
			get => _type;
			set
			{
				_type = value;
				_type.Parent = this;
			}
		}

		protected override void InnerAppendToSource(IndentedTextWriter src)
		{
			if (Child != null)
			{
				src.Write('(');
				_value.AppendToSource(src);
				src.Write(" as ");
				_type.AppendToSource(src);
				src.Write(')');
			}
			else
			{
				_value.AppendToSource(src);
				src.Write(" as ");
				_type.AppendToSource(src);
			}
		}
	}
}