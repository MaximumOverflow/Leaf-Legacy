using System.CodeDom.Compiler;

namespace Leaf.Parsing.AST.TypeNodes
{
	public class ReferenceTypeNode : TypeNode
	{
		public bool Mutable;
		private TypeNode _base = PlaceHolder;
		
		public ReferenceTypeNode() {}
		
		public ReferenceTypeNode(TypeNode @base) 
			=> Base = @base;
		
		public ReferenceTypeNode(string @base, string? ns = null) 
			=> Base = new PlainTypeNode(@base, ns);

		public TypeNode Base
		{
			get => _base;
			set
			{
				_base = value;
				_base.Parent = this;
			}
		}

		public override void AppendToSource(IndentedTextWriter src)
		{
			if(Mutable) src.Write("mut ");
			src.Write("ref ");
			_base.AppendToSource(src);
		}
	}
}