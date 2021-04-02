using DotNetCoreUtilities.Unsafe;
using Leaf.Compilation.Values;
using LLVMSharp.Interop;
using System;

namespace Leaf.Compilation.Types
{
	public readonly struct TypeMember
	{
		public Type Type { get; init; }
		public uint Index { get; init; }
		public string Name { get; init; }

		public TypeMember(uint index, string name, Type type)
			=> (Index, Name, Type) = (index, name, type);
	}

	[Flags]
	public enum TypeConversionFlags
	{
		None	 = 0b00,
		Implicit = 0b11,
		Explicit = 0b01,
	}

	public delegate Value TypeConversionDelegate(in Value value, in LLVMBuilderRef builder);
	
	public readonly struct TypeConversion
	{
		public readonly Type TargetType;
		public readonly TypeConversionFlags Flags;
		public readonly ValueFlags ResultingValueFlags;
		public readonly TypeConversionDelegate Delegate;

		private TypeConversion(Type targetType, TypeConversionFlags flags)
		{
			Flags = flags;
			TargetType = targetType;

			ResultingValueFlags = ValueFlags.None;
			if (targetType is ReferenceType refT)
			{
				ResultingValueFlags |= ValueFlags.LValue;
				ResultingValueFlags |= ValueFlags.Mutable; //TODO Implement immutable references
				if (refT.Base is FunctionType) ResultingValueFlags |= ValueFlags.Callable;
			}
			else if (targetType is FunctionType)  ResultingValueFlags |= ValueFlags.Callable;
			Delegate = null!;
		}

		public TypeConversion(Type targetType, TypeConversionFlags flags, LLVMValueRef func) 
		: this(targetType, flags)
		{
			Delegate = (in Value value, in LLVMBuilderRef builder) =>
			{
				var v = value.LlvmValue;
				var llvmValue = builder.BuildCall(func, v.AsSpan(), ReadOnlySpan<char>.Empty);

				return new Value
				{
					Type = targetType,
					LlvmValue = llvmValue,
				};
			};
		}

		public TypeConversion(Type targetType, TypeConversionFlags flags, TypeConversionDelegate @delegate) 
			: this(targetType, flags) => Delegate = @delegate;
	}
}