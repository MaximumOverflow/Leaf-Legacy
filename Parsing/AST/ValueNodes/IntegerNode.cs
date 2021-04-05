using System.CodeDom.Compiler;
using Leaf.Parsing.Grammar;
using System;

namespace Leaf.Parsing.AST.ValueNodes
{
	public abstract class IntegerNode : ValueNode
	{
		public static IntegerNode Create(LeafParser.IntegerContext ctx)
		{
			var i = ctx.Integer();
			var u = ctx.UnsignedInteger();

			if (i != null)
			{
				var value = long.Parse(i.GetText());
				return value switch
				{
					<= sbyte.MaxValue => new IntegerNode<sbyte>((sbyte) value),
					<= short.MaxValue => new IntegerNode<short>((short) value),
					<= int.MaxValue => new IntegerNode<int>((int) value),
					_ => new IntegerNode<long>(value),
				};
			}
			
			if (u != null)
			{
				var value = ulong.Parse(u.GetText());
				return value switch
				{
					<= byte.MaxValue => new IntegerNode<byte>((byte) value),
					<= ushort.MaxValue => new IntegerNode<ushort>((ushort) value),
					<= uint.MaxValue => new IntegerNode<uint>((uint) value),
					_ => new IntegerNode<ulong>(value),
				};
			}

			throw new ArgumentException("Invalid integer context.");
		}
	}
	
	public class IntegerNode<T> : IntegerNode where T : unmanaged
	{
		public readonly T Value;
		
		public IntegerNode(T value)
		{
			Value = value;
			if (typeof(T) != typeof(sbyte) && typeof(T) != typeof(short) && typeof(T) != typeof(int) && typeof(T) != typeof(long) &&
				typeof(T) != typeof(byte) && typeof(T) != typeof(ushort) && typeof(T) != typeof(uint) && typeof(T) != typeof(ulong))
				throw new Exception("Specified type is not an integer type.");
		}

		protected override void InnerAppendToSource(IndentedTextWriter src)
			=> src.Write(Value);
	}
}