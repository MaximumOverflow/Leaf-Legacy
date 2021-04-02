using System;
using Leaf.Compilation.Optimization;
using System.Collections.Generic;
using Leaf.Compilation.Values;
using LLVMSharp.Interop;
using System.IO;
using Leaf.Compilation.Exceptions;
using Leaf.Compilation.Functions;
using Leaf.Compilation.Types;
using Leaf.Compilation.Types.Attributes;
using Type = Leaf.Compilation.Types.Type;

namespace Leaf.Compilation.CompilationUnits
{
    public sealed class Module
    {
        public readonly string Name;
        public readonly Namespace RootNamespace;
        public readonly LLVMModuleRef LlvmModule;
        public readonly GlobalCompilationContext Context;
		public readonly Dictionary<string, Value> Constants;

		private readonly HashSet<Type> _foreignTypes;
		private readonly HashSet<Function> _foreignFunctions;

		public Module(string name, DirectoryInfo rootDirectory, GlobalCompilationContext context)
		{
			Name = name;
			Context = context;
			LlvmModule = LLVMModuleRef.CreateWithName(name);
			Constants = new Dictionary<string, Value>();
			RootNamespace = new Namespace(this, rootDirectory);

			_foreignTypes = new HashSet<Type>();
			_foreignFunctions = new HashSet<Function>();

			RootNamespace.EnumerateFragments();
		}

		public void OptimizeAll()
		{
			var pass = Optimizer.CreatePassManager(LlvmModule, Context.Project.OptimizationLevel);
			OptimizeAll(pass, RootNamespace);
		}

		private void OptimizeAll(LLVMPassManagerRef pass, Namespace ns)
		{
			foreach (var overloads in ns.Functions.Values)
			foreach (var function in overloads.Functions)
			{
				if (function.IsCompiled)
					pass.RunFunctionPassManager(function.LlvmValue);
			}

			foreach (var child in ns.Children.Values)
				OptimizeAll(pass, child);
		}

		public void EnsureLinkage(Function function)
		{
			if(function.Fragment.Module == this)
				return;
			
			if(_foreignFunctions.Contains(function))
				return;

			_foreignFunctions.Add(function);
			LlvmModule.AddFunction(function.GetMangledName(), function.Type.LlvmType);
		}

		public Value CreateAttributeSingleton(AttributeType type)
		{
			if (type.Members.Count != 0) throw new CompilerBugException();

			var name = "singleton_" + type.GetMangledName();
			var attr = LlvmModule.GetNamedGlobal(name);
			if (attr != null) goto Ret;

			attr = LlvmModule.AddGlobal(type, name);
			attr.Initializer = LLVMValueRef.CreateConstNamedStruct(type, ReadOnlySpan<LLVMValueRef>.Empty);
			
			Ret:
			return new Value
			{
				Type = type,
				LlvmValue = attr,
			};
		}
	}
}