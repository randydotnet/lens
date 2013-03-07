﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Lens.SyntaxTree.Utils;

namespace Lens.SyntaxTree.Compiler
{
	/// <summary>
	/// Finds a list of possible extension methods for a given type.
	/// </summary>
	public static class ExtensionMethodResolver
	{
		static ExtensionMethodResolver()
		{
			_Cache = new Dictionary<Type, Dictionary<string, List<MethodInfo>>>();
		}

		/// <summary>
		/// List of found extension methods
		/// </summary>
		private static readonly Dictionary<Type, Dictionary<string, List<MethodInfo>>> _Cache;

		/// <summary>
		/// Gets an extension method by given arguments.
		/// </summary>
		public static MethodInfo FindExtensionMethod(this Type type, string name, Type[] args)
		{
			if (!_Cache.ContainsKey(type))
				findMethodsForType(type);

			var cache = _Cache[type];
			var methods = cache.ContainsKey(name) ? cache[name] : new List<MethodInfo>();

			var result = methods.Where(m => m.Name == name)
								.Select(mi => new { Method = mi, Distance = GetArgumentsDistance(mi.GetParameters().Skip(1).Select(p => p.ParameterType).ToArray(), args) })
								.OrderBy(p => p.Distance)
								.ToArray();

			if (result.Length == 0 || result[0].Distance == int.MaxValue)
				throw new KeyNotFoundException("No suitable method was found!");

			if (result.Length > 2)
			{
				var ambiCount = result.Skip(1).TakeWhile(i => i.Distance == result[0].Distance).Count();
				if (ambiCount > 0)
					throw new AmbiguousMatchException();
			}

			return result[0].Method;
		}

		private static void findMethodsForType(Type type)
		{
			var dict = new Dictionary<string, List<MethodInfo>>();

			var asms = AppDomain.CurrentDomain.GetAssemblies();
			foreach (var asm in asms)
			{
				var types = asm.GetTypes();
				var exms = from currType in types
				           where currType.IsSealed && !currType.IsNested && currType.IsDefined(typeof (ExtensionAttribute), false)
				           from method in currType.GetMethods(BindingFlags.Static | BindingFlags.Public)
				           where method.IsDefined(typeof (ExtensionAttribute), false) && method.GetParameters()[0].ParameterType.IsExtendablyAssignableFrom(type)
				           select method;

				foreach (var curr in exms)
				{
					if (dict.ContainsKey(curr.Name))
						dict[curr.Name].Add(curr);
					else
						dict[curr.Name] = new List<MethodInfo> {curr};
				}
			}

			_Cache[type] = dict;
		}

		/// <summary>
		/// Gets total distance between two sets of argument types.
		/// </summary>
		public static int GetArgumentsDistance(Type[] src, Type[] dst)
		{
			if (src.Length != dst.Length)
				return int.MaxValue;

			var sum = 0;
			for (var idx = 0; idx < src.Length; idx++)
			{
				var currDist = dst[idx].DistanceFrom(src[idx]);
				if (currDist == int.MaxValue)
					return int.MaxValue;
				sum += currDist;
			}

			return sum;
		}
	}
}
