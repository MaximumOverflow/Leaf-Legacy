using LLVMSharp.Interop;
using System;

namespace Leaf.Compilation.Optimization
{
	public static class Optimizer
	{
		public static LLVMPassManagerRef CreatePassManager(LLVMModuleRef module, OptFlags flags)
		{
			var pass = module.CreateFunctionPassManager();
			
			if((flags & OptFlags.Reassociate) != 0)
				pass.AddReassociatePass();
			
			if((flags & OptFlags.Vectorize) != 0)
				pass.AddLoopVectorizePass();
			
			if((flags & OptFlags.CombineInstructions) != 0)
				pass.AddInstructionCombiningPass();
			
			if((flags & OptFlags.RotateLoops) != 0)
				pass.AddLoopRotatePass();
			
			if((flags & OptFlags.UnrollLoops) != 0)
				pass.AddLoopUnrollPass();
			
			if((flags & OptFlags.UnswitchLoops) != 0)
				pass.AddLoopUnswitchPass();
			
			if((flags & OptFlags.CanonicalizeInductionVariables) != 0)
				pass.AddIndVarSimplifyPass();

			if ((flags & OptFlags.DeleteUnusedLoops) != 0)
				pass.AddLoopDeletionPass();
			
			if((flags & OptFlags.GlobalValueNumbering) != 0)
				pass.AddNewGVNPass();
			
			if((flags & OptFlags.OptimizeMemCpy) != 0)
				pass.AddMemCpyOptPass();
			
			if((flags & OptFlags.SimplifyControlFlowGraph) != 0)
				pass.AddCFGSimplificationPass();

			pass.InitializeFunctionPassManager();
			return pass;
		}
		
		public static LLVMPassManagerRef CreatePassManager(LLVMModuleRef module, OptimizationLevel optLevel)
		{
			var pass = module.CreateFunctionPassManager();

			switch (optLevel)
			{
				case OptimizationLevel.None: break;

				case OptimizationLevel.Basic:
				{
					pass.AddInstructionCombiningPass();
					pass.AddConstantPropagationPass();
					pass.AddCFGSimplificationPass();
					pass.AddIndVarSimplifyPass();
					pass.AddReassociatePass();
					pass.AddNewGVNPass();
					goto case OptimizationLevel.None;
				}
				
				case OptimizationLevel.Moderate:
				{
					pass.AddLoopUnrollPass();
					pass.AddLoopUnswitchPass();
					pass.AddTailCallEliminationPass();
					goto case OptimizationLevel.Basic;
				}
					
				case OptimizationLevel.Maximum:
				{
					pass.AddLoopVectorizePass();
					goto case OptimizationLevel.Moderate;
				}
					
				default: throw new ArgumentOutOfRangeException();
			}

			pass.InitializeFunctionPassManager();
			return pass;
		}
	}
}