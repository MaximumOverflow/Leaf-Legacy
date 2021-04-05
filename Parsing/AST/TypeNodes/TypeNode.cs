using System.CodeDom.Compiler;
using Leaf.Parsing.Grammar;
using System;

namespace Leaf.Parsing.AST.TypeNodes
{
	public abstract class TypeNode : LeafAstNode
	{
		internal static readonly PlaceHolderType PlaceHolder = new PlaceHolderType();
		internal class PlaceHolderType : TypeNode
		{
			public override void AppendToSource(IndentedTextWriter src)
				=> src.Write("???");
		}
		
		public static TypeNode Create(LeafParser.Def_typeContext ctx)
		{
			var s = ctx.@struct();
			if (s != null) return new StructTypeNode(s);
			throw new NotImplementedException();
		}
		
		public static TypeNode Create(LeafParser.TypeContext ctx)
		{
			if (ctx.name != null)
				return new PlainTypeNode(ctx.name.Text, ctx.ns?.Text);
			
			if (ctx.ptr != null)
				return new PointerNode(Create(ctx.type(0)));

			if (ctx.Ref() != null)
				return new ReferenceTypeNode(Create(ctx.type(0))) {Mutable = ctx.mut != null};

			throw new NotImplementedException();
		}
	}
}