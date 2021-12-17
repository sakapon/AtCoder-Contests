using System;
using System.Collections.Generic;
using System.Linq;

class E
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
		var es = Array.ConvertAll(new bool[n - 1], _ => Read());

		// 辺の ID
		var map = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
		// 駒の移動 ID
		var aMap = Array.ConvertAll(new bool[n + 1], _ => new HashSet<int>());

		for (int i = 0; i < es.Length; i++)
		{
			var e = es[i];
			map[e[0]].Add(i);
			map[e[1]].Add(i);
		}

		for (int i = 1; i < m; i++)
		{
			if (a[i] == a[i - 1]) continue;
			aMap[a[i]].Add(i);
			aMap[a[i - 1]].Add(i);
		}

		// 辺を通過する回数
		var counts = new int[es.Length];
		Dfs(1, -1);

		HashSet<int> Dfs(int v, int pv)
		{
			var set = aMap[v];

			foreach (var ei in map[v])
			{
				var e = es[ei];
				var nv = e[0] == v ? e[1] : e[0];
				if (nv == pv) continue;

				var set2 = Dfs(nv, v);
				counts[ei] = set2.Count;
				set.SymmetricExceptWith(set2);
			}
			return set;
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
