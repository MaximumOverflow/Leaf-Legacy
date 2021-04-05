using System.Text.RegularExpressions;

namespace Leaf.Parsing.AST
{
	public static class AstRegex
	{
		public static readonly Regex IdRegex = new Regex("^[a-zA-Z_][a-zA-Z0-9_]*$", RegexOptions.Compiled);
		public static readonly Regex NamespaceRegex = new Regex("^[a-zA-Z_][a-zA-Z0-9_]*(\\.[a-zA-Z_][a-zA-Z0-9_]*)*$", RegexOptions.Compiled);
	}
}