using System.Runtime.InteropServices;
using LLVMSharp.Interop;

namespace Leaf.ExecutionEngine
{
    public class Program
	{
		static void Main(string[] args)
        {
			LLVM.LinkInMCJIT();
			LLVM.InitializeX86TargetInfo();
			LLVM.InitializeX86Target();
			LLVM.InitializeX86TargetMC();
			LLVM.InitializeX86AsmParser();
			LLVM.InitializeX86AsmPrinter();
		}
    }
}