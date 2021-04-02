using Type = Leaf.Compilation.Types.Type;
using Leaf.Compilation.Exceptions;
using Leaf.Compilation.Grammar;
using Leaf.Compilation.Values;
using Leaf.Compilation.Types;
using System;

namespace Leaf.Compilation.Reflection.Static
{
	public static class TypeInfo
	{
		public enum ResultType
		{
			Value, Type,
		}
		
		public static Value QueryValue(Type t, string name, LeafParser.ValueContext[]? values, in LocalCompilationContext ctx)
		{
			if (values?.Length == 0)
			{
				var resultType = GetProperty(t, name, out var value, out var type);
				if (resultType == ResultType.Type) throw new CompilationException("Expression does not return a value.", null);
				return value!.Value;
			}

			throw new NotImplementedException();
		}

		public static Type QueryType(Type t, string name, LeafParser.ValueContext[]? values, in LocalCompilationContext? ctx = null)
		{
			if (values?.Length == 0)
			{
				var resultType = GetProperty(t, name, out var value, out var type);
				if (resultType == ResultType.Value) throw new CompilationException("Expression does not return a type.", null);
				return type!;
			}

			throw new NotImplementedException();
		}

		public static ResultType GetProperty(Type t, string name, out Value? value, out Type? type)
		{
			var ctx = t.Namespace.Context;
			
			switch (name)
			{
				case "size":
				{
					type = null;
					value = new Value
					{
						Flags = ValueFlags.Constant,
						LlvmValue = t.LlvmType.SizeOf,
						Type = ctx.GlobalNamespace.Types["i64"],
					};
					return ResultType.Value;
				}
				
				case "alignment": 
				{
					type = null;
					value = new Value
					{
						Flags = ValueFlags.Constant,
						LlvmValue = t.LlvmType.AlignOf,
						Type = ctx.GlobalNamespace.Types["i64"],
					};
					return ResultType.Value;
				}

				case "base":
				{
					value = null;
					type = t switch
					{
						PointerType ptrT => ptrT.Base,
						ReferenceType refT => refT.Base,
						_ => throw new CompilationException($"Cannot get base type of '{t}'.", null)
					};

					return ResultType.Type;
				}
			}

			throw new CompilationException($"Invalid static reflection property '{name}'.", null);
		}

		public static Value Call(Type type, string name, ReadOnlySpan<Value> values)
		{
			throw new NotImplementedException();
		}
 	}
}