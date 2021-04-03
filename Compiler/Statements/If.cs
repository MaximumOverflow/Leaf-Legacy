using Leaf.Compilation.Grammar;
using Leaf.Compilation.Values;

namespace Leaf.Compilation.Statements
{
	public static partial class Statement
	{
		public static Value Compile(this LeafParser.IfContext i, in LocalCompilationContext ctx)
		{
			var builder = ctx.Builder;
			var function = ctx.CurrentFunction;
			var currentScope = ctx.CurrentScope;
			var boolT = ctx.GlobalContext.GlobalNamespace.Types["bool"];

			var check = Value.Get(i.value(), in ctx, new ValueRetrievalOptions {ExpectedType = boolT})
				.CastTo(boolT, in ctx, false, i.value());

			if (i.Else() == null)
			{
				var thenScope = ctx.PushScope();
				var continueBlock = currentScope.AppendLlvmBlock();
				var thenBlock = thenScope.LlvmBlock;

				builder.BuildCondBr(check.LlvmValue, thenBlock, continueBlock);
				builder.PositionAtEnd(thenBlock);

				foreach (var statement in i.conditional_scope(0).statement())
					statement.Compile(in ctx);

				if (!thenScope.ReturnValue.HasValue)
					builder.BuildBr(continueBlock);
				
				builder.PositionAtEnd(continueBlock);
				ctx.PopScope();
			}
			else
			{
				var thenScope = ctx.PushScope();
				var thenBlock = thenScope.LlvmBlock;
				var currentBlock = currentScope.LlvmBlock;

				builder.PositionAtEnd(thenBlock);
				foreach (var statement in i.conditional_scope(0).statement())
					statement.Compile(in ctx);
				
				ctx.PopScope();
				var elseScope = ctx.PushScope();
				var elseBlock = elseScope.LlvmBlock;
				var continueBlock = currentScope.AppendLlvmBlock();

				builder.PositionAtEnd(currentBlock);
				builder.BuildCondBr(check.LlvmValue, thenBlock, elseBlock);
				
				builder.PositionAtEnd(elseBlock);
				if (i.@if() == null)
				{
					foreach (var statement in i.conditional_scope(1).statement())
						statement.Compile(in ctx);
				}
				else i.@if().Compile(in ctx);
				
				builder.PositionAtEnd(thenScope.LlvmBlock);
				if (!thenScope.ReturnValue.HasValue)
					builder.BuildBr(continueBlock);

				builder.PositionAtEnd(elseScope.LlvmBlock);
				if (!elseScope.ReturnValue.HasValue)
				{
					builder.BuildBr(continueBlock);
					builder.PositionAtEnd(continueBlock);
				}
				else currentScope.SetReturnValue(elseScope.ReturnValue.Value);
				ctx.PopScope();
				
			}

			return check;
		}
	}
}