using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, qc) = Read2();
		var es = Array.ConvertAll(new bool[n - 1], _ => Read());
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		var root = 1;
		var map = ToMap(n + 1, es, true);

		var dp = new bool[n + 1];
		DFS(root, -1);

		return string.Join("\n", qs.Select(q =>
		{
			var (u, v) = q;
			return dp[u] == dp[v] ? "Town" : "Road";
		}));

		void DFS(int v, int pv)
		{
			foreach (var nv in map[v])
			{
				if (nv == pv) continue;
				dp[nv] = !dp[v];
				DFS(nv, v);
			}
		}
	}

	public static int[][] ToMap(int n, int[][] es, bool twoWay)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int>());
		foreach (var e in es)
		{
			map[e[0]].Add(e[1]);
			if (twoWay) map[e[1]].Add(e[0]);
		}
		return Array.ConvertAll(map, l => l.ToArray());
	}
}
