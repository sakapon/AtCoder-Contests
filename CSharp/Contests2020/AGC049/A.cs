using System;
using System.Collections.Generic;
using System.Linq;

class A
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = new int[n].Select(_ => Console.ReadLine()).ToArray();

		var uf = new UF(n);
		var map = Array.ConvertAll(new int[n], _ => new List<int>());
		var map_r = Array.ConvertAll(new int[n], _ => new List<int>());
		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < n; j++)
			{
				if (s[i][j] == '1')
				{
					uf.Unite(i, j);
					map[i].Add(j);
					map_r[j].Add(i);
				}
			}
		}

		var u = new bool[n];
		var order = new int[n];
		var c = n;
		var gs = new List<List<int>>();
		List<int> lg = null;

		void Dfs(int v)
		{
			u[v] = true;
			foreach (var nv in map[v]) if (!u[nv]) Dfs(nv);
			order[--c] = v;
		}
		for (int v = 0; v < n; v++) if (!u[v]) Dfs(v);

		void Dfs_r(int v)
		{
			u[v] = true;
			foreach (var nv in map_r[v]) if (!u[nv]) Dfs_r(nv);
			lg.Add(v);
		}
		Array.Clear(u, 0, n);
		foreach (var v in order) if (!u[v]) { gs.Add(lg = new List<int>()); Dfs_r(v); }

		var r = 0.0;

		foreach (var ufg in gs.GroupBy(g => uf.GetRoot(g[0])))
		{
			var gcs = ufg.Select(g => g.Count).ToArray();
			var seq = new Seq(gcs);

			var dp = new double[gcs.Length];
			dp[0] = 1;
			for (int i = 1; i < gcs.Length; i++)
			{
				var all = seq.Sum(0, i + 1);
				dp[i] = dp[i - 1] + (double)gcs[i] / all;
			}
			r += dp.Last();
		}
		Console.WriteLine(r);
	}
}

class Seq
{
	int[] a;
	long[] s;
	public Seq(int[] _a) { a = _a; }

	public long Sum(int minIn, int maxEx)
	{
		if (s == null)
		{
			s = new long[a.Length + 1];
			for (int i = 0; i < a.Length; ++i) s[i + 1] = s[i] + a[i];
		}
		return s[maxEx] - s[minIn];
	}

	// C# 8.0
	//public long Sum(Range r) => Sum(r.Start.GetOffset(a.Length), r.End.GetOffset(a.Length));
}

class UF
{
	int[] p;
	public UF(int n) { p = Enumerable.Range(0, n).ToArray(); }

	public void Unite(int a, int b) { if (!AreUnited(a, b)) p[p[b]] = p[a]; }
	public bool AreUnited(int a, int b) => GetRoot(a) == GetRoot(b);
	public int GetRoot(int a) => p[a] == a ? a : p[a] = GetRoot(p[a]);
	public int[][] ToGroups() => Enumerable.Range(0, p.Length).GroupBy(GetRoot).Select(g => g.ToArray()).ToArray();
}
