﻿using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Lens.Compiler;
using Lens.Resolver;
using Lens.Translations;
using Lens.Utils;

namespace Lens.SyntaxTree.PatternMatching.Rules
{
    /// <summary>
    /// Binds an expression to a name.
    /// </summary>
    internal class MatchNameRule : MatchRuleBase
    {
        #region Fields

        /// <summary>
        /// The desired name to bind to.
        /// </summary>
        public string Name;

        /// <summary>
        /// Expected type of the expression.
        /// </summary>
        public TypeSignature Type;

        /// <summary>
        /// Checks if the current name is prefixed with a "..." modifier in an array pattern.
        /// </summary>
        public bool IsArraySubsequence;

        /// <summary>
        /// Checks if the name is used as a placeholder.
        /// </summary>
        public bool IsWildcard => Name == "_";

        #endregion

        #region Resolve

        public override IEnumerable<PatternNameBinding> Resolve(Context ctx, Type expressionType)
        {
            if (!IsWildcard)
                yield return new PatternNameBinding(Name, expressionType);

            if (Type != null)
            {
                var specifiedType = ctx.ResolveType(Type);
                if (!specifiedType.IsExtendablyAssignableFrom(expressionType) && !expressionType.IsExtendablyAssignableFrom(specifiedType))
                    Error(CompilerMessages.PatternTypeMatchImpossible, specifiedType, expressionType);
            }
        }

        #endregion

        #region Transform

        public override IEnumerable<NodeBase> Expand(Context ctx, NodeBase expression, Label nextStatement)
        {
            if (Type != null)
            {
                yield return Expr.If(
                    Expr.Not(Expr.Is(expression, Type)),
                    Expr.Block(
                        Expr.JumpTo(nextStatement)
                    )
                );
            }

            if (!IsWildcard)
            {
                yield return Expr.Set(Name, expression);
            }
        }

        #endregion

        #region Equality members

        protected bool Equals(MatchNameRule other)
        {
            return string.Equals(Name, other.Name) && Equals(Type, other.Type) && IsArraySubsequence == other.IsArraySubsequence;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MatchNameRule) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Type != null ? Type.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ IsArraySubsequence.GetHashCode();
                return hashCode;
            }
        }

        #endregion
    }
}