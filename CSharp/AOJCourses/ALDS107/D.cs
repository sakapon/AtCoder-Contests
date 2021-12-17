using System;
using System.Collections.Generic;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var preorder = Read();
		var inorder = Read();

		var postorder = new List<int>();

		Func<int, int, int, int> Dfs = null;
		Dfs = (i, l, r) =>
		{
			var v = preorder[i];
			var m = Array.IndexOf(inorder, v);

			if (l < m) i = Dfs(i + 1, l, m);
			if (m + 1 < r) i = Dfs(i + 1, m + 1, r);

			postorder.Add(v);
			return i;
		};
		Dfs(0, 0, n);

		Console.WriteLine(string.Join(" ", postorder));
	}
}
