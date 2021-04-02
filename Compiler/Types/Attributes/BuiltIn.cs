using Leaf.Compilation.CompilationUnits;
using System;
using LLVMSharp.Interop;

namespace Leaf.Compilation.Types.Attributes
{
	public abstract class EmptyAttributeType : AttributeType
	{
		protected EmptyAttributeType(string name, Namespace ns, Flags subsequentTypeFlags) 
			: base(name, ns, subsequentTypeFlags) {}

		protected override LLVMTypeRef Compile()
			=> LLVMTypeRef.CreateStruct(ReadOnlySpan<LLVMTypeRef>.Empty, false);
	}
	public class Unsafe : EmptyAttributeType
	{
		public Unsafe(GlobalCompilationContext ctx) 
			: base("Unsafe", ctx.GlobalNamespace, Compilation.Flags.Unsafe) {}
	}
	
	public class CCompat : EmptyAttributeType
	{
		public CCompat(GlobalCompilationContext ctx) 
			: base("CCompat", ctx.GlobalNamespace, Compilation.Flags.CCompat) {}
		
		public CCompat(GlobalCompilationContext ctx, string name, Flags subsequentFlags = Compilation.Flags.None) 
			: base(name, ctx.GlobalNamespace, subsequentFlags | Compilation.Flags.CCompat) {}
	}

	public class External : CCompat
	{
		public External(GlobalCompilationContext ctx) 
			: base(ctx, "External", Compilation.Flags.Unsafe) {}
	}
	
	public class VarArgs : EmptyAttributeType
	{
		public VarArgs(GlobalCompilationContext ctx) 
			: base("VarArg", ctx.GlobalNamespace, Compilation.Flags.VarArgs | Compilation.Flags.Unsafe) {}
	}
}