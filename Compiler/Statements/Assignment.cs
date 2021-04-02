using Leaf.Compilation.Exceptions;
using Leaf.Compilation.Grammar;
using Leaf.Compilation.Values;
using System;

namespace Leaf.Compilation.Statements
{
	public static partial class Statement
	{
		public static Value Compile(this LeafParser.Var_assContext a, in LocalCompilationContext ctx)
		{
			var variable = Value.Get(a.value(0), in ctx).AsPlainLValue(in ctx);
			var value = Value.Get(a.value(1), in ctx, new ValueRetrievalOptions{ExpectedType = variable.Type});

			if ((variable.Flags & ValueFlags.Mutable) == 0)
				throw new CompilationException("Cannot assign to an immutable value.",
					ctx.CurrentScope.Function.Fragment, a.Start.Line);

			if (value.Type != variable.Type)
				throw new NotImplementedException();

			value = value.AsRValue(in ctx);

			ctx.Builder.BuildStore(value.LlvmValue, variable.LlvmValue);
			return variable;
		}
	}
}