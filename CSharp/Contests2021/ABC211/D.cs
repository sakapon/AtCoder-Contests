using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	const long M = 1000000007;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var map = ToMap(n + 1, es, false);
		var sv = 1;
		var ev = n;

		var dp = new long[n + 1];
		dp[sv] = 1;

		var costs = Array.ConvertAll(new bool[n + 1], _ => long.MaxValue);
		var q = new Queue<int>();
		costs[sv] = 0;
		q.Enqueue(sv);

		while (q.Count > 0)
		{
			var v = q.Dequeue();
			var nc = costs[v] + 1;

			foreach (var nv in map[v])
			{
				if (costs[nv] < nc) continue;

				if (costs[nv] == nc)
				{
					dp[nv] += dp[v];
					dp[nv] %= M;
				}
				else
				{
					dp[nv] = dp[v];
					costs[nv] = nc;
					//if (nv == ev) return costs;
					q.Enqueue(nv);
				}
			}
		}

		return dp[n];
	}

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
