using System;

namespace Leaf.Compilation
{
	[Flags]
	public enum Flags
	{
		None		= 0,
		Unsafe		= 0b00000001,
		CCompat		= 0b00000010,
		Generic		= 0b00000100,
		Unsigned	= 0b00001000,
		External	= 0b00010010,
		Primitive	= 0b00100000,
		VarArgs		= 0b01000000,
	}
}