using ParamCtx = Leaf.Compilation.Grammar.LeafParser.Parameter_defContext;
using AttrCtx = Leaf.Compilation.Grammar.LeafParser.Attribute_addContext;
using TypeCtx = Leaf.Compilation.Grammar.LeafParser.TypeContext;
using Type = Leaf.Compilation.Types.Type;
using Leaf.Compilation.Types.Attributes;
using Leaf.Compilation.CompilationUnits;
using DotNetCoreUtilities.Extensions;
using System.Collections.Immutable;
using Leaf.Compilation.Exceptions;
using Leaf.Compilation.Statements;
using System.Collections.Generic;
using Leaf.Compilation.Grammar;
using Leaf.Compilation.Values;
using Leaf.Compilation.Types;
using LLVMSharp.Interop;
using Extensions;
using System;

namespace Leaf.Compilation.Functions
{
	[Flags]
	public enum FunctionFlags
	{
		None		= Flags.None,
		Unsafe		= Flags.Unsafe,
		VarArgs		= Flags.VarArgs,
		CCompat		= Flags.CCompat,
		External	= Flags.External,
		MemberFunc	= Flags.MemberFunc,
	}

	public struct FunctionParameter
	{
		public Type Type { get; init; }
		public uint Index { get; init; }
		public string Name { get; init; }
		public ValueFlags Flags { get; init; }
	}

	public class Function : ICallable, IAttributeTarget
	{
		public FunctionType Type;
		public readonly string Name;
		public readonly Fragment Fragment;
		public FunctionFlags Flags { get; private set; }
		public readonly IReadOnlyDictionary<AttributeType, Value> Attributes;
		public readonly IReadOnlyDictionary<string, FunctionParameter> Parameters;

		private LLVMValueRef? _llvmValueRef;
		private LeafParser.Function_scopeContext? _scope;
		
		public Type ReturnType => Type.ReturnType;
		public bool IsCompiled => _llvmValueRef.HasValue;
		public AttributeTarget AttributeTargetType => AttributeTarget.StaticMethod;

		private Function(string name, Fragment fragment, TypeCtx ret, ParamCtx[] par, AttrCtx[]? attribs)
		{
			Name = name;
			_scope = null!;
			Fragment = fragment;
			Parameters = GetParameters(fragment, par);
			Attributes = ImmutableDictionary<AttributeType, Value>.Empty;

			if (!attribs.IsNullOrEmpty())
			{
				Attributes = AttributeType.GetAttributes(this, fragment, attribs);
				foreach (var attrib in Attributes.Keys)
					Flags |= (FunctionFlags) attrib.SubsequentFlags;
			}
			
			var retT = fragment.GetType(ret);
			var parT = Parameters.ArraySelect(p => p.Type);
			Type = FunctionType.Create(retT, parT, (Flags & FunctionFlags.VarArgs) != 0, fragment.Namespace.Module.Context);
		}
		
		public Function(string name, Fragment fragment, LeafParser.Function_implContext def, AttrCtx[]? attribs)
		: this(name, fragment, def.type(), def.parameter_def(), attribs)
		{
			_scope = def.function_scope();
		}


		/// <summary>A copy constructor used exclusively for linkage.</summary>
		public Function(Function fn, LLVMValueRef llvmValue)
		{
			Type = fn.Type;
			Name = fn.Name;
			Flags = fn.Flags;
			_scope = fn._scope;
			Fragment = fn.Fragment;
			Attributes = fn.Attributes;
			Parameters = fn.Parameters;
			_llvmValueRef = fn.LlvmValue;
			_llvmValueRef = llvmValue;
		}

		public Function(string name, Fragment fragment, LeafParser.Function_declContext def, AttrCtx[]? attribs)
		: this(name, fragment, def.type(), def.parameter_def(), attribs)
		{
			_llvmValueRef = fragment.Namespace.Module.LlvmModule.AddFunction(Name, Type);
			_scope = null!;
			
			var ctx = fragment.Module.Context;
			var global = ctx.GlobalNamespace;
			if ((Flags & FunctionFlags.External) == 0)
				throw new CompilationException("Function not marked as external.", fragment, def.Start.Line);
		}

		public LLVMValueRef LlvmValue
		{
			get
			{
				if (_llvmValueRef.HasValue)
					return _llvmValueRef.Value;

				_llvmValueRef = Compile();
				return _llvmValueRef.Value;
			}
		}
		
		public LLVMValueRef Compile(bool compileAsEntryPoint = false)
		{
			if (_llvmValueRef.HasValue) return _llvmValueRef.Value;
			
			var module = Fragment.Namespace.Module;
			var context = Fragment.Namespace.Context;
			
			var name = compileAsEntryPoint 
				? "main" 
				: (Flags & FunctionFlags.CCompat) == 0 ? GetMangledName() : Name;
			
			_llvmValueRef = module.LlvmModule.AddFunction(name, Type);

			using var localContext = new LocalCompilationContext(context, this);
			var builder = localContext.Builder;
			var scope = localContext.PushScope();
			builder.PositionAtEnd(scope.LlvmBlock);
			
			foreach (var param in Parameters.Values)
			{
				if (param.Type is LightReferenceType refT)
				{
					scope.Variables.Add(param.Name, new Value
					{
						Type = refT.Base,
						Flags = ValueFlags.LValue,
						LlvmValue = _llvmValueRef.Value.Params[param.Index],
					});
					
					continue;
				}
				
				var llvmValue = builder.BuildAlloca(param.Type);
				builder.BuildStore(_llvmValueRef.Value.Params[param.Index], llvmValue);
				
				var variable = new Value
				{
					Type = param.Type,
					LlvmValue = llvmValue,
					Flags = param.Flags | ValueFlags.Parameter,
					Allocator = param.Type is ReferenceType
						? builder.BuildLoad(builder.BuildStructGEP(llvmValue, 1))
						: default,
				};
				
				scope.Variables.Add(param.Name, variable);
			}

			if (_scope!.value() != null) throw new NotImplementedException();
			var statements = _scope.statement();

			try
			{
				foreach (var statement in statements)
					statement.Compile(in localContext);
				
				//TODO Check return type
				if (!scope.ReturnValue.HasValue)
				{
					if (ReturnType.LlvmType.Kind == LLVMTypeKind.LLVMVoidTypeKind)
						builder.BuildRetVoid();
					else throw new CompilationException($"Function '{name}' does not return a value.", Fragment);
				}
			}
			catch (CompilationException e) { throw new FunctionCompilationException(this, e); }

			_scope = null!;
			return _llvmValueRef.Value;
		}

		private static IReadOnlyDictionary<string, FunctionParameter> GetParameters(Fragment frag, LeafParser.Parameter_defContext[] param)
		{
			var parameters = new Dictionary<string, FunctionParameter>(param.Length);
			foreach (var def in param)
			{
				var flags = ValueFlags.LValue;
				var type = frag.GetType(def.type());

				if (type is ReferenceType refT)
				{
					if (def.mut == null)
						type = refT.AsLightRef();
					else flags |= ValueFlags.Mutable;
				}
				else if (def.mut != null)
					throw new CompilationException("The 'mut' keyword is only applicable to reference types.", frag, def.Start.Line);
				else flags |= ValueFlags.Mutable;

				var par = new FunctionParameter
				{
					Type = type,
					Flags = flags,
					Name = def.Id().GetText(),
					Index = (uint) parameters.Count,
				};

				if (!parameters.TryAdd(par.Name, par))
					throw new DuplicateSymbolException(par.Name, frag, def.Start.Line);
			}

			return parameters;
		}
		
		public override string ToString()
			=> $"{Fragment.Namespace.FullName}.{Name}[{Type}]";
		
		public string GetMangledName() => _scope != null
			? $"function_{Fragment.Namespace.GetMangledName()}_{Name}_{Type.GetMangledName()}"
			: Name;

		public void MarkAsMemberFunc() 
			=> Flags |= FunctionFlags.MemberFunc;

		public static implicit operator Value(Function f) => new Value
		{
			Type = f.Type,
			LlvmValue = f.LlvmValue,
			Flags = ValueFlags.Callable,
		};
	}
}