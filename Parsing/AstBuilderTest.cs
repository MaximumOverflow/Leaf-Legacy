using Leaf.Parsing.AST.StatementNodes;
using Leaf.Parsing.AST.FragmentNodes;
using Leaf.Parsing.AST.FunctionNodes;
using Leaf.Parsing.AST.ValueNodes;
using Leaf.Parsing.AST.TypeNodes;
using System;

namespace Leaf.Parsing
{
	public static class AstBuilderTest
	{
		public static void Test()
		{
			var fragment = new FragmentNode();
			fragment.AddImport(new ImportNode("C.Interop.Stdio", "stdio"));
			
			var typeDef = fragment.AddDefinition("Vec2");
			var type = new StructTypeNode(); typeDef.DefinedNode = type;
			type.Members = new[]
			{
				new MemberNode("x", "f32"),
				new MemberNode("y", "f32"),
			};

			var funcDef = fragment.AddDefinition("main");
			var func = new FunctionNode(); funcDef.DefinedNode = func;
			func.Parameters = new[]
			{
				new ParameterNode("argc", new PointerNode("i8")),
				new ParameterNode("argv", "i32"),
			};
			
			var scope = func.Scope = new ScopeNode();
			scope.AppendStatement(new DeclarationNode
			{
				Name = "test",
				Mutable = true,
				Type = new PlainTypeNode("Vec2"),
				Allocator = new PlainTypeNode("__heap_allocator")
			});

			scope.AppendStatement(new FunctionCallNode
			{
				LValue = new IdNode("printf"),
				Parameters = new ValueNode[]
				{
					new StringNode("%s\n"){CString = true},
					new IdNode("test")
					{
						Child = new IdNode("x")
						{
							Child = new FunctionCallNode
							{
								LValue = new IdNode("toString"),
								Child = new FunctionCallNode
								{
									LValue = new IdNode("buffer")
								}
							}
						}
					},
				}
			});

			Console.Write(fragment);
		}
	}
}