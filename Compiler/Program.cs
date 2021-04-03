using Stopwatch = DotNetCoreUtilities.Structures.Stopwatch;
using DotNetCoreUtilities.Miscellaneous;
using Leaf.Compilation.CompilationUnits;
using DotNetCoreUtilities.Extensions;
using Leaf.Compilation.Exceptions;
using System.Diagnostics;
using LLVMSharp.Interop;
using CommandLine;
using System.Linq;
using System.IO;
using System;

namespace Leaf.Compilation
{
    class Program
    {
        static void Main(string[] args)
		{
            var options = Parser.Default.ParseArguments<CompilationOptions>(args)
                .MapResult(a => a, err => { Environment.Exit(-1); return default!; });

            var projectDir = new DirectoryInfo(options.SourceDirectory);
            if (!projectDir.Exists) throw new FileNotFoundException("The specified source directory does not exist.");

			var sw = Stopwatch.StartNew();
			
			Module rootModule;
			var context = new GlobalCompilationContext(options);

			try
			{
				rootModule = context.GetModule(context.Project.Name!);
				var main = rootModule.RootNamespace.Functions["main"].Functions[0].Compile(true);
			}
			catch (CompilationException e)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(e.Message);
				
				#if DEBUG
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine();
				Console.WriteLine(e);
				#endif
				
				return;
			}

			if (!rootModule.LlvmModule.TryVerify(LLVMVerifierFailureAction.LLVMReturnStatusAction, out var error))
			{
				rootModule.LlvmModule.Dump();
				Console.ForegroundColor = ConsoleColor.DarkRed;
				Console.WriteLine();
				Console.WriteLine(CompilerBugException.Goofed);
				Console.WriteLine($"\nLLVM Error:\n{error}");
				Console.ResetColor();
				return;
			}
			
			rootModule.OptimizeAll();
			if (options.Verbose)
			{
				Console.ForegroundColor = ConsoleColor.Cyan;
				rootModule.LlvmModule.Dump();
				Console.WriteLine($"\nTotal memory allocation: {GC.GetTotalAllocatedBytes().GetReadableByteCount()}");
				Console.WriteLine($"Total garbage collections: {(GC.CollectionCount(0), GC.CollectionCount(1), GC.CollectionCount(2))}");
			}

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"\nLLVM IR generation completed in {sw.ElapsedMilliseconds}ms");

			var modules = context.Modules.Values.ToArray();
			var filePaths = modules.ArraySelect(m => $"{options.SourceDirectory}.build/{m.Name}.ll");

			Directory.CreateDirectory($"{options.SourceDirectory}/.build/");
			
			for (var i = 0; i < modules.Length; i++)
				modules[i].LlvmModule.PrintToFile(filePaths[i]);

			var clangArgs = new ProcessStartInfo
			{
				FileName = "clang", 
				Arguments = $"{string.Join(' ', filePaths)} -O{(int)options.OptimizationLevel}"
			};

			if (options.Verbose)
			{
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine($"Starting clang with arguments: {clangArgs.Arguments}");
			}

			Console.ResetColor();
			using var clang = Process.Start(clangArgs);
			clang!.WaitForExit();

			if (clang.ExitCode != 0)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(CompilerBugException.Goofed);
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine($"Compilation completed in {sw.ElapsedMilliseconds}ms");
			}

			if (options.RunAfterCompilation && options.TargetFormat == TargetFormat.Executable)
			{
				Console.ForegroundColor = ConsoleColor.DarkBlue;
				Console.WriteLine("\nExecuting program:");
				var program = Process.Start("a.out");
				program.WaitForExit();
				Console.ForegroundColor = ConsoleColor.DarkBlue;
				Console.WriteLine($"Program exited with code: {program.ExitCode}");
			}
		}
    }
}