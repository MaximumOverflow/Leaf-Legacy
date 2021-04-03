using Type = Leaf.Compilation.Types.Type;
using Leaf.Compilation.CompilationUnits;
using static Crayon.Output;
using System.Text;
using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;


namespace Leaf.Compilation
{
	public static class Warnings
	{
		private static readonly StringBuilder StringBuilder = new StringBuilder();
		
		private static string BuildWarningString(string warning, Fragment? fragment = null, ParserRuleContext? node = null)
		{
			lock (StringBuilder)
			{
				var msg = StringBuilder;
				StringBuilder.Clear();

				if (fragment != null)
				{
					var line = node?.Start.Line;
					msg.Append('[').Append(fragment.File.FullName);
					if (line.HasValue) msg.Append(", line: ").Append(line.Value);
					msg.Append("] ");
				}

				msg.Append(warning);
				if (node != null)
				{
					if (!char.IsWhiteSpace(warning[^1]))
						msg.Append(' ');
					
					msg.Append("Source: ");
					var stream = node.Start.InputStream;
					if (node.Parent is ParserRuleContext parent)
					{
						msg.Append(stream.GetText(new Interval(parent.Start.StartIndex, node.Start.StartIndex - 1)));
						msg.Append(Underline(stream.GetText(new Interval(node.Start.StartIndex, node.Stop.StopIndex))));
					}
					else msg.Append(Underline(stream.GetText(new Interval(node.Start.StartIndex, node.Stop.StopIndex))));
				}
				
				return msg.ToString();
			}
		}
		
		public static void RaiseImplicitCastWarning(Type from, Type to, Fragment? fragment = null, ParserRuleContext? node = null)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(BuildWarningString($"Implicit cast from '{from}' to '{to}' with possible loss of precision.", fragment, node));
		}
	}
}