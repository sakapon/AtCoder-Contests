using System;
using System.Collections.Generic;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n - 1], _ => Read());

		var map = ToMap(n + 1, es, false);

		// 自身を含む個数
		var subvers = new int[n + 1];

		var upperSum = new long[n + 1];
		var lowerSum = new long[n + 1];
		var r = new long[n + 1];

		Dfs1(1, -1);
		Dfs2(1, -1);

		return string.Join("\n", r[1..]);

		void Dfs1(int v, int pv)
		{
			foreach (var nv in map[v])
			{
				if (nv == pv) continue;
				Dfs1(nv, v);

				subvers[v] += subvers[nv];
				lowerSum[v] += lowerSum[nv] + subvers[nv];
			}
			subvers[v]++;
		}

		void Dfs2(int v, int pv)
		{
			r[v] = upperSum[v] + lowerSum[v];

			foreach (var nv in map[v])
			{
				if (nv == pv) continue;

				upperSum[nv] = r[v] - (lowerSum[nv] + subvers[nv]) + (n - subvers[nv]);

				Dfs2(nv, v);
			}
		}
	}

	public static int[][] ToMap(int n, int[][] es, bool directed) => Array.ConvertAll(ToMapList(n, es, directed), l => l.ToArray());
	public static List<int>[] ToMapList(int n, int[][] es, bool directed)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int>());
		foreach (var e in es)
		{
			map[e[0]].Add(e[1]);
			if (!directed) map[e[1]].Add(e[0]);
		}
		return map;
	}
}
