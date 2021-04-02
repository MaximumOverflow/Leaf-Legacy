using Type = Leaf.Compilation.Types.Type;
using Leaf.Compilation.Exceptions;
using Leaf.Compilation.Grammar;
using System;
using Leaf.Compilation.Functions;
using Leaf.Compilation.Types;
using StructType = Leaf.Compilation.Types.StructType;

namespace Leaf.Compilation.CompilationUnits
{
	public partial class Fragment
	{
		private void DefineType(LeafParser.Def_typeContext def, LeafParser.Attribute_addContext[]? attribs)
		{
			Type type;
			var name = def.Id().GetText();

			if (def.generic_def_list() != null)
				throw new NotImplementedException("Generics are not supported.");

			if (def.@struct() != null)
				type = new StructType(name, this, def.@struct(), attribs);
			else throw new NotImplementedException();

			if (!Namespace.Types.TryAdd(name, type))
				throw new DuplicateSymbolException(name, this, def.Start.Line);
		}

		private Function DefineFunction(
			string name, LeafParser.Function_declContext? decl, LeafParser.Function_implContext? impl, 
			LeafParser.Generic_def_listContext generics, LeafParser.Attribute_addContext[]? attribs
		)
		{
			if (generics != null)
				throw new NotImplementedException("Generics are not supported.");

			var func = decl != null
				? new Function(name, this, decl!, attribs)
				: new Function(name, this, impl!, attribs);

			if (func.Parameters.TryGetValue("this", out var par))
			{
				if (par.Index != 0)
					throw new CompilationException("Parameter 'this' must have an index of 0.", this, decl?.Start.Line ?? impl!.Start.Line);
				
				if(par.Type is not ReferenceType refT)
					throw new CompilationException("Parameter 'this' must be a reference type.", this, decl?.Start.Line ?? impl!.Start.Line);
				
				refT.Base.AddMethod(func);
			}

			return func;
		}

		private void DefineFunction(LeafParser.Def_funcContext def, LeafParser.Attribute_addContext[]? attribs)
		{
			var decl = def.function_decl();
			var impl = def.function_impl();
			var name = def.Id().GetText();

			try
			{
				var func = DefineFunction(name, decl, impl, def.generic_def_list(), attribs);

				if (!Namespace.Functions.TryGetValue(name, out var group))
				{
					group = new OverloadGroup(name, Namespace);
					Namespace.Functions.Add(name, group);
				}
				
				group.AddImplementation(func);
			}
			catch (CompilationException e)
			{ throw new CompilationException($"Could not compile function '{name}'.", this, def.Start.Line, e); }
		}

		private void DefineOperator(LeafParser.Def_operatorContext def, LeafParser.Attribute_addContext[]? attribs)
		{
			var impl = def.function_impl();
			var name = def.operator_id().GetText();
			throw new NotImplementedException();
		}
	}
}