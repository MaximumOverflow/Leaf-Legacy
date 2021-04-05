using Leaf.Parsing.AST.FragmentNodes;
using Leaf.Parsing.Grammar;
using Antlr4.Runtime;
using System;

namespace Leaf.Parsing
{
	public class Program
	{
		const string Path = "/home/max/RiderProjects/Leaf/Compiler/ProjectTests/test.leaf";
		
		public static void Main()
		{
			var stream = new AntlrFileStream(Path);
			var lexer = new LeafLexer(stream);
			var tokens = new CommonTokenStream(lexer);
			var parser = new LeafParser(tokens);

			var fragment = new FragmentNode(parser.entry_point());
			Console.WriteLine(fragment.ToString());
		}
	}
}