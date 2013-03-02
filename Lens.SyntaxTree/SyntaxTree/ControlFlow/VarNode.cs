﻿namespace Lens.SyntaxTree.SyntaxTree.ControlFlow
{
	/// <summary>
	/// The variable declaration node.
	/// </summary>
	public class VarNode : NameDeclarationNodeBase
	{
		public VarNode(string name = null) : base(name, false)
		{ }
	}
}
