using DotNetCoreUtilities.Extensions;
using System.Collections.Generic;
using Leaf.Compilation.Exceptions;
using Leaf.Compilation.Functions;
using Leaf.Compilation.Types;
using System.Linq;
using System.IO;
using Leaf.Compilation.Types.Attributes;

namespace Leaf.Compilation.CompilationUnits
{
    public sealed class Namespace
	{
		private bool _enumerated;
        public readonly string Name;
        public readonly Module Module;
        public readonly Namespace? Parent;
		public readonly DirectoryInfo? Directory;
		public readonly GlobalCompilationContext Context;
		
		public readonly Dictionary<string, Type> Types;
		public readonly Dictionary<string, Namespace> Children;
		public readonly Dictionary<string, AttributeType> Attributes;
		public readonly Dictionary<string, OverloadGroup> Functions;

		public Namespace(Module module, string name, DirectoryInfo? directory)
		{
			Name = name;
			Module = module;
			Directory = directory;
			Context = module?.Context!;
			Types = new Dictionary<string, Type>();
			Children = new Dictionary<string, Namespace>();
			Attributes = new Dictionary<string, AttributeType>();
			Functions = new Dictionary<string, OverloadGroup>();
		}

		public Namespace(Namespace? parent, string name, DirectoryInfo? directory, GlobalCompilationContext ctx) 
		: this(parent?.Module!, name, directory)
		{
			Context = ctx;
			Parent = parent;
			parent?.Children.Add(name, this);
		}
		
		public Namespace(Module module, DirectoryInfo directory) 
			: this(module, module.Name, directory) {}

		public void EnumerateFragments()
		{
			if(_enumerated) return;
			_enumerated = true;
			
			if (Directory == null) return;
			var files = Directory.EnumerateFiles("*.leaf").ToArray();
			var fragments = files.ArraySelect(f => new Fragment(f, this));

			foreach (var fragment in fragments)
			{
				try { fragment.Compile(); }
				catch (CompilationException e)
				{
					var message = $"Could not create namespace {FullName}.";
					throw new CompilationException(message, fragment, cause: e);
				}
			}
			
		}

        public string FullName => Parent != null
            ? $"{Parent.FullName}.{Name}"
            : Name;
		
		public string GetMangledName() => !string.IsNullOrEmpty(Parent?.Name)
			? $"{Parent.GetMangledName()}_{Name}"
			: Name;
	}
}