using System;

namespace Leaf.Compilation.Optimization
{
	[Flags]
	public enum OptFlags : ushort
	{
		None = 0,
		
		/// <summary>
		/// This pass reassociates commutative expressions in an order that is designed to promote better constant propagation, GCSE, LICM, PRE, etc.
		/// </summary>
		Reassociate = 1 << 0,
		
		/// <summary>
		/// This pass implements a simple loop unroller.
		/// It works best when loops have been canonicalized by the <see cref="OptFlags.CanonicalizeInductionVariables"/> pass,
		/// allowing it to determine the trip counts of loops easily.
		/// </summary>
		UnrollLoops = 1 << 1,
		
		/// <summary>
		/// This pass transforms loops that contain branches on loop-invariant conditions to have multiple loops.
		/// </summary>
		UnswitchLoops = 1 << 2,
		
		/// <summary>
		/// A simple loop rotation transformation.
		/// </summary>
		RotateLoops = 1 << 3,
		
		/// <summary>
		/// This file implements the Dead Loop Deletion Pass.
		/// This pass is responsible for eliminating loops with non-infinite computable trip counts that have no side effects or volatile instructions,
		/// and do not contribute to the computation of the function’s return value.
		/// </summary>
		DeleteUnusedLoops = 1 << 4,
		
		/// <summary>
		/// Combine instructions to form fewer, simple instructions. This pass does not modify the CFG.
		/// This pass is where algebraic simplification happens.
		/// </summary>
		CombineInstructions = 1 << 5,
		
		/// <summary>
		/// This pass performs global value numbering to eliminate fully and partially redundant instructions.
		/// It also performs redundant load elimination.
		/// </summary>
		GlobalValueNumbering = 1 << 6,
		
		/// <summary>
		/// Performs dead code elimination and basic block merging.
		/// </summary>
		SimplifyControlFlowGraph = 1 << 7,

		/// <summary>
		/// This transformation analyzes and transforms the induction variables (and computations derived from them) into simpler forms suitable
		/// for subsequent analysis and transformation.
		/// </summary>
		CanonicalizeInductionVariables = 1 << 8,
		
		/// <summary>
		/// This pass performs various transformations related to eliminating memcpy calls, or transforming sets of stores into memsets.
		/// </summary>
		OptimizeMemCpy = 1 << 9,
		
		/// <summary>
		/// This pass combines instructions inside basic blocks to form vector instructions.
		/// It iterates over each basic block, attempting to pair compatible instructions,
		/// repeating this process until no additional pairs are selected for vectorization.
		/// When the outputs of some pair of compatible instructions are used as inputs by some other pair of compatible instructions,
		/// those pairs are part of a potential vectorization chain.
		/// Instruction pairs are only fused into vector instructions when they are part of a chain longer than some threshold length.
		/// Moreover, the pass attempts to find the best possible chain for each pair of compatible instructions.
		/// These heuristics are intended to prevent vectorization in cases where it would not yield a performance increase of the resulting code.
		/// </summary>
		Vectorize = 1 << 10,
	}
}