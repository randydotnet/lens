﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lens.Compiler;
using Lens.SyntaxTree.Literals;
using Lens.Translations;
using Lens.Utils;

namespace Lens.SyntaxTree.Expressions
{
	/// <summary>
	/// A node representing a new dictionary.
	/// </summary>
	internal class NewDictionaryNode : ValueListNodeBase<KeyValuePair<NodeBase, NodeBase>>, IEnumerable<KeyValuePair<NodeBase, NodeBase>>
	{
		private Type _KeyType;
		private Type _ValueType;

		protected override Type resolve(Context ctx, bool mustReturn)
		{
			if(Expressions.Count == 0)
				error(CompilerMessages.DictionaryEmpty);

			_KeyType = Expressions[0].Key.Resolve(ctx);
			_ValueType = resolveItemType(Expressions.Select(exp => exp.Value), ctx);

			if (_ValueType == typeof(NullType))
				error(Expressions[0].Value, CompilerMessages.DictionaryTypeUnknown);

			ctx.CheckTypedExpression(Expressions[0].Key, _KeyType);
			ctx.CheckTypedExpression(Expressions[0].Value, _ValueType, true);

			return typeof(Dictionary<,>).MakeGenericType(_KeyType, _ValueType);
		}

		public override IEnumerable<NodeChild> GetChildren()
		{
			for (var idx = 0; idx < Expressions.Count; idx++)
			{
				var id = idx;
				var curr = Expressions[idx];
				yield return new NodeChild(curr.Key, x => Expressions[id] = new KeyValuePair<NodeBase, NodeBase>(x, curr.Value));
				yield return new NodeChild(curr.Value, x => Expressions[id] = new KeyValuePair<NodeBase, NodeBase>(curr.Key, x));
			}
		}

		protected override void emitCode(Context ctx, bool mustReturn)
		{
			var gen = ctx.CurrentMethod.Generator;
			var dictType = Resolve(ctx);

			var tmpVar = ctx.Scope.DeclareImplicit(ctx, dictType, true);

			var ctor = ctx.ResolveConstructor(dictType, new[] {typeof (int)});
			var addMethod = ctx.ResolveMethod(dictType, "Add", new[] { _KeyType, _ValueType });

			var count = Expressions.Count;
			gen.EmitConstant(count);
			gen.EmitCreateObject(ctor.ConstructorInfo);
			gen.EmitSaveLocal(tmpVar.LocalBuilder);

			foreach (var curr in Expressions)
			{
				var currKeyType = curr.Key.Resolve(ctx);
				var currValType = curr.Value.Resolve(ctx);

				ctx.CheckTypedExpression(curr.Key, currKeyType);
				ctx.CheckTypedExpression(curr.Value, currValType, true);

				if (currKeyType != _KeyType)
					error(curr.Key, CompilerMessages.DictionaryKeyTypeMismatch, currKeyType, _KeyType, _ValueType);

				if (!_ValueType.IsExtendablyAssignableFrom(currValType))
					error(curr.Value, CompilerMessages.DictionaryValueTypeMismatch, currValType, _KeyType, _ValueType);

				gen.EmitLoadLocal(tmpVar.LocalBuilder);

				curr.Key.Emit(ctx, true);
				Expr.Cast(curr.Value, _ValueType).Emit(ctx, true);

				gen.EmitCall(addMethod.MethodInfo);
			}

			gen.EmitLoadLocal(tmpVar.LocalBuilder);
		}

		#region Equality members

		protected bool Equals(NewDictionaryNode other)
		{
			// KeyValuePair doesn't have Equals overridden, that's why it's so messy here:
			return Expressions.Select(e => e.Key).SequenceEqual(other.Expressions.Select(e => e.Key))
				   && Expressions.Select(e => e.Value).SequenceEqual(other.Expressions.Select(e => e.Value));
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((NewDictionaryNode)obj);
		}

		public override int GetHashCode()
		{
			return (Expressions != null ? Expressions.GetHashCode() : 0);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion

		public void Add(NodeBase key, NodeBase value)
		{
			Expressions.Add(new KeyValuePair<NodeBase, NodeBase>(key, value));
		}

		public IEnumerator<KeyValuePair<NodeBase, NodeBase>> GetEnumerator()
		{
			return Expressions.GetEnumerator();
		}

		public override string ToString()
		{
			return string.Format("dict({0})", string.Join(";", Expressions.Select(x => string.Format("{0} => {1}", x.Key, x.Value))));
		}
	}
}
