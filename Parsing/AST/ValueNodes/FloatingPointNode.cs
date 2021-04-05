using System.CodeDom.Compiler;
using System;

namespace Leaf.Parsing.AST.ValueNodes
{
	public class FloatingPointNode<T> : ValueNode where T : unmanaged
	{
		public readonly T Value;
		
		public FloatingPointNode(T value)
		{
			Value = value;
			if (typeof(T) != typeof(Half) && typeof(T) != typeof(float) && typeof(T) != typeof(double))
				throw new Exception("Specified type is not a floating point type.");
		}

		protected override void InnerAppendToSource(IndentedTextWriter src)
			=> src.Write(Value);
	}
}