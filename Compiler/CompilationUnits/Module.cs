using Type = Leaf.Compilation.Types.Type;
using Leaf.Compilation.Types.Attributes;
using Leaf.Compilation.Optimization;
using Leaf.Compilation.Exceptions;
using System.Collections.Generic;
using Leaf.Compilation.Functions;
using Leaf.Compilation.Values;
using Leaf.Compilation.Types;
using LLVMSharp.Interop;
using System.IO;
using System;

namespace Leaf.Compilation.CompilationUnits
{
    public sealed class Module
    {
        public readonly string Name;
        public readonly Namespace RootNamespace;
        public readonly LLVMModuleRef LlvmModule;
        public readonly GlobalCompilationContext Context;
		public readonly Dictionary<LLVMValueRef, Value> Constants;
		public readonly Dictionary<string, Value> StrConstants;

		private readonly Dictionary<Function, Function> _foreignFunctions;

		public Module(string name, DirectoryInfo rootDirectory, GlobalCompilationContext context)
		{
			Name = name;
			Context = context;
			LlvmModule = LLVMModuleRef.CreateWithName(name);
			StrConstants = new Dictionary<string, Value>();
			Constants = new Dictionary<LLVMValueRef, Value>();
			RootNamespace = new Namespace(this, rootDirectory);

			_foreignFunctions = new Dictionary<Function, Function>();
		}
		
		public void EnumerateFragments() => RootNamespace.EnumerateFragments();


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

		public void EnsureLinkage(ref Function function)
		{
			if(function.Fragment.Module == this)
				return;

			if (_foreignFunctions.TryGetValue(function, out var fn))
			{
				function = fn;
				return;
			}

			fn = new Function(function, LlvmModule.AddFunction(function.GetMangledName(), function.Type.LlvmType));
			_foreignFunctions.Add(function, fn);
			function = fn;
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

		public Value GetConstCString(string text, in LocalCompilationContext ctx)
		{
			var builder = ctx.Builder;
			var type = PointerType.Create(Context.GlobalNamespace.Types["i8"]);

			Span<LLVMValueRef> indices = stackalloc LLVMValueRef[]
			{
				LLVMValueRef.CreateConstInt(LLVMTypeRef.Int64, 0),
				LLVMValueRef.CreateConstInt(LLVMTypeRef.Int64, 0),
			};
			
			if (StrConstants.TryGetValue(text, out var str))
				return new Value
				{
					Type = type, 
					Flags = ValueFlags.Constant,
					LlvmValue = builder.BuildGEP(str.LlvmValue, indices, "")
				};
			
			var llvmStr = Context.LlvmContext.GetConstString(text, false);
			var llvmValue = LlvmModule.AddGlobal(llvmStr.TypeOf, "");
			unsafe {LLVM.SetGlobalConstant(llvmValue, 1);}
			llvmValue.Initializer = llvmStr;
			
			StrConstants.Add(text, new Value
			{
				Type = type, 
				LlvmValue = llvmValue, 
				Flags = ValueFlags.LValue | ValueFlags.Constant
			});
			
			return new Value
			{
				Type = type,
				Flags = ValueFlags.Constant,
				LlvmValue = builder.BuildGEP(llvmValue, indices, "")
			};
		}

		public Value GetConstStruct(StructType type, Span<LLVMValueRef> values, in LocalCompilationContext ctx)
		{
			var builder = ctx.Builder;
			var constant = LLVMValueRef.CreateConstNamedStruct(type, values);

			if (Constants.TryGetValue(constant, out var value))
				return value;

			var llvmValue = LlvmModule.AddGlobal(type, "");
			unsafe {LLVM.SetGlobalConstant(llvmValue, 1);}
			llvmValue.Initializer = constant;

			var variable = new Value
			{
				Type = type,
				LlvmValue = llvmValue,
				Flags = ValueFlags.LValue | ValueFlags.Constant,
			};
			
			Constants.Add(constant, variable);
			return variable;
		}
	}
}