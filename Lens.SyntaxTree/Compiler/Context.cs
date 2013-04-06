﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using JetBrains.Annotations;
using Lens.SyntaxTree.SyntaxTree;
using Lens.SyntaxTree.SyntaxTree.ControlFlow;

namespace Lens.SyntaxTree.Compiler
{
	/// <summary>
	/// The main context class that stores information about currently compiled Assembly.
	/// </summary>
	public partial class Context
	{
		#region Constants

		/// <summary>
		/// The name of the main type in the assembly.
		/// </summary>
		private const string RootTypeName = "<ScriptRootType>";

		/// <summary>
		/// The name of the entry point of the assembly.
		/// </summary>
		private const string RootMethodName = "Run";

		/// <summary>
		/// The default size of a method's IL Generator stream.
		/// </summary>
		public const int ILStreamSize = 16384;

		#endregion

		public Context(LensCompilerOptions options = null)
		{
			var an = new AssemblyName(getAssemblyName());

			_TypeResolver = new TypeResolver();
			_TypeResolver.ExternalLookup = name =>
			{
				TypeEntity ent;
				_DefinedTypes.TryGetValue(name, out ent);
				return ent == null ? null : ent.TypeBuilder;
			};

			_DefinedTypes = new Dictionary<string, TypeEntity>();
			_DefinedProperties = new Dictionary<string, GlobalPropertyInfo>();

			Options = options ?? new LensCompilerOptions();
			var saveable = Options.AllowSave;
			if (saveable)
			{
				MainAssembly = AppDomain.CurrentDomain.DefineDynamicAssembly(an, AssemblyBuilderAccess.RunAndSave);
				MainModule = MainAssembly.DefineDynamicModule(an.Name, an.Name + ".dll");
			}
			else
			{
				MainAssembly = AppDomain.CurrentDomain.DefineDynamicAssembly(an, AssemblyBuilderAccess.Run);
				MainModule = MainAssembly.DefineDynamicModule(an.Name);
			}

			ContextId = GlobalPropertyHelper.RegisterContext();

			MainType = CreateType(RootTypeName);
			MainType.Kind = TypeEntityKind.Internal;
			MainType.Interfaces = new[] {typeof (IScript)};
			MainMethod = MainType.CreateMethod(RootMethodName, typeof(object), Type.EmptyTypes, false, true);
			MainMethod.ReturnType = typeof (object);
		}

		public IScript Compile(IEnumerable<NodeBase> nodes)
		{
			loadNodes(nodes);
			prepareEntities();

			createAutoEntities();
			prepareEntities();

			processClosures();
			prepareEntities();

			compileCore();
			finalizeAssembly();

			var inst = Activator.CreateInstance(ResolveType(RootTypeName));
			return inst as IScript;
		}

		/// <summary>
		/// Throws a new error.
		/// </summary>
		[ContractAnnotation("=> halt")]
		public void Error(string msg, params object[] args)
		{
			throw new LensCompilerException(string.Format(msg, args));
		}

		/// <summary>
		/// Throws a new error bound to a location.
		/// </summary>
		[ContractAnnotation("=> halt")]
		public void Error(LocationEntity ent, string msg, params object[] args)
		{
			throw new LensCompilerException(string.Format(msg, args), ent);
		}

		#region Properties

		/// <summary>
		/// Context ID for imported properties.
		/// </summary>
		public int ContextId { get; set; }

		/// <summary>
		/// Compiler options.
		/// </summary>
		internal LensCompilerOptions Options { get; private set; }

		/// <summary>
		/// The assembly that's being currently built.
		/// </summary>
		public AssemblyBuilder MainAssembly { get; private set; }

		/// <summary>
		/// The main module of the current assembly.
		/// </summary>
		public ModuleBuilder MainModule { get; private set; }

		/// <summary>
		/// The main type in which all "global" functions are stored.
		/// </summary>
		internal TypeEntity MainType { get; private set; }

		/// <summary>
		/// The function that is the body of the script.
		/// </summary>
		internal MethodEntity MainMethod { get; private set; }

		/// <summary>
		/// Type that is currently processed.
		/// </summary>
		internal TypeEntity CurrentType { get; set; }

		/// <summary>
		/// Method that is currently processed.
		/// </summary>
		internal MethodEntityBase CurrentMethod { get; set; }

		/// <summary>
		/// The current most nested try block.
		/// </summary>
		internal TryNode CurrentTryBlock
		{
			get { return CurrentMethod.CurrentTryBlock; }
			set { CurrentMethod.CurrentTryBlock = value; }
		}

		/// <summary>
		/// The current most nested catch block.
		/// </summary>
		internal CatchNode CurrentCatchBlock
		{
			get { return CurrentMethod.CurrentCatchBlock; }
			set { CurrentMethod.CurrentCatchBlock = value; }
		}

		/// <summary>
		/// The lexical scope of the current scope.
		/// </summary>
		internal Scope CurrentScope
		{
			get { return CurrentMethod == null ? null : CurrentMethod.Scope; }
		}

		/// <summary>
		/// Gets an IL Generator for current method.
		/// </summary>
		internal ILGenerator CurrentILGenerator
		{
			get { return CurrentMethod == null ? null : CurrentMethod.Generator; }
		}

		/// <summary>
		/// Root type.
		/// </summary>
		internal TypeEntity RootType
		{
			get { return _DefinedTypes[RootTypeName]; }
		}

		/// <summary>
		/// An ID for closure types.
		/// </summary>
		internal int ClosureId;

		#endregion

		#region Fields

		/// <summary>
		/// The counter that allows multiple assemblies.
		/// </summary>
		private static int _AssemblyId;

		/// <summary>
		/// A helper that resolves built-in .NET types by their string signatures.
		/// </summary>
		private readonly TypeResolver _TypeResolver;

		/// <summary>
		/// The root of type lookup.
		/// </summary>
		private readonly Dictionary<string, TypeEntity> _DefinedTypes;

		/// <summary>
		/// The lookup table for imported properties.
		/// </summary>
		private readonly Dictionary<string, GlobalPropertyInfo> _DefinedProperties;

		#endregion
	}
}
