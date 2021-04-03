using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using DotNetCoreUtilities.Extensions;
using LLVMSharp.Interop;
using Function = Leaf.Compilation.Functions.Function;
using Value = Leaf.Compilation.Values.Value;

namespace Leaf.Compilation.CompilationUnits
{
	public sealed class Scope : IDisposable
	{
		private static readonly ConcurrentBag<Dictionary<string, Value>> VarDictPool = new();
		
		public readonly Scope? Parent;
		public readonly Function Function;
		public readonly Dictionary<string, Value> Variables;

		public Value? ReturnValue { get; private set; }
		public LLVMBasicBlockRef LlvmBlock { get; private set; }
		public Namespace Namespace => Function.Fragment.Namespace;

		public Scope(Scope? parent, Function function, string blockName = "")
		{
			Parent = parent;
			Function = function;
			LlvmBlock = AppendLlvmBlock(blockName);
			Variables = VarDictPool.TakeOrCreate();
		}
		
		public LLVMBasicBlockRef AppendLlvmBlock(string name = "")
			=> LlvmBlock = Function.LlvmValue.AppendBasicBlock(name);

		public bool SetReturnValue(in Value value)
		{
			if (ReturnValue.HasValue)
				return false;

			ReturnValue = value;
			return true;
		}

		public void Dispose()
		{
			Variables.Clear();
			VarDictPool.Add(Variables);
		}
	}
}