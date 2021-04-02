using Leaf.Compilation.CompilationUnits;
using Type = Leaf.Compilation.Types.Type;

namespace Leaf.Compilation.Exceptions
{
    public class TypeCompilationException : CompilationException
    {
        public TypeCompilationException(Type type, int? line = null, CompilationException? cause = null) 
            : base($"Could not compile type '{type}'.", type.Fragment, line, cause) {}
    }
	
	public class TypeNotFoundException : CompilationException
	{
		public TypeNotFoundException(string type, Fragment? fragment, int? line = null, CompilationException? cause = null) 
			: base($"Could not find type '{type}'.", fragment, line, cause) {}
	}
	
	public class InvalidTypeException : CompilationException
	{
		public InvalidTypeException(Type type, Fragment? fragment, int? line = null, CompilationException? cause = null) 
			: base($"Type '{type}' is not valid in the current context.", fragment, line, cause) {}
	}
	
	public class TypeInstantiationException : CompilationException
	{
		public TypeInstantiationException(Type type, Fragment? fragment, int? line = null, CompilationException? cause = null) 
			: base($"Type '{type}' cannot be insantiated.", fragment, line, cause) {}
	}
}