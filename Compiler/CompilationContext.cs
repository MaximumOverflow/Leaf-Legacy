using Module = Leaf.Compilation.CompilationUnits.Module;
using Type = Leaf.Compilation.Types.Type;
using Leaf.Compilation.Types.Attributes;
using Leaf.Compilation.CompilationUnits;
using Leaf.Compilation.Types.Allocators;
using DotNetCoreUtilities.Extensions;
using System.Collections.Concurrent;
using Leaf.Compilation.Exceptions;
using System.Collections.Generic;
using Leaf.Compilation.Functions;
using Leaf.Compilation.Types;
using LLVMSharp.Interop;
using Extensions;
using System.IO;
using System;

namespace Leaf.Compilation
{
    public sealed class GlobalCompilationContext
    {
        public readonly CompilationOptions Project;
        public readonly LLVMContextRef LlvmContext;
		public readonly Dictionary<string, Module> Modules;

		public readonly Allocator HeapAllocator;
		public readonly Allocator StackAllocator;
		public readonly AllocatorVTable AllocatorVTableType;
		
		public readonly Namespace GlobalNamespace;
		public readonly Dictionary<(Type, IReadOnlyList<Type>, bool), FunctionType> FuncTypes;

		public GlobalCompilationContext(CompilationOptions project)
		{
			Project = project;
			LlvmContext = LLVMContextRef.Global;
			Modules = new Dictionary<string, Module>();
			GlobalNamespace = new Namespace(null!, "", null!, this);
			FuncTypes = new Dictionary<(Type, IReadOnlyList<Type>, bool), FunctionType>(FunctionType.Comparer.Instance);

			GlobalNamespace.Types.Add("void",	new StructType("void",	GlobalNamespace, LLVMTypeRef.Void,  flags: TypeFlags.Primitive));
			GlobalNamespace.Types.Add("void*",	new PointerType("void", GlobalNamespace.Types["void"], LLVMTypeRef.CreatePointer(LLVMTypeRef.Int8, 0), TypeFlags.Unsafe));

			GlobalNamespace.Types.Add("i8",		new StructType("i8",	GlobalNamespace, LLVMTypeRef.Int8,  flags: TypeFlags.Primitive));
			GlobalNamespace.Types.Add("i16",	new StructType("i16",	GlobalNamespace, LLVMTypeRef.Int16, flags: TypeFlags.Primitive));
			GlobalNamespace.Types.Add("i32",	new StructType("i32",	GlobalNamespace, LLVMTypeRef.Int32, flags: TypeFlags.Primitive));
			GlobalNamespace.Types.Add("i64",	new StructType("i64",	GlobalNamespace, LLVMTypeRef.Int64, flags: TypeFlags.Primitive));
			GlobalNamespace.Types.Add("bool",	new StructType("bool",	GlobalNamespace, LLVMTypeRef.Int1,  flags: TypeFlags.Primitive));

			GlobalNamespace.Types.Add("f16",	new StructType("f16",	GlobalNamespace, LLVMTypeRef.Half,   flags: TypeFlags.Primitive));
			GlobalNamespace.Types.Add("f32",	new StructType("f32",	GlobalNamespace, LLVMTypeRef.Float,  flags: TypeFlags.Primitive));
			GlobalNamespace.Types.Add("f64",	new StructType("f64",	GlobalNamespace, LLVMTypeRef.Double, flags: TypeFlags.Primitive));
			
			GlobalNamespace.Types.Add("u8",		new StructType("u8",	GlobalNamespace, LLVMTypeRef.Int8,  flags: TypeFlags.Primitive | TypeFlags.Unsigned));
			GlobalNamespace.Types.Add("u16",	new StructType("u16",	GlobalNamespace, LLVMTypeRef.Int16, flags: TypeFlags.Primitive | TypeFlags.Unsigned));
			GlobalNamespace.Types.Add("u32",	new StructType("u32",	GlobalNamespace, LLVMTypeRef.Int32, flags: TypeFlags.Primitive | TypeFlags.Unsigned));
			GlobalNamespace.Types.Add("u64",	new StructType("u64",	GlobalNamespace, LLVMTypeRef.Int64, flags: TypeFlags.Primitive | TypeFlags.Unsigned));

			GlobalNamespace.Attributes.Add("Unsafe",		new Unsafe(this));
			GlobalNamespace.Attributes.Add("CCompat",		new CCompat(this));
			GlobalNamespace.Attributes.Add("VarArgs",		new VarArgs(this));
			GlobalNamespace.Attributes.Add("External",		new External(this));
			GlobalNamespace.Attributes.Add("Demangle",		GlobalNamespace.Attributes["CCompat"]);
			
			HeapAllocator = new HeapAllocator(this);
			StackAllocator = new StackAllocator(this);
			AllocatorVTableType = new AllocatorVTable(this);
			GlobalNamespace.Types.Add("__heap_allocator", HeapAllocator);
			GlobalNamespace.Types.Add("__stack_allocator", StackAllocator);
			GlobalNamespace.Types.Add("__allocator_vtable", AllocatorVTableType);
		}

		public Module GetModule(string name)
		{
			if(string.IsNullOrEmpty(name) || name == Project.Name)
				return Modules.TryGetValue("__root_module", out var mainModule) 
					? mainModule : AddModule(name);
			
			return Modules.TryGetValue(name, out var module) ? module : AddModule(name);
		}

		public Module AddModule(string name)
		{
			if (string.IsNullOrEmpty(name) || name == Project.Name)
			{
				var module = new Module(Project.Name ?? "__root_module", new DirectoryInfo(Project.SourceDirectory), this);
				Modules.Add(Project.Name ?? "", module);
				return module;
			}
			
			var path = new DirectoryInfo($"Libs/{name}");
			if (!path.Exists) throw new ModuleNotFoundException(name);

			var module2 = new Module(name, path, this);
			Modules.Add(name, module2);
			return module2;
		}
	}

	#nullable disable
	public readonly struct LocalCompilationContext : IDisposable
	{
		private static readonly ConcurrentBag<Stack<Scope>> ScopeStackPool = new();
		
		public readonly Fragment CurrentFragment;
		public readonly LLVMBuilderRef Builder;
		public readonly GlobalCompilationContext GlobalContext;
		
		public readonly Type CurrentType;
		public readonly Function CurrentFunction;
		public readonly Stack<Scope> ScopeStack;

		public LocalCompilationContext(GlobalCompilationContext globalContext, Function currentFunction)
		{
			CurrentType = null;
			GlobalContext = globalContext;
			CurrentFunction = currentFunction;
			CurrentFragment = currentFunction.Fragment;
			ScopeStack = ScopeStackPool.TakeOrCreate();
			Builder = GlobalContext.LlvmContext.CreateBuilder();
		}

		public Scope CurrentScope => ScopeStack.Peek();

		public Scope PushScope()
		{
			var scope = ScopeStack.IsNullOrEmpty()
				? new Scope(null, CurrentFunction)
				: new Scope(ScopeStack.Peek(), CurrentFunction);
			
			ScopeStack.Push(scope);
			return scope;
		}

		public void PopScope()
			=> ScopeStack.Pop();

		public void Dispose()
		{
			Builder.Dispose();
			foreach (var scope in ScopeStack)
				scope.Dispose();
			
			ScopeStack.Clear();
			ScopeStackPool.Add(ScopeStack);
		}
	}
	#nullable enable
}