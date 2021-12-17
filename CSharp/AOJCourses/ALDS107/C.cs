using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());

		var map = new int[n][];
		for (int i = 0; i < n; i++)
		{
			var a = Read();
			map[a[0]] = a.Skip(1).ToArray();
		}

		var root = Enumerable.Range(0, n).Except(map.SelectMany(vs => vs)).Single();

		var preorder = new List<int>();
		var inorder = new List<int>();
		var postorder = new List<int>();

		Action<int> Dfs = null;
		Dfs = v =>
		{
			preorder.Add(v);
			if (map[v][0] != -1) Dfs(map[v][0]);
			inorder.Add(v);
			if (map[v][1] != -1) Dfs(map[v][1]);
			postorder.Add(v);
		};
		Dfs(root);

		Console.WriteLine("Preorder");
		Console.WriteLine(" " + string.Join(" ", preorder));
		Console.WriteLine("Inorder");
		Console.WriteLine(" " + string.Join(" ", inorder));
		Console.WriteLine("Postorder");
		Console.WriteLine(" " + string.Join(" ", postorder));
	}
}
