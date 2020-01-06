using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		int n = h[0], u = h[1], v = h[2];
		var rs = new int[n - 1].Select(_ => read()).ToArray();
		var map = rs.Concat(rs.Select(r => new[] { r[1], r[0] })).ToLookup(r => r[0], r => r[1]);

		var d = new int[n + 1];
		var branch = new bool[n + 1];
		var parents = new int[n + 1].Select(_ => new List<int>()).ToArray();
		var stack = new List<int>();
		Dfs(v, map, d, branch, parents, stack);

		var escape = 0;
		foreach (var x in Enumerable.Range(1, n).Where(i => !branch[i]).OrderBy(i => -d[i]))
		{
			if (x == u) { escape = x; break; }
			if (d[x] <= d[u]) continue;

			var t = x;
			while (d[t] > d[u])
				t = parents[t][(int)Math.Log(d[t] - d[u], 2)];
			if (t == u) { escape = x; break; }

			var ut = u;
			while (t != ut)
			{
				var k = 0;
				while (k < parents[t].Count && parents[t][k] != parents[ut][k]) k++;
				if (k > 0) k--;
				t = parents[t][k];
				ut = parents[ut][k];
			}
			if (d[u] - d[t] < d[t] - d[v]) { escape = x; break; }
		}
		Console.WriteLine(d[escape] - 1);
	}

	static int[] p2 = Enumerable.Range(0, 20).Select(i => (int)Math.Pow(2, i)).ToArray();

	static void Dfs(int p, ILookup<int, int> map, int[] d, bool[] branch, List<int>[] parents, List<int> stack)
	{
		d[p] = stack.Count;
		for (int i = 0; p2[i] <= stack.Count; i++)
			parents[p].Add(stack[stack.Count - p2[i]]);

		stack.Add(p);
		foreach (var np in map[p])
		{
			if (stack.Count >= 2 && np == stack[stack.Count - 2]) continue;
			branch[p] = true;
			Dfs(np, map, d, branch, parents, stack);
		}
		stack.RemoveAt(stack.Count - 1);
	}
}
