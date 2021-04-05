using System.CodeDom.Compiler;
using System.IO;

namespace Leaf.Parsing.AST
{
	public abstract class LeafAstNode
	{
		public int? Line { get; protected init; }
		public LeafAstNode? Parent { get; internal set; }


		public abstract void AppendToSource(IndentedTextWriter src);

		public sealed override string ToString()
		{
			var writer = new StringWriter();
			AppendToSource(new IndentedTextWriter(writer));
			return writer.ToString();
		}
	}
}