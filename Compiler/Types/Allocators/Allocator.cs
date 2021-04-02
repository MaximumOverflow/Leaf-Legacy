using AttribDict = System.Collections.Generic.IReadOnlyDictionary<Leaf.Compilation.Types.Attributes.AttributeType, Leaf.Compilation.Values.Value>;
using AllocCtx = Leaf.Compilation.Grammar.LeafParser.AllocatorContext;
using Leaf.Compilation.CompilationUnits;
using Leaf.Compilation.Types.Attributes;
using Leaf.Compilation.Exceptions;
using Leaf.Compilation.Functions;
using Leaf.Compilation.Values;
using LLVMSharp.Interop;
using System;


namespace Leaf.Compilation.Types.Allocators
{
	public abstract class Allocator : StructType
	{
		public Function? FreeFn { get; private set; }
		public Function? AllocateFn { get; private set; }
		public override AttributeTarget AttributeTargetType => AttributeTarget.Attribute;

		protected readonly string VtablePtrName;
		protected readonly GlobalCompilationContext CompilationContext;

		protected Allocator(string name, GlobalCompilationContext ctx, Namespace ns) : base(name, ns)
		{
			CompilationContext = ctx;
			VtablePtrName = $"__vtable_{GetMangledName()}";
		}
		
		public sealed override string GetMangledName() => (Flags & TypeFlags.Primitive) == 0
			? $"allocator_{Namespace.GetMangledName()}_{Name}"
			: Name;

		public void SetVTable(Function allocate, Function free)
		{
			var mod = Namespace.Module;
			var ctx = mod.Context;
			var vtt = ctx.AllocatorVTableType;
			
			if (allocate.Type != vtt.Members["allocate_fn"].Type)
				throw new InvalidTypeException(allocate.Type, null);
			
			if (free.Type != vtt.Members["free_fn"].Type)
				throw new InvalidTypeException(free.Type, null);
		}

		public virtual LLVMValueRef GetVTablePointer(Module module)
		{
			var vtt = CompilationContext.AllocatorVTableType;
			var vtp = module.LlvmModule.GetNamedGlobal(VtablePtrName);
			if (vtp != null) return vtp;

			if (AllocateFn == null)
				throw new CompilationException($"Allocator '{this}' does not define function 'allocate'.", null);
			
			if (FreeFn == null)
				throw new CompilationException($"Allocator '{this}' does not define function 'free'.", null);
			
			vtp = module.LlvmModule.AddGlobal(CompilationContext.AllocatorVTableType, VtablePtrName);
			vtp.Initializer = LLVMValueRef.CreateConstNamedStruct(vtt, stackalloc LLVMValueRef[] 
				{AllocateFn.LlvmValue, FreeFn.LlvmValue});
			
			return vtp;
		}

		public abstract void Free(Span<Value> args, in LocalCompilationContext ctx);
		public abstract Value Allocate(Type type, bool mutable, in LocalCompilationContext ctx, string name = "");
	}
}