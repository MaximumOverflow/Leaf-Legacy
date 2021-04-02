using System;
using Leaf.Compilation.Grammar;
using Leaf.Compilation.Types;
using Leaf.Compilation.Values;

namespace Leaf.Compilation.Statements
{
	public static partial class Statement
	{
		public static Value Compile(this LeafParser.ReturnContext r, in LocalCompilationContext ctx)
		{
			var builder = ctx.Builder;
			var ret = r.value();

			if (ret == null)
			{
				ctx.Builder.BuildRetVoid();
				ctx.CurrentScope.SetReturnValue(default);
				return default;
			}

			var value = Value.Get(r.value(), in ctx,
			new ValueRetrievalOptions {ExpectedType = ctx.CurrentFunction.ReturnType});

			if (r.cond != null)
				throw new NotImplementedException();

			if (value.Type != ctx.CurrentFunction.ReturnType)
				throw new NotImplementedException();

			if (value.Type is ReferenceType refT)
				throw new NotImplementedException();
			else
			{
				var llvmValue = (value.Flags & ValueFlags.LValue) != 0
					? builder.BuildLoad(value.LlvmValue)
					: value.LlvmValue;
				
				ctx.Builder.BuildRet(llvmValue);
			}
			
			ctx.CurrentScope.SetReturnValue(value);
			return value;
		}
	}
}