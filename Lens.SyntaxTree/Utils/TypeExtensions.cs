﻿using System;
using System.Linq;

namespace Lens.SyntaxTree.Utils
{
	/// <summary>
	/// A collection of helpful methods for types.
	/// </summary>
	internal static class TypeExtensions
	{
		static TypeExtensions()
		{
			SignedIntegerTypes = new[]
				                     {
					                     typeof (sbyte),
					                     typeof (short),
					                     typeof (int),
					                     typeof (long),
					                     typeof (decimal)
				                     };

			UnsignedIntegerTypes = new[]
				                       {
					                       typeof (byte),
					                       typeof (ushort),
					                       typeof (uint),
					                       typeof (ulong),
				                       };

			FloatTypes = new[]
				             {
					             typeof (float),
					             typeof (double)
				             };
		}

		public static Type[] SignedIntegerTypes { get; private set; }
		public static Type[] UnsignedIntegerTypes { get; private set; }
		public static Type[] FloatTypes { get; private set; }

		/// <summary>
		/// Checks if a type is a signed integer type.
		/// </summary>
		public static bool IsSignedIntegerType(this Type type)
		{
			return SignedIntegerTypes.Contains(type);
		}

		/// <summary>
		/// Checks if a type is an unsigned integer type.
		/// </summary>
		public static bool IsUnsignedIntegerType(this Type type)
		{
			return UnsignedIntegerTypes.Contains(type);
		}

		/// <summary>
		/// Checks if a type is a floating point type.
		/// </summary>
		public static bool IsFloatType(this Type type)
		{
			return FloatTypes.Contains(type);
		}

		/// <summary>
		/// Checks if a type is any of the numeric types.
		/// </summary>
		public static bool IsNumericType(this Type type)
		{
			return type.IsSignedIntegerType()
			       || type.IsFloatType()
			       || type.IsUnsignedIntegerType();
		}

		/// <summary>
		/// Checks if a variable of given type can be assigned from other type (including type extension).
		/// </summary>
		/// <returns></returns>
		public static bool IsExtendablyAssignableFrom(this Type varType, Type exprType)
		{
			return varType.DistanceFrom(exprType) < int.MaxValue;
		}

		/// <summary>
		/// Gets assignment type distance.
		/// </summary>
		public static int DistanceFrom(this Type varType, Type exprType)
		{
			throw new NotImplementedException();
		}
	}
}