using Leaf.Compilation.CompilationUnits;
using System.Collections.Generic;
using Leaf.Compilation.Functions;
using Leaf.Compilation.Types;

namespace Leaf.Compilation.Exceptions
{
	public class FunctionCompilationException : CompilationException
	{
		public FunctionCompilationException(Function function, CompilationException? cause = null) 
			: base($"Could not compile function {function}.", function.Fragment, null, cause) {}
	}
	
	public class FunctionOverloadNotFoundException : CompilationException
	{
		public FunctionOverloadNotFoundException(OverloadGroup overloads, IReadOnlyList<Type> types, Fragment? fragment = null) 
			: base($"Function overload {overloads.Name}({string.Join(", ", types)}) does not exist.", fragment) {}
	}
	
	public class DuplicateFunctionOverloadException : CompilationException
	{
		public DuplicateFunctionOverloadException(OverloadGroup overloads, IReadOnlyList<Type> types, Fragment? fragment = null) 
			: base($"Duplicate definition of function {overloads}({string.Join(", ", types)}).", fragment) {}
	}
}