using System;
using System.Collections.Generic;
using System.Linq;

class C2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var nodes = Enumerable.Range(0, n).Select(i => new Node { Id = i }).ToArray();
		for (int i = 0; i < n; i++)
		{
			var a = Read();
			if (a[1] != -1) nodes[a[0]].Left = nodes[a[1]];
			if (a[2] != -1) nodes[a[0]].Right = nodes[a[2]];
		}

		var root = nodes.Single(t => t.Parent == null);

		var preorder = new List<int>();
		var inorder = new List<int>();
		var postorder = new List<int>();
		root.Trace(preorder, inorder, postorder);

		Console.WriteLine("Preorder");
		Console.WriteLine(" " + string.Join(" ", preorder));
		Console.WriteLine("Inorder");
		Console.WriteLine(" " + string.Join(" ", inorder));
		Console.WriteLine("Postorder");
		Console.WriteLine(" " + string.Join(" ", postorder));
	}
}

[System.Diagnostics.DebuggerDisplay(@"\{{Id}\}")]
class Node
{
	public int Id;
	public Node Parent;

	Node _left;
	public Node Left
	{
		get { return _left; }
		set
		{
			_left = value;
			if (value != null) value.Parent = this;
		}
	}

	Node _right;
	public Node Right
	{
		get { return _right; }
		set
		{
			_right = value;
			if (value != null) value.Parent = this;
		}
	}

	public void Trace(List<int> preorder, List<int> inorder, List<int> postorder)
	{
		preorder.Add(Id);
		if (Left != null) Left.Trace(preorder, inorder, postorder);
		inorder.Add(Id);
		if (Right != null) Right.Trace(preorder, inorder, postorder);
		postorder.Add(Id);
	}
}
