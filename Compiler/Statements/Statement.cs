using Leaf.Compilation.Exceptions;
using Leaf.Compilation.Grammar;
using Leaf.Compilation.Values;
using System;

namespace Leaf.Compilation.Statements
{
	public static partial class Statement
	{
		public static Value Compile(this LeafParser.StatementContext s, in LocalCompilationContext ctx)
		{
			try
			{
				var def = s.var_def();
				if (def != null) return def.Compile(in ctx);

				var ass = s.var_ass();
				if (ass != null) return ass.Compile(in ctx);

				var ret = s.@return();
				if (ret != null) return ret.Compile(in ctx);

				var call = s.function_call();
				if (call != null) return call.Compile(in ctx);

				var free = s.free();
				if (free != null) return free.Compile(in ctx);

				var @while = s.@while();
				if (@while != null) return @while.Compile(in ctx);
			}
			catch (CompilationException e) 
			{ throw new CompilationException("Cannot compile statement.", ctx.CurrentFragment, s.Start.Line, e); }

			throw new NotImplementedException();
		}
	}
}