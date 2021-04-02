using System;
using System.Net.Sockets;
using Leaf.Compilation.CompilationUnits;
using Leaf.Compilation.Grammar;
using Leaf.Compilation.Values;

namespace Leaf.Compilation.Statements
{
	public static partial class Statement
	{
		public static Value Compile(this LeafParser.WhileContext w, in LocalCompilationContext ctx)
		{
			var builder = ctx.Builder;
			var fn = ctx.CurrentFunction;
			var currentScope = ctx.CurrentScope;
			var boolT = ctx.GlobalContext.GlobalNamespace.Types["bool"];

			var checkBlock = fn.LlvmValue.AppendBasicBlock("");
			var whileScope = new Scope(currentScope, ctx.CurrentFunction);
			var whileBlock = whileScope.LlvmBlock;
			var continueBlock = currentScope.AppendLlvmBlock();

			builder.BuildBr(checkBlock);
			builder.PositionAtEnd(checkBlock);
			var check = Value.Get(w.value(), in ctx);

			if (check.Type != boolT)
				throw new NotImplementedException();

			builder.BuildCondBr(check.LlvmValue, whileBlock, continueBlock);
			builder.PositionAtEnd(whileBlock);
			
			foreach (var statement in w.conditional_scope().statement())
				statement.Compile(in ctx);
			
			builder.BuildBr(checkBlock);
			builder.PositionAtEnd(continueBlock);

			return check;
		}
	}
}