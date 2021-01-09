using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		var n = h[0];
		var es = new int[h[1]].Select(_ => Read()).ToArray();
		var qs = new int[int.Parse(Console.ReadLine())].Select(_ => Read()).ToArray();

		var map = Array.ConvertAll(new int[n], _ => new List<int>());
		var map_r = Array.ConvertAll(new int[n], _ => new List<int>());
		foreach (var e in es)
		{
			map[e[0]].Add(e[1]);
			map_r[e[1]].Add(e[0]);
		}

		var uf = new UF(n);
		var u = new bool[n];
		var order = new List<int>();

		Action<int> Dfs = null;
		Dfs = v =>
		{
			u[v] = true;
			foreach (var nv in map[v])
			{
				if (u[nv]) continue;
				Dfs(nv);
			}
			order.Add(v);
		};
		for (int v = 0; v < n; v++) if (!u[v]) Dfs(v);

		Action<int> Dfs_r = null;
		Dfs_r = v =>
		{
			u[v] = true;
			foreach (var nv in map_r[v])
			{
				if (u[nv]) continue;
				uf.Unite(v, nv);
				Dfs_r(nv);
			}
		};
		Array.Clear(u, 0, n);
		order.Reverse();
		foreach (var v in order) if (!u[v]) Dfs_r(v);

		Console.WriteLine(string.Join("\n", qs.Select(v => uf.AreUnited(v[0], v[1]) ? 1 : 0)));
	}
}

class UF
{
	int[] p;
	public UF(int n) { p = Enumerable.Range(0, n).ToArray(); }

	public void Unite(int a, int b) { if (!AreUnited(a, b)) p[p[b]] = p[a]; }
	public bool AreUnited(int a, int b) => GetRoot(a) == GetRoot(b);
	public int GetRoot(int a) => p[a] == a ? a : p[a] = GetRoot(p[a]);
}
