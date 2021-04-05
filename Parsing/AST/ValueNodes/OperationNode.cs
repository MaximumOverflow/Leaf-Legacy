using System.CodeDom.Compiler;
using System;
using Leaf.Parsing.Grammar;

namespace Leaf.Parsing.AST.ValueNodes
{
	public enum Operator
	{
		Add, Sub,
		Mul, Div, Mod,
		
		Eq, Neq
	}
	
	public static class OperatorExtensions
	{
		public static string AsString(this Operator op) => op switch
		{
			Operator.Add => "+",
			Operator.Sub => "-",
			Operator.Mul => "*",
			Operator.Div => "/",
			Operator.Mod => "%",
			Operator.Eq => "==",
			Operator.Neq => "!=",
			_ => throw new ArgumentOutOfRangeException(nameof(op), op, null)
		};
	}
	
	public class OperationNode : ValueNode
	{
		public Operator Operator;
		private ValueNode _value0 = PlaceHolder, _value1 = PlaceHolder;
		
		public OperationNode() {}

		public OperationNode(Operator op, LeafParser.ValueContext v0, LeafParser.ValueContext v1)
		{
			Operator = op;
			Value0 = Create(v0); 
			Value1 = Create(v1);
		}


		public ValueNode Value0
		{
			get => _value0;
			set
			{
				_value0 = value;
				_value0.Parent = this;
			}
		}
		
		public ValueNode Value1
		{
			get => _value0;
			set
			{
				_value1 = value;
				_value1.Parent = this;
			}
		}

		protected override void InnerAppendToSource(IndentedTextWriter src)
		{
			_value0.AppendToSource(src);
			src.Write(' '); src.Write(Operator.AsString()); src.Write(' ');
			_value1.AppendToSource(src);
		}
	}
}