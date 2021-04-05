using System.CodeDom.Compiler;

namespace Leaf.Parsing.AST.TypeNodes
{
	public class PointerNode : TypeNode
	{
		private TypeNode _base = PlaceHolder;
		
		public PointerNode() {}
		public PointerNode(TypeNode @base) 
			=> Base = @base;
		
		public PointerNode(string @base, string? ns = null) 
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
			_base.AppendToSource(src);
			src.Write('*');
		}
	}
}