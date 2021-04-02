using Leaf.Compilation.Exceptions;
using Leaf.Compilation.Values;
using LLVMSharp.Interop;
using System;
using Leaf.Compilation.CompilationUnits;
using Leaf.Compilation.Types.Attributes;

namespace Leaf.Compilation.Types.Allocators
{
	public sealed class HeapAllocator : Allocator
	{
		public HeapAllocator(GlobalCompilationContext ctx) 
			: base("Heap", ctx, ctx.GlobalNamespace) {}

		public override Value Allocate(Type type, bool mutable, in LocalCompilationContext ctx, string name = "")
		{
			LLVMValueRef llvmValue;
			
			switch (type)
			{
				case ReferenceType or FunctionType or AttributeType:
					throw new TypeInstantiationException(type, ctx.CurrentFragment);
				
				case StructType or PointerType:
					llvmValue = ctx.Builder.BuildMalloc(type, (ReadOnlySpan<char>) name);
					break;

				default: throw new NotImplementedException();
			}

			return new Value
			{
				Type = type,
				LlvmValue = llvmValue,
				Allocator = GetVTablePointer(ctx.CurrentFunction.Fragment.Module),
				Flags = ValueFlags.LValue | (mutable ? ValueFlags.Mutable : ValueFlags.None)
			};
		}

		public override void Free(Span<Value> args, in LocalCompilationContext ctx)
		{
			if (args.Length != 1)
				throw new CompilationException($"Free was supplied with an invalid number of parameters. Expected 1, received {args.Length}", ctx.CurrentFragment);

			var value = args[0];
			ctx.Builder.BuildFree(value.LlvmValue);
		}

		public override LLVMValueRef GetVTablePointer(Module module)
		{
			var vtt = CompilationContext.AllocatorVTableType;
			var vtp = module.LlvmModule.GetNamedGlobal(VtablePtrName);
			if (vtp != null) return vtp;

			vtp = module.LlvmModule.AddGlobal(CompilationContext.AllocatorVTableType, VtablePtrName);
			vtp.Initializer = LLVMValueRef.CreateConstNamedStruct(vtt, stackalloc LLVMValueRef[]
			{
				AllocateFn?.LlvmValue ?? GetMalloc(module), 
				FreeFn?.LlvmValue ?? GetFree(module)
			});
			
			return vtp;
		}

		private LLVMValueRef GetMalloc(Module module)
		{
			var malloc = module.LlvmModule.GetNamedFunction("malloc");
			if (malloc == null)
				malloc = module.LlvmModule.AddFunction("malloc",
					LLVMTypeRef.CreateFunction(LLVMTypeRef.CreatePointer(LLVMTypeRef.Int8, 0),
						stackalloc LLVMTypeRef[] {LLVMTypeRef.Int32}, false));

			return malloc;
		}
		
		private LLVMValueRef GetFree(Module module)
		{
			var free = module.LlvmModule.GetNamedFunction("free");
			if (free == null)
				free = module.LlvmModule.AddFunction("free",
					LLVMTypeRef.CreateFunction(LLVMTypeRef.Void, stackalloc LLVMTypeRef[] 
						{LLVMTypeRef.CreatePointer(LLVMTypeRef.Int8, 0)}, false));

			return free;
		}
	}
}