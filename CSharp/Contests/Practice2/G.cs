using System;
using System.Collections.Generic;
using System.Linq;

class G
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		var n = h[0];
		// 多重辺および自己ループを除去。
		var es = new int[h[1]].Select(_ => Read()).Select(e => (v0: e[0], v1: e[1])).Where(e => e.v0 != e.v1).Distinct().ToArray();

		var map = Array.ConvertAll(new int[n], _ => new List<int>());
		var map_r = Array.ConvertAll(new int[n], _ => new List<int>());
		foreach (var (v0, v1) in es)
		{
			map[v0].Add(v1);
			map_r[v1].Add(v0);
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

		Console.WriteLine(gs.Count);
		Console.WriteLine(string.Join("\n", gs.Select(g => string.Join(" ", g.Prepend(g.Count)))));
	}
}
