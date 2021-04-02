using Leaf.Compilation.CompilationUnits;
using Leaf.Compilation.Types;

namespace Leaf.Compilation.Exceptions
{
	public class DuplicateSymbolException : CompilationException
	{
		public DuplicateSymbolException(string name, Fragment fragment, int? line = null, CompilationException? cause = null) 
			: base($"Symbol '{name}' already exists in the current scope.", fragment, line, cause) {}
	}
	
	public class SymbolNotFoundException : CompilationException
	{
		public SymbolNotFoundException(string name, Scope scope, int? line = null, CompilationException? cause = null) 
			: base($"Symbol '{name}' does not exist in the current scope.", scope.Function.Fragment, line, cause) {}
	}
	
	public class MemberNotFoundException : CompilationException
	{
		public MemberNotFoundException(string name, Type type, Scope scope, int? line = null, CompilationException? cause = null) 
			: base($"Type '{type}' does not define symbol '{name}'.", scope.Function.Fragment, line, cause) {}
	}
}