using System;
using System.Collections.Generic;
using System.Linq;

class Q021
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var map = EdgesToMap1(n + 1, es, true);
		var g = Scc(n + 1, map);
		var cs = Tally(g, n + 1);
		return cs.Sum(c => (long)c * (c - 1) / 2);
	}

	static int[] Tally(int[] a, int max)
	{
		var r = new int[max + 1];
		foreach (var x in a) ++r[x];
		return r;
	}

	static List<int>[] EdgesToMap1(int n, int[][] es, bool directed)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int>());
		foreach (var e in es)
		{
			map[e[0]].Add(e[1]);
			if (!directed) map[e[1]].Add(e[0]);
		}
		return map;
	}

	// 結果のグループ ID は逆順。
	static int[] Scc(int n, List<int>[] map)
	{
		var g = new int[n];
		var order = new int[n];
		var back = new int[n];
		int gi = 0, oi = 0;
		var pend = new Stack<int>();

		Func<int, int> Dfs = null;
		Dfs = v =>
		{
			back[v] = order[v] = ++oi;
			pend.Push(v);

			foreach (var nv in map[v])
			{
				if (g[nv] != 0) continue;
				back[v] = Math.Min(back[v], back[nv] == 0 ? Dfs(nv) : back[nv]);
			}

			if (back[v] == order[v])
			{
				gi++;
				var lv = -1;
				while (lv != v && pend.Count > 0) g[lv = pend.Pop()] = gi;
			}
			return back[v];
		};
		for (int v = 0; v < n; v++) if (g[v] == 0) Dfs(v);

		return g;
	}
}
