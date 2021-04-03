using Leaf.Compilation.CompilationUnits;
using System.Diagnostics;
using System.Text;
using System;

namespace Leaf.Compilation.Exceptions
{
    public class CompilationException : Exception
	{
		public readonly int? Line;
		public readonly Fragment? Fragment;
		public readonly CompilationException? Cause;

		public CompilationException(string? message, Fragment? fragment, int? line = null, CompilationException? cause = null) : base(message)
		{
			Line = line;
			Cause = cause;
			Fragment = fragment;
			Debugger.Break();
		}

		public override string Message
		{
			get
			{
				var msg = new StringBuilder();
				
				if (Cause != null) 
					msg.AppendLine(Cause.Message);
				
				if (Fragment != null)
				{
					msg.Append('[').Append(Fragment.File.FullName);
					if (Line.HasValue) msg.Append(", line: ").Append(Line.Value);
					msg.Append("] ");
				}

				msg.Append(base.Message);
				return msg.ToString();
			}
		}
	}
}