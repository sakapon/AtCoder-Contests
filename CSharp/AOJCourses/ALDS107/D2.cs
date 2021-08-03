using System;
using System.Collections.Generic;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var preorder = Read();
		var inorder = Read();

		var i = 0;

		Func<int, int, Node> Dfs = null;
		Dfs = (l, r) =>
		{
			var v = preorder[i++];
			var m = Array.IndexOf(inorder, v);
			var node = new Node { Id = v };

			if (l < m) node.Left = Dfs(l, m);
			if (m + 1 < r) node.Right = Dfs(m + 1, r);

			return node;
		};
		var root = Dfs(0, n);

		var postorder = new List<int>();
		root.Trace(new List<int>(), new List<int>(), postorder);
		Console.WriteLine(string.Join(" ", postorder));
	}
}
