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

		var dp = TSP(m + 2, (1 << m + 2) - 1, map, sv);
		for (int x = 3 << m; x < 1 << m + 2; x++)
		{
			if (dp[x][gv] <= t) r = Math.Max(r, BitOperations.PopCount((uint)x) - 2);
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

	// 集合 s をすべて訪れる
	static long[][] TSP(int n, int s, long[][] map, int sv)
	{
		var dp = NewArray2(1 << n, n, long.MaxValue);
		dp[1 << sv][sv] = 0;

		AllSubsets(n, s, x =>
		{
			for (int j = 0; j < n; j++)
			{
				if (dp[x][j] == long.MaxValue) continue;

				for (int nj = 0; nj < n; nj++)
				{
					if ((s & (1 << nj)) == 0) continue;

					var nc = dp[x][j] + map[j][nj];
					if (dp[x | (1 << nj)][nj] <= nc) continue;
					dp[x | (1 << nj)][nj] = nc;
				}
			}
			return false;
		});

		return dp;
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));

	public static void AllSubsets(int n, int s, Func<int, bool> action)
	{
		for (int x = 0; ; x = (x - s) & s)
		{
			if (action(x)) break;
			if (x == s) break;
		}
	}
}
