using System;
using System.Collections.Generic;
using System.Linq;

class Q073
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var c = Console.ReadLine().Split().Select(s => s[0]).Prepend('/').ToArray();
		var es = Array.ConvertAll(new bool[n - 1], _ => Read());

		var map = ToMap(n + 1, es, false);

		// 0: ab: 条件を満たす
		// 1: a : 未確定
		// 2: b : 未確定
		var dp = NewArray2<long>(n + 1, 3);

		Dfs(1, -1);
		return dp[1][0];

		void Dfs(int v, int pv)
		{
			var vj = c[v] == 'a' ? 1 : 2;
			dp[v][0] = 1;
			dp[v][vj] = 1;

			foreach (var nv in map[v])
			{
				if (nv == pv) continue;

				Dfs(nv, v);

				dp[v][0] *= 2 * dp[nv][0] + dp[nv][1] + dp[nv][2];
				dp[v][0] %= M;
				dp[v][vj] *= dp[nv][0] + dp[nv][vj];
				dp[v][vj] %= M;
			}

			dp[v][0] -= dp[v][vj];
			dp[v][0] += M;
			dp[v][0] %= M;
		}
	}

	const long M = 1000000007;
	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));

	static List<int>[] ToMap(int n, int[][] es, bool directed)
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
