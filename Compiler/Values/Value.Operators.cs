using Leaf.Compilation.Grammar;
using Leaf.Compilation.Types;
using LLVMSharp.Interop;
using System;
using Leaf.Compilation.Exceptions;
using Leaf.Compilation.Statements;

namespace Leaf.Compilation.Values
{
	public enum Operator
	{
		Add, Sub,
		Mul, Div, Mod,
		
		Eq, Neq
	}

	public static class OperatorExtensions
	{
		public static string AsString(this Operator op) => op switch
		{
			Operator.Add => "+",
			Operator.Sub => "-",
			Operator.Mul => "*",
			Operator.Div => "/",
			Operator.Mod => "%",
			Operator.Eq => "==",
			Operator.Neq => "!=",
			_ => throw new ArgumentOutOfRangeException(nameof(op), op, null)
		};
	}
	
	public readonly partial struct Value
	{
		public static Value GetOp(LeafParser.ValueContext v0, LeafParser.ValueContext v1, Operator op,
		in LocalCompilationContext ctx, in ValueRetrievalOptions options)
		{
			var builder = ctx.Builder;
			var val0 = Get(v0, ctx, in options);
			var val1 = Get(v1, ctx, in options);
			
			var t0 = val0.Type switch
			{
				ReferenceType refT => refT.Base,
				LightReferenceType refT => refT.Base,
				_ => val0.Type,
			};
			
			var t1 = val1.Type switch
			{
				ReferenceType refT => refT.Base,
				LightReferenceType refT => refT.Base,
				_ => val1.Type,
			};

			if (t0.Methods.TryGetValue(op.AsString(), out var overloads))
			{
				var args = new[] { val0, val1};
				var func = overloads.GetImplementation(args);
				return Statement.CompileCall(func, args, in ctx);
			}

			if ((t0.Flags & TypeFlags.Primitive) != 0 && (t1.Flags & TypeFlags.Primitive) != 0)
			{
				val0 = val0.AsRValue(in ctx);
				val1 = val1.AsRValue(in ctx);

				if (val0.Type != val1.Type)
				{
					val1 = val1.CastTo(val0.Type, in ctx, node: v1);
				}
				
				switch (op)
				{
					case Operator.Add when val0.Type.LlvmType.Kind == LLVMTypeKind.LLVMIntegerTypeKind:
						return new Value
						{
							Type = val0.Type,
							Flags = (val0.Flags & ValueFlags.Constant) != 0 && (val1.Flags & ValueFlags.Constant) != 0 ? ValueFlags.Constant : ValueFlags.None,
							LlvmValue = (val0.Flags & ValueFlags.Constant) != 0 && (val1.Flags & ValueFlags.Constant) != 0
								? LLVMValueRef.CreateConstAdd(val0.LlvmValue, val1.LlvmValue) 
								: builder.BuildAdd(val0.LlvmValue, val1.LlvmValue)
						};
					
					case Operator.Add when val0.Type.LlvmType.Kind == LLVMTypeKind.LLVMFloatTypeKind:
						return new Value
						{
							Type = val0.Type,
							Flags = (val0.Flags & ValueFlags.Constant) != 0 && (val1.Flags & ValueFlags.Constant) != 0 ? ValueFlags.Constant : ValueFlags.None,
							LlvmValue = (val0.Flags & ValueFlags.Constant) != 0 && (val1.Flags & ValueFlags.Constant) != 0
								? LLVMValueRef.CreateConstFAdd(val0.LlvmValue, val1.LlvmValue) 
								: builder.BuildFAdd(val0.LlvmValue, val1.LlvmValue)
						};
					
					case Operator.Sub when val0.Type.LlvmType.Kind == LLVMTypeKind.LLVMIntegerTypeKind:
						return new Value
						{
							Type = val0.Type,
							Flags = (val0.Flags & ValueFlags.Constant) != 0 && (val1.Flags & ValueFlags.Constant) != 0 ? ValueFlags.Constant : ValueFlags.None,
							LlvmValue = (val0.Flags & ValueFlags.Constant) != 0 && (val1.Flags & ValueFlags.Constant) != 0
								? LLVMValueRef.CreateConstSub(val0.LlvmValue, val1.LlvmValue) 
								: builder.BuildSub(val0.LlvmValue, val1.LlvmValue)
						};
					
					case Operator.Sub when val0.Type.LlvmType.Kind == LLVMTypeKind.LLVMFloatTypeKind:
						return new Value
						{
							Type = val0.Type,
							Flags = (val0.Flags & ValueFlags.Constant) != 0 && (val1.Flags & ValueFlags.Constant) != 0 ? ValueFlags.Constant : ValueFlags.None,
							LlvmValue = (val0.Flags & ValueFlags.Constant) != 0 && (val1.Flags & ValueFlags.Constant) != 0
								? LLVMValueRef.CreateConstFSub(val0.LlvmValue, val1.LlvmValue) 
								: builder.BuildFSub(val0.LlvmValue, val1.LlvmValue)
						};
					
					case Operator.Mul when val0.Type.LlvmType.Kind == LLVMTypeKind.LLVMIntegerTypeKind:
						return new Value
						{
							Type = val0.Type,
							Flags = (val0.Flags & ValueFlags.Constant) != 0 && (val1.Flags & ValueFlags.Constant) != 0 ? ValueFlags.Constant : ValueFlags.None,
							LlvmValue = (val0.Flags & ValueFlags.Constant) != 0 && (val1.Flags & ValueFlags.Constant) != 0
								? LLVMValueRef.CreateConstMul(val0.LlvmValue, val1.LlvmValue) 
								: builder.BuildMul(val0.LlvmValue, val1.LlvmValue)
						};
					
					case Operator.Mul when val0.Type.LlvmType.Kind == LLVMTypeKind.LLVMFloatTypeKind:
						return new Value
						{
							Type = val0.Type,
							Flags = (val0.Flags & ValueFlags.Constant) != 0 && (val1.Flags & ValueFlags.Constant) != 0 ? ValueFlags.Constant : ValueFlags.None,
							LlvmValue = (val0.Flags & ValueFlags.Constant) != 0 && (val1.Flags & ValueFlags.Constant) != 0
								? LLVMValueRef.CreateConstFAdd(val0.LlvmValue, val1.LlvmValue) 
								: builder.BuildFAdd(val0.LlvmValue, val1.LlvmValue)
						};
					
					case Operator.Div when val0.Type.LlvmType.Kind == LLVMTypeKind.LLVMIntegerTypeKind && (val0.Type.Flags & TypeFlags.Unsigned) != 0:
						return new Value
						{
							Type = val0.Type,
							Flags = (val0.Flags & ValueFlags.Constant) != 0 && (val1.Flags & ValueFlags.Constant) != 0 ? ValueFlags.Constant : ValueFlags.None,
							LlvmValue = (val0.Flags & ValueFlags.Constant) != 0 && (val1.Flags & ValueFlags.Constant) != 0
								? LLVMValueRef.CreateConstUDiv(val0.LlvmValue, val1.LlvmValue) 
								: builder.BuildUDiv(val0.LlvmValue, val1.LlvmValue)
						};
					
					case Operator.Div when val0.Type.LlvmType.Kind == LLVMTypeKind.LLVMIntegerTypeKind && (val0.Type.Flags & TypeFlags.Unsigned) == 0:
						return new Value
						{
							Type = val0.Type,
							Flags = (val0.Flags & ValueFlags.Constant) != 0 && (val1.Flags & ValueFlags.Constant) != 0 ? ValueFlags.Constant : ValueFlags.None,
							LlvmValue = (val0.Flags & ValueFlags.Constant) != 0 && (val1.Flags & ValueFlags.Constant) != 0
								? LLVMValueRef.CreateConstSDiv(val0.LlvmValue, val1.LlvmValue) 
								: builder.BuildSDiv(val0.LlvmValue, val1.LlvmValue)
						};
					
					case Operator.Div when val0.Type.LlvmType.Kind == LLVMTypeKind.LLVMFloatTypeKind:
						return new Value
						{
							Type = val0.Type,
							Flags = (val0.Flags & ValueFlags.Constant) != 0 && (val1.Flags & ValueFlags.Constant) != 0 ? ValueFlags.Constant : ValueFlags.None,
							LlvmValue = (val0.Flags & ValueFlags.Constant) != 0 && (val1.Flags & ValueFlags.Constant) != 0
								? LLVMValueRef.CreateConstFDiv(val0.LlvmValue, val1.LlvmValue) 
								: builder.BuildFDiv(val0.LlvmValue, val1.LlvmValue)
						};
					
					case Operator.Mod when val0.Type.LlvmType.Kind == LLVMTypeKind.LLVMIntegerTypeKind && (val0.Type.Flags & TypeFlags.Unsigned) != 0:
						return new Value
						{
							Type = val0.Type,
							Flags = (val0.Flags & ValueFlags.Constant) != 0 && (val1.Flags & ValueFlags.Constant) != 0 ? ValueFlags.Constant : ValueFlags.None,
							LlvmValue = (val0.Flags & ValueFlags.Constant) != 0 && (val1.Flags & ValueFlags.Constant) != 0
								? LLVMValueRef.CreateConstURem(val0.LlvmValue, val1.LlvmValue) 
								: builder.BuildURem(val0.LlvmValue, val1.LlvmValue)
						};
					
					case Operator.Mod when val0.Type.LlvmType.Kind == LLVMTypeKind.LLVMIntegerTypeKind && (val0.Type.Flags & TypeFlags.Unsigned) == 0:
						return new Value
						{
							Type = val0.Type,
							Flags = (val0.Flags & ValueFlags.Constant) != 0 && (val1.Flags & ValueFlags.Constant) != 0 ? ValueFlags.Constant : ValueFlags.None,
							LlvmValue = (val0.Flags & ValueFlags.Constant) != 0 && (val1.Flags & ValueFlags.Constant) != 0
								? LLVMValueRef.CreateConstSRem(val0.LlvmValue, val1.LlvmValue) 
								: builder.BuildSRem(val0.LlvmValue, val1.LlvmValue)
						};
					
					case Operator.Mod when val0.Type.LlvmType.Kind == LLVMTypeKind.LLVMFloatTypeKind:
						return new Value
						{
							Type = val0.Type,
							Flags = (val0.Flags & ValueFlags.Constant) != 0 && (val1.Flags & ValueFlags.Constant) != 0 ? ValueFlags.Constant : ValueFlags.None,
							LlvmValue = (val0.Flags & ValueFlags.Constant) != 0 && (val1.Flags & ValueFlags.Constant) != 0
								? LLVMValueRef.CreateConstFRem(val0.LlvmValue, val1.LlvmValue) 
								: builder.BuildFRem(val0.LlvmValue, val1.LlvmValue)
						};
					
					case Operator.Eq when val0.Type.LlvmType.Kind == LLVMTypeKind.LLVMIntegerTypeKind:
						return new Value
						{
							Type = ctx.GlobalContext.GlobalNamespace.Types["bool"],
							Flags = (val0.Flags & ValueFlags.Constant) != 0 && (val1.Flags & ValueFlags.Constant) != 0 ? ValueFlags.Constant : ValueFlags.None,
							LlvmValue = (val0.Flags & ValueFlags.Constant) != 0 && (val1.Flags & ValueFlags.Constant) != 0
								? LLVMValueRef.CreateConstInt(LLVMTypeRef.Int1, val0.LlvmValue == val1.LlvmValue ? 1u : 0u) 
								: builder.BuildICmp(LLVMIntPredicate.LLVMIntEQ, val0.LlvmValue, val1.LlvmValue)
						};
					
					case Operator.Neq when val0.Type.LlvmType.Kind == LLVMTypeKind.LLVMIntegerTypeKind:
						return new Value
						{
							Type = ctx.GlobalContext.GlobalNamespace.Types["bool"],
							Flags = (val0.Flags & ValueFlags.Constant) != 0 && (val1.Flags & ValueFlags.Constant) != 0 ? ValueFlags.Constant : ValueFlags.None,
							LlvmValue = (val0.Flags & ValueFlags.Constant) != 0 && (val1.Flags & ValueFlags.Constant) != 0
								? LLVMValueRef.CreateConstInt(LLVMTypeRef.Int1, val0.LlvmValue != val1.LlvmValue ? 1u : 0u) 
								: builder.BuildICmp(LLVMIntPredicate.LLVMIntNE, val0.LlvmValue, val1.LlvmValue)
						};
				}
			}

			throw new CompilationException($"Operator {op.AsString()} not defined for types '{val0.Type}', '{val1.Type}'.", ctx.CurrentFragment, v0.Start.Line);
		}

	}
}