using CommandLine;

namespace Leaf.Compilation
{
    public enum TargetArchitecture
    {
        UniversalBytecode,
        X86, Arm,
    }

    public enum TargetFormat
    {
        Executable,
        StaticLibrary,
        DynamicLibrary,
    }

    public enum OptimizationLevel
    {
        None,
        Basic,
        Moderate,
        Maximum,
    }

    public class CompilationOptions
    {
        [Option('n', "name", HelpText = "Specifies a custom name for the resulting binary.")]
        public string? Name { get; init; }
		
		#pragma warning disable 8618
		[Value(int.MaxValue, MetaName = "project", Required = true, HelpText = "Specifies the target project definition file.", Hidden = true)]
		public string SourceDirectory { get; init; }
		#pragma warning restore 8618
        
        [Option('f', "format", Default = TargetFormat.Executable, HelpText = "Specifies the result of the compilation.")]
        public TargetFormat TargetFormat { get; init; }
        
        [Option('o', "optimization", Default = OptimizationLevel.None, HelpText = "Specifies the optimization level to use.")]
        public OptimizationLevel OptimizationLevel { get; init; }
        
        [Option('t', "target", Default = TargetArchitecture.UniversalBytecode, HelpText = "Specifies the target architecture.")]
        public TargetArchitecture TargetArchitecture { get; init; }
		
		[Option('v', "verbose", Default = false, HelpText = "Prints the resulting LLVM IR and other diagnostic messages.")]
		public bool Verbose { get; init; }
		
		[Option('r', "run", Default = false, HelpText = "Runs the compiled program.")]
		public bool RunAfterCompilation { get; init; }
	}
}