using System;
using Antlr4.Runtime;
using Leaf.Compilation.Types;
using LLVMSharp.Interop;
using LTK = LLVMSharp.Interop.LLVMTypeKind;
using Type = Leaf.Compilation.Types.Type;

namespace Leaf.Compilation.Values
{
	[Flags]
	public enum ConversionFlags : byte
	{
		None = 0b00,
		Explicit = 0b01,
		Implicit = 0b11,
	}
	
	public readonly partial struct Value
	{
		public Value CastTo(Type type, in LocalCompilationContext ctx, bool @explicit = false, ParserRuleContext? node = null)
		{
			var builder = ctx.Builder;
			if (Type == type)
			{
				if ((Flags & ValueFlags.LValue) == 0 && type is not ReferenceType)
					return this;
				
				return Type switch
				{
					ReferenceType or LightReferenceType => new Value
					{
						Type = type,
						LlvmValue = builder.BuildLoad(LlvmValue)
					},
					
					_ => AsRValue(in ctx)
				};
			}

			switch (Type)
			{
				case ReferenceType refT when refT.Base.Equals(type): return new Value
				{
					Type = type,
					LlvmValue = builder.BuildLoad(builder.BuildLoad(builder.BuildStructGEP(LlvmValue, 0)))
				};
				
				case ReferenceType refT when type is LightReferenceType lrefT && lrefT.Base == refT.Base: return new Value
				{
					Type = type,
					LlvmValue = builder.BuildLoad(builder.BuildStructGEP(LlvmValue, 0))
				};
				
				case LightReferenceType refT when refT.Base.Equals(type): return new Value
				{
					Type = type,
					LlvmValue = builder.BuildLoad(LlvmValue)
				};

				default:
				{
					if (type is ReferenceType refT && refT.Base == Type)
						return CreateReference(this, in ctx, false);
					
					if (type is LightReferenceType lrefT && lrefT.Base == Type)
						return CreateReference(this, in ctx, true);

					if ((Type.Flags & TypeFlags.Primitive) != 0 && (Type.Flags & TypeFlags.Primitive) != 0)
					{
						var ltk0 = Type.LlvmType.Kind;
						var ltk1 = type.LlvmType.Kind;
						LLVMValueRef llvmValue;
						
						switch (ltk0)
						{
							case LTK.LLVMIntegerTypeKind when ltk1 == LLVMTypeKind.LLVMIntegerTypeKind:
							{
								llvmValue = builder.BuildIntCast(LlvmValue, type);
								if(!@explicit && type.LlvmType.IntWidth < Type.LlvmType.IntWidth)
									Warnings.RaiseImplicitCastWarning(Type, type, ctx.CurrentFragment, node);
								
								break;
							}
							
							default: throw new NotImplementedException($"{Type} -> {type}");
						}
						
						return new Value
						{
							Type = type,
							LlvmValue = llvmValue,
						};
					}

					throw new NotImplementedException($"{Type} -> {type}");
				}
			}
		}
	}
}