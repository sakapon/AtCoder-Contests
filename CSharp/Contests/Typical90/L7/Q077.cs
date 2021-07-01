using System;
using System.Collections.Generic;
using System.Linq;

class Q077
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, t) = Read2();
		var a = Array.ConvertAll(new bool[n], _ => Read2());
		var b = Array.ConvertAll(new bool[n], _ => Read2());

		var rn = Enumerable.Range(0, n).ToArray();
		var bInv = rn.ToDictionary(j => b[j], j => j);
		var nexts = new[] { (t, 0), (t, t), (0, t), (-t, t), (-t, 0), (-t, -t), (0, -t), (t, -t) };
		var nextsInv = Enumerable.Range(0, 8).ToDictionary(i => nexts[i], i => i + 1);

		var bm = new BipartiteMatching(n, n);
		for (int i = 0; i < n; i++)
		{
			var (x, y) = a[i];
			foreach (var (dx, dy) in nexts)
			{
				var np = (x + dx, y + dy);
				if (bInv.ContainsKey(np))
					bm.AddEdge(i, bInv[np]);
			}
		}

		var res = bm.Dinic();
		if (res.Length < n) return "No";

		var r = Array.ConvertAll(rn, i =>
		{
			var j = res[i][1];
			var (x, y) = a[i];
			var (nx, ny) = b[j];
			return nextsInv[(nx - x, ny - y)];
		});
		return "Yes\n" + string.Join(" ", r);
	}
}

public class BipartiteMatching
{
	int n1;
	List<int>[] map;
	int[] match;
	bool[] u;

	// 0 <= v1 < n1, 0 <= v2 < n2
	public BipartiteMatching(int n1, int n2)
	{
		this.n1 = n1;
		var n = n1 + n2;
		map = Array.ConvertAll(new bool[n], _ => new List<int>());
		u = new bool[n];
	}

	public void AddEdge(int from, int to)
	{
		map[from].Add(n1 + to);
		map[n1 + to].Add(from);
	}

	// { from, to }
	public void AddEdges(int[][] des)
	{
		foreach (var e in des) AddEdge(e[0], e[1]);
	}

	bool Dfs(int v1)
	{
		u[v1] = true;
		foreach (var v2 in map[v1])
		{
			var u1 = match[v2];
			if (u1 == -1 || !u[u1] && Dfs(u1))
			{
				match[v1] = v2;
				match[v2] = v1;
				return true;
			}
		}
		return false;
	}

	public int[][] Dinic()
	{
		match = Array.ConvertAll(map, _ => -1);

		for (int v1 = 0; v1 < n1; ++v1)
		{
			if (match[v1] != -1) continue;
			Array.Clear(u, 0, u.Length);
			Dfs(v1);
		}

		var r = new List<int[]>();
		for (int v1 = 0; v1 < n1; ++v1)
			if (match[v1] != -1)
				r.Add(new[] { v1, match[v1] - n1 });
		return r.ToArray();
	}
}
