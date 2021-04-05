using System;
using System.CodeDom.Compiler;
using System.Text.RegularExpressions;

namespace Leaf.Parsing.AST.ValueNodes
{
	public class StringNode : ValueNode
	{
		public bool CString;
		private string _value = "";
		
		public StringNode() {}
		public StringNode(string str) => Value = str;

		public string Value
		{
			get => _value;
			set => _value = Regex.Escape(value);
		}

		protected override void InnerAppendToSource(IndentedTextWriter src)
		{
			if(CString) src.Write('&');
			src.Write('"');
			src.Write(Regex.Unescape(_value));
			src.Write('"');
		}
	}
}