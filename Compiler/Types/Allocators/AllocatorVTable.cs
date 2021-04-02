using System.Collections.Generic;
using LLVMSharp.Interop;
using System;

namespace Leaf.Compilation.Types.Allocators
{
	public class AllocatorVTable : StructType
	{
		public AllocatorVTable(GlobalCompilationContext ctx) 
			: base("__allocator_vtable", ctx.GlobalNamespace, CreateLlvmType(ctx), CreateMembers(ctx), TypeFlags.Unsafe) {}

		private static LLVMTypeRef CreateLlvmType(GlobalCompilationContext ctx)
		{
			var type = ctx.LlvmContext.CreateNamedStruct("__allocator_vtable");
			Span<LLVMTypeRef> members = stackalloc LLVMTypeRef[]
			{
				LLVMTypeRef.CreatePointer(LLVMTypeRef.CreateFunction(LLVMTypeRef.CreatePointer(LLVMTypeRef.Int8, 0), new[] {LLVMTypeRef.Int32}), 0),
				LLVMTypeRef.CreatePointer(LLVMTypeRef.CreateFunction(LLVMTypeRef.Void, new[] {LLVMTypeRef.CreatePointer(LLVMTypeRef.Int8, 0)}), 0),
			};
			
			type.StructSetBody(members, false);
			return type;
		}

		private static IReadOnlyDictionary<string, TypeMember> CreateMembers(GlobalCompilationContext ctx)
		{
			var global = ctx.GlobalNamespace;
			return new Dictionary<string, TypeMember>(2)
			{
				{
					"allocate_fn",
					new TypeMember
					{
						Index = 0,
						Name = "allocate_fn",
						Type = PointerType.Create(FunctionType.Create(global.Types["void*"], new[] {global.Types["u32"]}, false, ctx)),
					}
				},

				{
					"free_fn",
					new TypeMember
					{
						Index = 1,
						Name = "free_fn",
						Type = PointerType.Create(FunctionType.Create(global.Types["void"], new []{global.Types["void*"]}, false, ctx))
					}
				}
			};
		}
	}
}