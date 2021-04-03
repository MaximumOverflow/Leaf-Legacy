using System;
using Type = Leaf.Compilation.Types.Type;
using Leaf.Compilation.CompilationUnits;
using DotNetCoreUtilities.Extensions;
using Leaf.Compilation.Exceptions;
using System.Collections.Generic;
using Leaf.Compilation.Values;
using System.Linq;
using Extensions;
using Leaf.Compilation.Types;

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

		public Function GetImplementation(Span<Type> types)
		{
			var func = FindMatch(types);
			if (func == null) throw new FunctionOverloadNotFoundException(this, types.ArraySelect(t => t));
			return func;
		}
		
		public Function GetImplementation(Span<Value> args)
		{
			var func = FindMatch(args);
			if (func == null) throw new FunctionOverloadNotFoundException(this, args.ArraySelect(a => a.Type));
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

		private Function? FindMatch(Span<Type> supplied)
		{
			foreach (var function in Functions)
				if (CompareTypes(function.Type.ParamTypes, supplied, (function.Flags & FunctionFlags.VarArgs) != 0))
					return function;

			return null;
		}
		
		private Function? FindMatch(Span<Value> supplied)
		{
			foreach (var function in Functions)
				if (CompareTypes(function.Type.ParamTypes, supplied, (function.Flags & FunctionFlags.VarArgs) != 0))
					return function;

			return null;
		}

		public static bool CompareTypes(Span<Type> expected, Span<Type> supplied, bool varArg)
			=> CompareTypes(expected, supplied, varArg, t => t);
		
		public static bool CompareTypes(Span<Type> expected, Span<Value> supplied, bool varArg)
			=> CompareTypes(expected, supplied, varArg, v => v.Type);
		
		private static bool CompareTypes<T>(Span<Type> expected, Span<T> supplied, bool varArg, Func<T, Type> select)
		{
			if (!varArg && expected.Length != supplied.Length)
				return false;

			if (supplied.Length < expected.Length)
				return false;

			for (var i = 0; i < expected.Length; i++)
			{
				var type = select(supplied[i]);
				var exp = expected[i];
				if(type.Equals(exp)) continue;

				switch (type)
				{
					case ReferenceType refT when exp is ReferenceType erefT:
						if (!refT.Base.Equals(erefT.Base)) return false; break;
					
					case ReferenceType refT when exp is LightReferenceType erefT:
						if (!refT.Base.Equals(erefT.Base)) return false; break;
						
					case LightReferenceType refT when exp is LightReferenceType erefT:
						if (!refT.Base.Equals(erefT.Base)) return false; break;
					
					case ReferenceType refT:
						if (!refT.Base.Equals(exp)) throw new NotImplementedException(); break;
					
					case LightReferenceType refT:
						if (!refT.Base.Equals(exp)) throw new NotImplementedException(); break;

					default:
					{
						if(exp is ReferenceType refT && refT.Base.Equals(type))
							continue;
						
						if(exp is LightReferenceType lrefT && lrefT.Base.Equals(type))
							continue;
						
						return false;
					}
				}
			}

			return true;
		}
	}
}