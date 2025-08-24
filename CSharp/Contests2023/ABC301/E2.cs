using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class E2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w, t) = Read3();
		var s0 = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		var n = h * w;
		var s = s0.SelectMany(c => c).ToArray();

		var rn = Enumerable.Range(0, n).ToArray();
		var sv = rn.Single(v => s[v] == 'S');
		var gv = rn.Single(v => s[v] == 'G');
		var ovs = rn.Where(v => s[v] == 'o').Append(sv).Append(gv).ToArray();

		var d = ovs.Select(v =>
		{
			var cs = Bfs(v);
			return ovs.Select(v => cs[v]).ToArray();
		}).ToArray();

		// お菓子の数
		var m = ovs.Length - 2;
		sv = m;
		gv = m + 1;

		if (m == 0)
		{
			if (d[sv][gv] <= t) return 0;
			return -1;
		}

		var dp = NewArray2(1 << m, m, long.MaxValue);
		for (int v = 0; v < m; v++)
		{
			dp[1 << v][v] = d[sv][v];
		}
		TSP(m, dp, d);

		var f = 0U;
		if (d[sv][gv] <= t) f |= 1U << 0;

		for (uint x = 0; x < 1 << m; x++)
		{
			for (int v = 0; v < m; v++)
			{
				if (dp[x][v] == long.MaxValue) continue;
				if (d[v][gv] == long.MaxValue) continue;
				if (dp[x][v] + d[v][gv] <= t) f |= 1U << BitOperations.PopCount(x);
			}
		}
		if (f == 0) return -1;
		return BitOperations.Log2(f);

		IEnumerable<int> GetNexts(int v)
		{
			var (i, j) = (v / w, v % w);
			if (j > 0) yield return v - 1;
			if (i > 0) yield return v - w;
			if (j + 1 < w) yield return v + 1;
			if (i + 1 < h) yield return v + w;
		}

		long[] Bfs(int sv)
		{
			var cs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
			var q = new Queue<int>();
			cs[sv] = 0;
			q.Enqueue(sv);

			while (q.Count > 0)
			{
				var v = q.Dequeue();
				var nc = cs[v] + 1;

				foreach (var nv in GetNexts(v))
				{
					if (s[nv] == '#') continue;
					if (cs[nv] <= nc) continue;
					cs[nv] = nc;
					q.Enqueue(nv);
				}
			}
			return cs;
		}
	}

	public static T[][] NewArray2<T>(int n1, int n2, T v = default(T)) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));

	// dp[訪問済の頂点集合][最後に訪問した頂点]: 最短距離
	public static long[][] TSP(int n, long[][] dp, long[][] d)
	{
		for (uint x = 0; x < 1U << n; ++x)
		{
			for (int v = 0; v < n; ++v)
			{
				if (dp[x][v] == long.MaxValue) continue;

				for (int nv = 0; nv < n; ++nv)
				{
					var nx = x | (1U << nv);
					if (nx == x) continue;
					if (d[v][nv] == long.MaxValue) continue;

					var nc = dp[x][v] + d[v][nv];
					if (dp[nx][nv] <= nc) continue;
					dp[nx][nv] = nc;
				}
			}
		}
		return dp;
	}
}
