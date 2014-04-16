﻿using System;
using System.Collections.Generic;
using Lens.Compiler;
using Lens.Resolver;
using Lens.Translations;
using Lens.Utils;

namespace Lens.SyntaxTree.Expressions
{
	/// <summary>
	/// A node representing assignment to an index.
	/// </summary>
	internal class SetIndexNode : IndexNodeBase
	{
		private MethodWrapper _Indexer;

		/// <summary>
		/// Value to be assigned.
		/// </summary>
		public NodeBase Value { get; set; }

		public override IEnumerable<NodeChild> GetChildren()
		{
			yield return new NodeChild(Expression, x => Expression = x);
			yield return new NodeChild(Index, x => Index = x);
			yield return new NodeChild(Value, x => Value = x);
		}

		protected override Type resolve(Context ctx, bool mustReturn)
		{
			var exprType = Expression.Resolve(ctx);
			var idxType = Index.Resolve(ctx);

			if (!exprType.IsArray)
			{
				try
				{
					_Indexer = ReflectionHelper.ResolveIndexer(exprType, idxType, false);
				}
				catch (LensCompilerException ex)
				{
					ex.BindToLocation(this);
					throw;
				}
			}

			var idxDestType = exprType.IsArray ? typeof (int) : _Indexer.ArgumentTypes[0];
			var valDestType = exprType.IsArray ? exprType.GetElementType() : _Indexer.ArgumentTypes[1];

			if(!idxDestType.IsExtendablyAssignableFrom(idxType))
				error(Index, CompilerMessages.ImplicitCastImpossible, idxType, idxDestType);

			ensureLambdaInferred(ctx, Value, valDestType);
			var valType = Value.Resolve(ctx);
			if (!valDestType.IsExtendablyAssignableFrom(valType))
				error(Value, CompilerMessages.ImplicitCastImpossible, valType, valDestType);

			return base.resolve(ctx, mustReturn);
		}

		protected override void emitCode(Context ctx, bool mustReturn)
		{
			if (_Indexer == null)
				compileArray(ctx);
			else
				compileCustom(ctx);
		}

		private void compileArray(Context ctx)
		{
			var gen = ctx.CurrentMethod.Generator;

			var exprType = Expression.Resolve(ctx);
			var itemType = exprType.GetElementType();

			Expression.Emit(ctx, true);
			Expr.Cast(Index, typeof (int)).Emit(ctx, true);
			Expr.Cast(Value, itemType).Emit(ctx, true);
			gen.EmitSaveIndex(itemType);
		}

		private void compileCustom(Context ctx)
		{
			var gen = ctx.CurrentMethod.Generator;

			try
			{
				var idxDest = _Indexer.ArgumentTypes[0];
				var valDest = _Indexer.ArgumentTypes[1];

				Expression.Emit(ctx, true);

				Expr.Cast(Index, idxDest).Emit(ctx, true);
				Expr.Cast(Value, valDest).Emit(ctx, true);

				gen.EmitCall(_Indexer.MethodInfo);
			}
			catch (LensCompilerException ex)
			{
				ex.BindToLocation(this);
				throw;
			}
		}

		#region Equality members

		protected bool Equals(SetIndexNode other)
		{
			return base.Equals(other) && Equals(Value, other.Value);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((SetIndexNode)obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (base.GetHashCode() * 397) ^ (Value != null ? Value.GetHashCode() : 0);
			}
		}

		#endregion

		public override string ToString()
		{
			return string.Format("setidx({0} of {1} = {2})", Index, Expression, Value);
		}
	}
}
