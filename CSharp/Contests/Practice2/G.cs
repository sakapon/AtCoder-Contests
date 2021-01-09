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
		var es = new int[h[1]].Select(_ => Read()).ToArray();

		var map = Array.ConvertAll(new int[n], _ => new List<int>());
		var map_r = Array.ConvertAll(new int[n], _ => new List<int>());
		foreach (var e in es)
		{
			map[e[0]].Add(e[1]);
			map_r[e[1]].Add(e[0]);
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
