using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class E
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

		var map = ovs.Select(v =>
		{
			var cs = Bfs(v);
			return ovs.Select(v => cs[v]).ToArray();
		}).ToArray();

		// お菓子の数
		var m = ovs.Length - 2;
		sv = m;
		gv = m + 1;

		var r = -1;
		var dp = TSP(m, map);

		for (int x = 0; x < 1 << m; x++)
		{
			if (dp[x][gv] <= t) r = Math.Max(r, BitOperations.PopCount((uint)x));
		}
		return r;

		void Next(int v, Action<int> forNext)
		{
			var i = v / w;
			var j = v % w;
			int nv;
			if (i > 0 && s[nv = v - w] != '#') forNext(nv);
			if (i < h - 1 && s[nv = v + w] != '#') forNext(nv);
			if (j > 0 && s[nv = v - 1] != '#') forNext(nv);
			if (j < w - 1 && s[nv = v + 1] != '#') forNext(nv);
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
				Next(v, nv =>
				{
					var nc = cs[v] + 1;
					if (cs[nv] <= nc) return;
					cs[nv] = nc;
					q.Enqueue(nv);
				});
			}
			return cs;
		}
	}

	// n: 途中で訪れる頂点の数。始点および終点は含まれません。
	// 始点の頂点番号: n
	// 終点の頂点番号: n + 1
	static long[][] TSP(int n, long[][] map)
	{
		// [途中で訪れた頂点の集合][最後に訪れた頂点。始点および終点を含む]
		var dp = Array.ConvertAll(new bool[1 << n], _ => Array.ConvertAll(new bool[n + 2], __ => long.MaxValue));
		dp[0][n] = 0;

		for (int x = 0; x < 1 << n; x++)
		{
			for (int j = 0; j <= n; j++)
			{
				if (dp[x][j] == long.MaxValue) continue;

				for (int nj = 0; nj < n; nj++)
				{
					if (map[j][nj] == long.MaxValue) continue;

					var nc = dp[x][j] + map[j][nj];
					if (dp[x | (1 << nj)][nj] <= nc) continue;
					dp[x | (1 << nj)][nj] = nc;
				}

				// 終点
				{
					if (map[j][n + 1] == long.MaxValue) continue;

					var nc = dp[x][j] + map[j][n + 1];
					if (dp[x][n + 1] <= nc) continue;
					dp[x][n + 1] = nc;
				}
			}
		}
		return dp;
	}
}
