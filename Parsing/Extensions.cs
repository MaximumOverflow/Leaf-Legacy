using System.CodeDom.Compiler;
using System.Text.RegularExpressions;
using Leaf.Parsing.AST;

namespace Leaf.Parsing
{
	public static class RegexExtensions
	{
		public static bool IsPerfectMatch(this Regex regex, string value)
		{				
			var match = regex.Match(value);
			return match.Value == value;
		}
	}
}