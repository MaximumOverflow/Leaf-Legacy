using System.Collections.Generic;
using System.CodeDom.Compiler;
using System;
using DotNetCoreUtilities.Extensions;
using Leaf.Parsing.Grammar;

namespace Leaf.Parsing.AST.TypeNodes
{
	public class StructTypeNode : TypeNode
	{
		private readonly Dictionary<string, MemberNode> _members = new Dictionary<string, MemberNode>();

		public StructTypeNode() {}
		
		public StructTypeNode(LeafParser.StructContext ctx)
		{
			Line = ctx.Start.Line;
			Members = ctx.type_member().ArraySelect(m => new MemberNode(m));
		}

		public IReadOnlyCollection<MemberNode> Members
		{
			get => _members.Values;
			set
			{
				if (value == null)
					throw new ArgumentNullException(nameof(value));
				
				_members.Clear();
				foreach (var member in value)
				{
					member.Parent = this;
					_members.Add(member.Name, member);
				}
			}
		}

		public override void AppendToSource(IndentedTextWriter src)
		{
			src.Write("struct {"); src.Indent++;
			if(_members.Count != 0) src.WriteLine();
			foreach (var member in Members)
			{
				member.AppendToSource(src);
				src.WriteLine();
			}
			
			src.Indent--; src.Write('}');
		}
	}
}