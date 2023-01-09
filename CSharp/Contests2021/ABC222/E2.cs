using System;
using System.Collections.Generic;
using System.Linq;

class E2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m, k) = Read3();
		k = Math.Abs(k);
		var a = Read();
		var es = Array.ConvertAll(new bool[n - 1], _ => Read2());

		// 辺の ID
		var map = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
		for (int i = 0; i < es.Length; i++)
		{
			var (u, v) = es[i];
			map[u].Add(i);
			map[v].Add(i);
		}

		// 辺を通過する回数
		var counts = new int[es.Length];

		for (int j = 1; j < m; j++)
		{
			var sv = a[j - 1];
			var ev = a[j];
			Dfs(sv, -1);

			bool Dfs(int v, int pv)
			{
				if (v == ev) return true;

				foreach (var ei in map[v])
				{
					var (nu, nv) = es[ei];
					if (nv == v) nv = nu;
					if (nv == pv) continue;

					if (Dfs(nv, v))
					{
						counts[ei]++;
						return true;
					}
				}
				return false;
			}
		}

		var s = counts.Sum();
		if (s < k) return 0;
		if ((s + k) % 2 != 0) return 0;

		var x = (s + k) / 2;

		var dp = new long[s + 1];
		dp[0] = 1;

		foreach (var c in counts)
		{
			for (int i = s; i >= 0; i--)
			{
				var ni = i + c;
				if (s < ni) continue;
				dp[ni] += dp[i];
				dp[ni] %= M;
			}
		}

		return dp[x];
	}

	const long M = 998244353;
}
