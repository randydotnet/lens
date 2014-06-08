﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lens.Compiler;
using Lens.Resolver;
using Lens.Translations;
using Lens.Utils;

namespace Lens.SyntaxTree.ControlFlow
{
	internal class ForeachNode : NodeBase
	{
		/// <summary>
		/// A variable to assign current item to.
		/// </summary>
		public string VariableName { get; set; }

		/// <summary>
		/// Explicitly specified local variable.
		/// </summary>
		public Local Local { get; set; }

		/// <summary>
		/// A single expression of iterable type.
		/// Must be set to X if the loop is defined like (for A in X)
		/// </summary>
		public NodeBase IterableExpression { get; set; }

		/// <summary>
		/// The lower limit of loop range.
		/// Must be set to X if the loop is defined like (for A in X..Y)
		/// </summary>
		public NodeBase RangeStart { get; set; }

		/// <summary>
		/// The upper limit of loop range.
		/// Must be set to Y if the loop is defined like (for A in X..Y)
		/// </summary>
		public NodeBase RangeEnd { get; set; }

		public CodeBlockNode Body { get; set; }

		private Type _VariableType;
		private Type _EnumeratorType;
		private PropertyWrapper _CurrentProperty;

		protected override Type resolve(Context ctx, bool mustReturn)
		{
			if (IterableExpression != null)
				detectEnumerableType(ctx);
			else
				detectRangeType(ctx);

			if (VariableName != null && ctx.Scope.FindLocal(VariableName) != null)
				throw new LensCompilerException(string.Format(CompilerMessages.VariableDefined, VariableName));

			if (!mustReturn)
				return typeof (UnitType);

			if (Local == null)
			{
				// variable must be defined: declare it in a temporary scope for pre-resolve state
				var tmpVar = new Local(VariableName, _VariableType);
				return Scope.WithTempLocals(ctx, () => Body.Resolve(ctx), tmpVar);
			}

			// index local specified explicitly: no need to account for it in pre-resolve
			return Body.Resolve(ctx);
		}

		public override NodeBase Expand(Context ctx, bool mustReturn)
		{
			if (IterableExpression != null)
			{
				var type = IterableExpression.Resolve(ctx);
				if (type.IsArray)
					return expandArray(ctx);
				
				return expandEnumerable(ctx, mustReturn);
			}

			return expandRange(ctx);
		}

		public override IEnumerable<NodeChild> GetChildren()
		{
			if (IterableExpression != null)
			{
				yield return new NodeChild(IterableExpression, x => IterableExpression = x);
			}
			else
			{
				yield return new NodeChild(RangeStart, x => RangeStart = x);
				yield return new NodeChild(RangeEnd, x => RangeEnd = x);
			}

			yield return new NodeChild(Body, null);
		}

		private NodeBase expandEnumerable(Context ctx, bool mustReturn)
		{
			var iteratorVar = ctx.Scope.DeclareImplicit(ctx, _EnumeratorType, false);
			var enumerableType = _EnumeratorType.IsGenericType
				? typeof (IEnumerable<>).MakeGenericType(_EnumeratorType.GetGenericArguments()[0])
				: typeof (IEnumerable);

			var init = Expr.Set(
				iteratorVar,
				Expr.Invoke(
					Expr.Cast(IterableExpression, enumerableType),
					"GetEnumerator"
				)
			);

			var loop = Expr.While(
				Expr.Invoke(Expr.Get(iteratorVar), "MoveNext"),
				Expr.Block(
					getIndexAssignment(Expr.GetMember(Expr.Get(iteratorVar), "Current")),
					Body
				)
			);

			if (_EnumeratorType.Implements(typeof (IDisposable), false))
			{
				var dispose = Expr.Block(Expr.Invoke(Expr.Get(iteratorVar), "Dispose"));
				var returnType = Resolve(ctx);
				var saveLast = mustReturn && !returnType.IsVoid();

				if (saveLast)
				{
					var resultVar = ctx.Scope.DeclareImplicit(ctx, _EnumeratorType, false);
					return Expr.Block(
						Expr.Try(
							Expr.Block(
								init,
								Expr.Set(resultVar, loop)
							),
							dispose
						),
						Expr.Get(resultVar)
					);
				}

				return Expr.Try(
					Expr.Block(init, loop),
					dispose
				);
			}

			return Expr.Block(
				init,
				loop
			);
		}

		private NodeBase expandArray(Context ctx)
		{
			var arrayVar = ctx.Scope.DeclareImplicit(ctx, IterableExpression.Resolve(ctx), false);
			var idxVar = ctx.Scope.DeclareImplicit(ctx, typeof(int), false);
			var lenVar = ctx.Scope.DeclareImplicit(ctx, typeof(int), false);

			return Expr.Block(
				Expr.Set(idxVar, Expr.Int(0)),
				Expr.Set(arrayVar, IterableExpression),
				Expr.Set(lenVar, Expr.GetMember(Expr.Get(arrayVar), "Length")),
				Expr.While(
					Expr.Less(
						Expr.Get(idxVar),
						Expr.Get(lenVar)
					),
					Expr.Block(
						getIndexAssignment(
							Expr.GetIdx(
								Expr.Get(arrayVar),
								Expr.Get(idxVar)
							)
						),
						Expr.Set(
							idxVar,
							Expr.Add(Expr.Get(idxVar), Expr.Int(1))
						),
						Body
					)
				)
			);
		}

		private NodeBase expandRange(Context ctx)
		{
			var signVar = ctx.Scope.DeclareImplicit(ctx, _VariableType, false);
			var idxVar = ctx.Scope.DeclareImplicit(ctx, _VariableType, false);

			return Expr.Block(
				Expr.Set(idxVar, RangeStart),
				Expr.Set(
					signVar,
					Expr.Invoke(
						"Math",
						"Sign",
						Expr.Sub(RangeEnd, Expr.Get(idxVar))
					)
				),
				Expr.While(
					Expr.If(
						Expr.Equal(Expr.Get(signVar), Expr.Int(1)),
						Expr.Block(Expr.LessEqual(Expr.Get(idxVar), RangeEnd)),
						Expr.Block(Expr.GreaterEqual(Expr.Get(idxVar), RangeEnd))
					),
					Expr.Block(
						getIndexAssignment(Expr.Get(idxVar)),
						Body,
						Expr.Set(
							idxVar,
							Expr.Add(
								Expr.Get(idxVar),
								Expr.Get(signVar)
							)
						)
					)
				)
			);
		}

		private void detectEnumerableType(Context ctx)
		{
			var seqType = IterableExpression.Resolve(ctx);
			if (seqType.IsArray)
			{
				_VariableType = seqType.GetElementType();
				return;
			}
			
			var ifaces = seqType.ResolveInterfaces();
			if (seqType.IsInterface)
				ifaces = ifaces.Union(new[] {seqType}).ToArray();

			var generic = ifaces.FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof (IEnumerable<>));
			if (generic != null)
				_EnumeratorType = typeof (IEnumerator<>).MakeGenericType(generic.GetGenericArguments()[0]);

			else if (ifaces.Contains(typeof (IEnumerable)))
				_EnumeratorType = typeof (IEnumerator);

			else
				error(IterableExpression, CompilerMessages.TypeNotIterable, seqType);

			_CurrentProperty = ctx.ResolveProperty(_EnumeratorType, "Current");
			_VariableType = _CurrentProperty.PropertyType;
		}

		private void detectRangeType(Context ctx)
		{
			var t1 = RangeStart.Resolve(ctx);
			var t2 = RangeEnd.Resolve(ctx);

			if(t1 != t2)
				error(CompilerMessages.ForeachRangeTypeMismatch, t1, t2);

			if(!t1.IsIntegerType())
				error(CompilerMessages.ForeachRangeNotInteger, t1);

			_VariableType = t1;
		}

		private NodeBase getIndexAssignment(NodeBase indexGetter)
		{
			return Local == null
				? Expr.Let(VariableName, indexGetter)
				: Expr.Set(Local, indexGetter) as NodeBase;
		}

		#region Equality members

		protected bool Equals(ForeachNode other)
		{
			return string.Equals(VariableName, other.VariableName)
			       && Equals(IterableExpression, other.IterableExpression)
			       && Equals(RangeStart, other.RangeStart)
				   && Equals(RangeEnd, other.RangeEnd)
				   && Equals(Body, other.Body);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((ForeachNode)obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int hashCode = (VariableName != null ? VariableName.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (IterableExpression != null ? IterableExpression.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (RangeStart != null ? RangeStart.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (RangeEnd != null ? RangeEnd.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (Body != null ? Body.GetHashCode() : 0);
				return hashCode;
			}
		}

		#endregion
	}
}
