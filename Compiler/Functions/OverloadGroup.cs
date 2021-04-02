using Type = Leaf.Compilation.Types.Type;
using Leaf.Compilation.CompilationUnits;
using DotNetCoreUtilities.Extensions;
using Leaf.Compilation.Exceptions;
using System.Collections.Generic;
using Leaf.Compilation.Values;
using System.Linq;
using Extensions;

namespace Leaf.Compilation.Functions
{
	public sealed class OverloadGroup
	{
		public readonly string Name;
		public readonly Namespace Namespace;
		public readonly List<Function> Functions;

		public OverloadGroup(string name, Namespace ns)
		{
			Name = name;
			Namespace = ns;
			Functions = new List<Function>();
		}

		public Function GetImplementation(IReadOnlyList<Type> types)
		{
			var func = Functions.Find(f => types.SequenceEqual(f.Type.ParamTypes)
				|| (f.Flags & FunctionFlags.VarArgs) != 0 && Extensions.EnumerableExtensions.IncompleteSequenceEqual(f.Type.ParamTypes, types));
			
			if (func == null) 
				throw new FunctionOverloadNotFoundException(this, types);
			
			return func;
		}
		
		public Function GetImplementation(IReadOnlyList<Value> args)
		{
			var func = Functions.Find(f =>
			{
				return args.SequenceEqual(f.Type.ParamTypes, v => v.Type)
				|| (f.Flags & FunctionFlags.VarArgs) != 0 && f.Type.ParamTypes.IncompleteSequenceEqual(args, v => v.Type);
			});
			
			if (func == null) 
				throw new FunctionOverloadNotFoundException(this, args.ArraySelect(a => a.Type));
			
			return func;
		}

		public void AddImplementation(Function function)
		{
			if (Functions.Find(f => f.Type.ParamTypes.SequenceEqual(function.Type.ParamTypes)) != null)
				throw new DuplicateFunctionOverloadException(this, function.Type.ParamTypes, function.Fragment);
			
			Functions.Add(function);
		}

		public bool ImplementsOverload(IReadOnlyList<Type> types)
			=> Functions.Find(f => f.Type.ParamTypes.SequenceEqual(types)) != null;
	}
}