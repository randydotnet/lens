﻿using System;
using System.Collections.Generic;
using Lens.Compiler;
using Lens.Translations;
using Lens.Utils;

namespace Lens.SyntaxTree.Expressions
{
	/// <summary>
	/// A node representing write access to a member of a type, field or property.
	/// </summary>
	internal class SetMemberNode : MemberNodeBase
	{
		private bool _IsStatic;
		private PropertyWrapper _Property;
		private FieldWrapper _Field;

		/// <summary>
		/// Value to be assigned.
		/// </summary>
		public NodeBase Value { get; set; }

		public override IEnumerable<NodeChild> GetChildren()
		{
			yield return new NodeChild(Expression, x => Expression = x);
			yield return new NodeChild(Value, x => Value = x);
		}

		protected override Type resolve(Context ctx, bool mustReturn)
		{
			resolve(ctx);
			return base.resolve(ctx, mustReturn);
		}

		private void resolve(Context ctx)
		{
			var type = StaticType != null
				? ctx.ResolveType(StaticType)
				: Expression.Resolve(ctx);

			checkTypeInSafeMode(ctx, type);

			// check for field
			try
			{
				_Field = ctx.ResolveField(type, MemberName);
				_IsStatic = _Field.IsStatic;
				if (Expression == null && !_IsStatic)
					error(CompilerMessages.DynamicMemberFromStaticContext, type, MemberName);

				return;
			}
			catch (KeyNotFoundException) { }

			try
			{
				_Property = ctx.ResolveProperty(type, MemberName);
				if(!_Property.CanSet)
					error(CompilerMessages.PropertyNoSetter, MemberName, type);

				_IsStatic = _Property.IsStatic;
				if (Expression == null && !_IsStatic)
					error(CompilerMessages.DynamicMemberFromStaticContext, MemberName, type);
			}
			catch (KeyNotFoundException)
			{
				error(CompilerMessages.TypeSettableIdentifierNotFound, type, MemberName);
			}
		}

		protected override void emitCode(Context ctx, bool mustReturn)
		{
			var gen = ctx.CurrentMethod.Generator;

			var destType = _Field != null ? _Field.FieldType : _Property.PropertyType;
			var valType = Value.Resolve(ctx);

			ctx.CheckTypedExpression(Value, valType, true);

			if (!destType.IsExtendablyAssignableFrom(valType))
				error(CompilerMessages.ImplicitCastImpossible, valType, destType);

			if (!_IsStatic)
			{
				var exprType = Expression.Resolve(ctx);
				if (Expression is IPointerProvider && exprType.IsStruct())
					(Expression as IPointerProvider).PointerRequired = true;

				Expression.Emit(ctx, true);
			}

			Expr.Cast(Value, destType).Emit(ctx, true);

			if (_Field != null)
				gen.EmitSaveField(_Field.FieldInfo);
			else
				gen.EmitCall(_Property.Setter, true);
		}

		#region Equality members

		protected bool Equals(SetMemberNode other)
		{
			return base.Equals(other) && Equals(Value, other.Value);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((SetMemberNode)obj);
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
			return string.Format("setmbr({0} of {1} = {2})", MemberName, Expression, Value);
		}
	}
}
