using System;
using System.Collections.Generic;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (h, w) = Read2();
		var a = Array.ConvertAll(new bool[h], _ => Read());

		var amax = 500000;
		var d = new List<int[]>[amax + 1];
		var dr = new Dictionary<int, int>[amax + 1];
		var dc = new Dictionary<int, int>[amax + 1];

		for (int i = 0; i < h; i++)
		{
			for (int j = 0; j < w; j++)
			{
				var k = a[i][j];
				if (k == 0) continue;

				if (d[k] == null)
				{
					d[k] = new List<int[]>();
					dr[k] = new Dictionary<int, int>();
					dc[k] = new Dictionary<int, int>();
				}

				if (!dr[k].ContainsKey(i))
					dr[k][i] = dr[k].Count;
				if (!dc[k].ContainsKey(j))
					dc[k][j] = dc[k].Count;

				d[k].Add(new[] { dr[k][i], dc[k][j] });
			}
		}

		var r = 0L;

		for (int k = 0; k <= amax; k++)
		{
			if (d[k] == null) continue;
			if (d[k].Count == 1)
			{
				r++;
				continue;
			}

			var bm = new BipartiteMatching(dr[k].Count, dc[k].Count);
			bm.AddEdges(d[k].ToArray());
			var res = bm.Dinic();
			r += res.Length;
		}
		Console.WriteLine(r);
	}
}

public class BipartiteMatching
{
	int n1;
	List<int>[] map;
	public int[][] Map;
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
		foreach (var v2 in Map[v1])
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
		Map = Array.ConvertAll(map, l => l.ToArray());
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
