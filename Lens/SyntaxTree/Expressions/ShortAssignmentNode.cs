﻿using System;
using System.Collections.Generic;
using Lens.Compiler;
using Lens.Lexer;
using Lens.SyntaxTree.Literals;
using Lens.Utils;

namespace Lens.SyntaxTree.Expressions
{
	internal class ShortAssignmentNode : NodeBase
	{
		#region Constructor
		
		public ShortAssignmentNode(LexemType opType, NodeBase expr)
		{
			AssignmentOperator = _OperatorLookups[opType];
			Expression = expr;
		}

		#endregion

		#region Fields

		/// <summary>
		/// The kind of operator to use short assignment for.
		/// </summary>
		public readonly Func<NodeBase, NodeBase, NodeBase> AssignmentOperator;

		/// <summary>
		/// Assignment expression to expand.
		/// </summary>
		public NodeBase Expression;

		private readonly static Dictionary<LexemType, Func<NodeBase, NodeBase, NodeBase>> _OperatorLookups = new Dictionary<LexemType, Func<NodeBase, NodeBase, NodeBase>>
		{
			{ LexemType.And, Expr.And },
			{ LexemType.Or, Expr.Or },
			{ LexemType.Xor, Expr.Xor },
			{ LexemType.ShiftLeft, Expr.ShiftLeft },
			{ LexemType.ShiftRight, Expr.ShiftRight },
			{ LexemType.Plus, Expr.Add },
			{ LexemType.Minus, Expr.Sub },
			{ LexemType.Divide, Expr.Div },
			{ LexemType.Multiply, Expr.Mult },
			{ LexemType.Remainder, Expr.Mod },
			{ LexemType.Power, Expr.Pow },
		};

		#endregion

		#region NodeBase overrides

		protected override IEnumerable<NodeChild> getChildren()
		{
			yield return new NodeChild(Expression, x => Expression = x);
		}

		protected override NodeBase expand(Context ctx, bool mustReturn)
		{
			if (Expression is SetIdentifierNode)
				return expandIdentifier(Expression as SetIdentifierNode);

			if (Expression is SetMemberNode)
				return expandMember(ctx, Expression as SetMemberNode);

			if (Expression is SetIndexNode)
				return expandIndex(ctx, Expression as SetIndexNode);

			throw new InvalidOperationException("Invalid shorthand assignment expression!");
		}

		#endregion

		#region Expansion rules

		private NodeBase expandIdentifier(SetIdentifierNode node)
		{
			return Expr.Set(
				node.Identifier,
				AssignmentOperator(
					Expr.Get(node.Identifier),
					node.Value
				)
			);
		}

		private NodeBase expandMember(Context ctx, SetMemberNode node)
		{
			// type::name += value
			if (node.StaticType != null)
			{
				return Expr.SetMember(
					node.StaticType,
					node.MemberName,
					AssignmentOperator(
						Expr.GetMember(
							node.StaticType,
							node.MemberName
						),
						node.Value
					)
				);
			}

			// simple case: no need to cache expression
			if (node.Expression is SetIdentifierNode)
			{
				return Expr.SetMember(
					node.Expression,
					node.MemberName,
					AssignmentOperator(
						Expr.GetMember(
							node.Expression,
							node.MemberName
						),
						node.Value
					)
				);
			}

			// (x + y).name += value
			// must cache (x + y) to a local variable to prevent double execution
			var tmpVar = ctx.Scope.DeclareImplicit(ctx, node.Expression.Resolve(ctx), false);
			return Expr.Block(
				Expr.Set(tmpVar, node.Expression),
				Expr.SetMember(
					Expr.Get(tmpVar),
					node.MemberName,
					AssignmentOperator(
						Expr.GetMember(
							Expr.Get(tmpVar),
							node.MemberName
						),
						node.Value
					)
				)
			);
		}

		private NodeBase expandIndex(Context ctx, SetIndexNode node)
		{
			var body = Expr.Block();

			// must cache expression?
			if (!(node.Expression is GetIdentifierNode))
			{
				var tmpExpr = ctx.Scope.DeclareImplicit(ctx, node.Expression.Resolve(ctx), false);
				body.Add(Expr.Set(tmpExpr, node.Expression));
				node.Expression = Expr.Get(tmpExpr);
			}

			// must cache index?
			if (!(node.Index is GetIdentifierNode || node.Index is ILiteralNode || node.Index.IsConstant))
			{
				var tmpIdx = ctx.Scope.DeclareImplicit(ctx, node.Index.Resolve(ctx), false);
				body.Add(Expr.Set(tmpIdx, node.Index));
				node.Index = Expr.Get(tmpIdx);
			}

			body.Add(
				Expr.SetIdx(
					node.Expression,
					node.Index,
					AssignmentOperator(
						Expr.GetIdx(
							node.Expression,
							node.Index
						),
						node.Value
					)
				)
			);

			return body;
		}

		#endregion
	}
}
