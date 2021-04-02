using Leaf.Compilation.CompilationUnits;

namespace Leaf.Compilation.Exceptions
{
	public class ModuleNotFoundException : CompilationException
	{
		public ModuleNotFoundException(string name) 
			: base($"Module {name} does not exist.", null) {}
	}
}