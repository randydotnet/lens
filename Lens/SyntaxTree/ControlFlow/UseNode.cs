﻿using Lens.Compiler;

namespace Lens.SyntaxTree.ControlFlow
{
    /// <summary>
    /// A namespace reference declaration.
    /// 
    /// This node is for parser only.
    /// </summary>
    internal class UseNode : NodeBase
    {
        #region Fields

        /// <summary>
        /// Namespace to be resolved.
        /// </summary>
        public string Namespace { get; set; }

        #endregion

        #region Emit

        protected override void EmitInternal(Context ctx, bool mustReturn)
        {
            // does nothing
            // all UseNodes are processed by Context.CreateFromNodes()
        }

        #endregion

        #region Debug

        protected bool Equals(UseNode other)
        {
            return string.Equals(Namespace, other.Namespace);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((UseNode) obj);
        }

        public override int GetHashCode()
        {
            return (Namespace != null ? Namespace.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return $"use({Namespace})";
        }

        #endregion
    }
}