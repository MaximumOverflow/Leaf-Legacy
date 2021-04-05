using Leaf.Parsing.AST.ValueNodes;
using Leaf.Parsing.AST.TypeNodes;
using System.CodeDom.Compiler;
using Leaf.Parsing.Grammar;
using System;

namespace Leaf.Parsing.AST.StatementNodes
{
	public class DeclarationNode : StatementNode
	{
		public bool Mutable;
		public bool Reference;
		private TypeNode? _type;
		private ValueNode? _value;
		private TypeNode? _allocator;
		private string _name = "???";
		
		public DeclarationNode() {}

		public DeclarationNode(LeafParser.Var_def_tContext ctx)
		{
			Line = ctx.Start.Line;
			_name = ctx.Id().GetText();
			Mutable = ctx.Var() != null;
			Type = TypeNode.Create(ctx.type(0));
			if(ctx.type(1) != null) Allocator = TypeNode.Create(ctx.type(1));
		}
		
		public DeclarationNode(LeafParser.Var_def_vContext ctx)
		{
			Line = ctx.Start.Line;
			_name = ctx.Id().GetText();
			Mutable = ctx.Var() != null;
			Reference = ctx.Ref() != null;
			Value = ValueNode.Create(ctx.value());
			if(ctx.type(0) != null) Type = TypeNode.Create(ctx.type(0));
			if(ctx.type(1) != null) Allocator = TypeNode.Create(ctx.type(1));
		}
		
		public string Name
		{
			get => _name;
			set
			{
				if(!AstRegex.IdRegex.IsPerfectMatch(value))
					throw new ArgumentException($"'{value}' is not a valid member name.");

				_name = value;
			}
		}

		public TypeNode? Type
		{
			get => _type;
			set
			{
				_type = value;
				if (_type != null)
					_type.Parent = this;
			}
		}

		public ValueNode? Value
		{
			get => _value;
			set
			{
				_value = value;
				if (_value != null)
					_value.Parent = this;
			}
		}
		
		public TypeNode? Allocator
		{
			get => _allocator;
			set
			{
				_allocator = value;
				if (_allocator != null)
					_allocator.Parent = this;
			}
		}

		public override void AppendToSource(IndentedTextWriter src)
		{
			src.Write(Mutable ? "var " : "let ");
			if(Reference) src.Write("ref ");
			src.Write(_name);
			
			if (_type != null)
			{
				src.Write(": ");
				_type.AppendToSource(src);
			}

			if (_value != null)
			{
				src.Write(" = ");
				_value.AppendToSource(src);
			}
			
			if (_allocator != null)
			{
				src.Write(" : ");
				_allocator.AppendToSource(src);
			}
			
			src.Write(';');
		}
	}
}