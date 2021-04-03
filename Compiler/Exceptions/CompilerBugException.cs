using System.Diagnostics;
using System;

namespace Leaf.Compilation.Exceptions
{
	public class CompilerBugException : Exception
	{
		public const string Goofed = "If you're reading this, I done goofed. Please send a bug report, so I don't goof up again.";
		
		public CompilerBugException() 
			: base(Goofed) => Debugger.Break();

		public CompilerBugException(string? message) 
			: base(Goofed + '\n' + message) => Debugger.Break();
	}
}