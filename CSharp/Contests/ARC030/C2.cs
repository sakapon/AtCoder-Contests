using System;
using System.Collections.Generic;
using System.Linq;

class C2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m, k) = Read3();
		var c = Array.ConvertAll(Console.ReadLine().Split(), s => s[0]);
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var (gc, gis) = SCC(n + 1, es);
		gc--;

		var map = Array.ConvertAll(new bool[gc], _ => new HashSet<int>());
		foreach (var e in es)
		{
			var g0 = gis[e[0]];
			var g1 = gis[e[1]];
			if (g0 != g1) map[g0].Add(g1);
		}

		var gcls = Array.ConvertAll(map, _ => new List<char>());
		for (int v = 1; v <= n; ++v) gcls[gis[v]].Add(c[v - 1]);
		var gss = Array.ConvertAll(gcls, l =>
		{
			var cs = l.ToArray();
			Array.Sort(cs);
			return new string(cs);
		});

		var dp = NewArray2(gc, k + 1, "~");
		for (int i = 0; i < gc; i++)
		{
			dp[i][0] = "";
			for (int j = 1; j <= gss[i].Length; j++)
			{
				if (j > k) break;
				dp[i][j] = gss[i][..j];
			}
		}

		for (int i = 0; i < gc; i++)
		{
			for (int j = 0; j <= k; j++)
			{
				if (dp[i][j] == "~") continue;

				foreach (var ni in map[i])
				{
					for (int d = 0; d <= gss[ni].Length; d++)
					{
						var nj = j + d;
						if (nj > k) break;
						var nv = dp[i][j] + gss[ni][..d];
						if (string.CompareOrdinal(dp[ni][nj], nv) > 0) dp[ni][nj] = nv;
					}
				}
			}
		}

		var r = "~";
		for (int i = 0; i < gc; i++)
		{
			if (map[i].Count > 0) continue;
			if (string.CompareOrdinal(r, dp[i][k]) > 0) r = dp[i][k];
		}
		if (r == "~") return -1;
		return r;
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));

	// 1-based の場合、頂点 0 は最後のグループに含まれます。
	public static (int gc, int[] gis) SCC(int n, int[][] es)
	{
		var u = new bool[n];
		var t = n;
		var map = Array.ConvertAll(u, _ => new List<int>());
		var mapr = Array.ConvertAll(u, _ => new List<int>());
		foreach (var e in es)
		{
			map[e[0]].Add(e[1]);
			mapr[e[1]].Add(e[0]);
		}

		var vs = new int[n];
		for (int v = 0; v < n; ++v) Dfs(v);

		Array.Clear(u, 0, n);
		var gis = new int[n];
		foreach (var v in vs) if (Dfsr(v)) ++t;
		return (t, gis);

		void Dfs(int v)
		{
			if (u[v]) return;
			u[v] = true;
			foreach (var nv in map[v]) Dfs(nv);
			vs[--t] = v;
		}

		bool Dfsr(int v)
		{
			if (u[v]) return false;
			u[v] = true;
			foreach (var nv in mapr[v]) Dfsr(nv);
			gis[v] = t;
			return true;
		}
	}
}
