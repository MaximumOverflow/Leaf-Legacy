using System.Collections.Generic;
using LLVMSharp.Interop;
using System.Linq;
using System;
using DotNetCoreUtilities.Extensions;
using Leaf.Compilation.Exceptions;
using Leaf.Compilation.Types.Attributes;

namespace Leaf.Compilation.Types
{
	public class FunctionType : Type
	{
		public readonly Type ReturnType;
		public readonly Type[] ParamTypes;
		public override AttributeTarget AttributeTargetType => AttributeTarget.FunctionType;

		private FunctionType(LLVMTypeRef llvmType, Type returnType, Type[] paramTypes, GlobalCompilationContext ctx)
			: base(GetName(returnType, paramTypes), ctx.GlobalNamespace, llvmType)
			=> (ReturnType, ParamTypes) = (returnType, paramTypes);

		protected override LLVMTypeRef Compile()
			=> LlvmTypeRef!.Value;

		public static FunctionType Create(Type ret, Type[] par, bool varArg, GlobalCompilationContext ctx)
		{
			if (ctx.FuncTypes.TryGetValue((ret, par, varArg), out var type))
				return type;

			Span<LLVMTypeRef> paramSpan = stackalloc LLVMTypeRef[par.Length];
			for (var i = 0; i < par.Length; i++) paramSpan[i] = par[i];

			var llvmType = LLVMTypeRef.CreateFunction(ret, paramSpan, varArg);
			type = new FunctionType(llvmType, ret, par, ctx);
			
			ctx.FuncTypes.Add((ret, par, varArg), type);
			return type;
		}

		private static string GetName(Type ret, IEnumerable<Type> par)
			=> $"({string.Join(", ", par)})->{ret}";

		public override string GetMangledName()
			=> $"{string.Join('_', ParamTypes.ArraySelect(p => p.GetMangledName()))}_{ReturnType.GetMangledName()}";

		public class Comparer : IEqualityComparer<(Type, IReadOnlyList<Type>, bool)>
		{
			public static readonly Comparer Instance = new();
			
			public bool Equals((Type, IReadOnlyList<Type>, bool) x, (Type, IReadOnlyList<Type>, bool) y)
				=> ReferenceEquals(x.Item1, y.Item1) && x.Item2.SequenceEqual(y.Item2) && x.Item3 == y.Item3;

			public int GetHashCode((Type, IReadOnlyList<Type>, bool) obj)
			{
				var hash = new HashCode();
				
				hash.Add(obj.Item1);
				hash.Add(obj.Item3);
				foreach (var type in obj.Item2)
					hash.Add(type);

				return hash.ToHashCode();
			}
		}

		public override LLVMValueRef DefaultInitializer
			=> throw new InvalidTypeException(this, null);
	}
}