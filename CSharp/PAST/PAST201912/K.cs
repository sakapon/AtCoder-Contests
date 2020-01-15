using System;
using System.Collections.Generic;
using System.Linq;

class K
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var p = new int[n].Select(_ => int.Parse(Console.ReadLine())).ToList();
		p.Insert(0, 0);
		var s = new int[int.Parse(Console.ReadLine())].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray());

		var root = 0;
		var map = new int[n + 1].Select(_ => new List<int>()).ToArray();
		for (int i = 1; i <= n; i++)
		{
			if (p[i] == -1) root = i;
			else map[p[i]].Add(i);
		}

		var d = new int[n + 1];
		var parents = new int[n + 1].Select(_ => new List<int>()).ToArray();
		var stack = new List<int>();
		Dfs(root, map, d, parents, stack);

		var r = new List<bool>();
		foreach (var q in s)
		{
			if (d[q[0]] <= d[q[1]]) { r.Add(false); continue; }

			int a = q[0], b = q[1];
			while (d[a] > d[b])
				a = parents[a][(int)Math.Log(d[a] - d[b], 2)];
			r.Add(a == b);
		}
		Console.WriteLine(string.Join("\n", r.Select(x => x ? "Yes" : "No")));
	}

	static int[] p2 = Enumerable.Range(0, 20).Select(i => (int)Math.Pow(2, i)).ToArray();

	static void Dfs(int p, List<int>[] map, int[] d, List<int>[] parents, List<int> stack)
	{
		d[p] = stack.Count;
		for (int i = 0; p2[i] <= stack.Count; i++)
			parents[p].Add(stack[stack.Count - p2[i]]);

		stack.Add(p);
		foreach (var np in map[p])
			Dfs(np, map, d, parents, stack);
		stack.RemoveAt(stack.Count - 1);
	}
}
