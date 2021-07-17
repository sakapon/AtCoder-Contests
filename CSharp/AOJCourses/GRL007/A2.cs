using System;
using System.Collections.Generic;

class A2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		int nx = h[0], ny = h[1];
		var es = Array.ConvertAll(new bool[h[2]], _ => Read());

		var bm = new BipartiteMatching(nx, ny);
		bm.AddEdges(es);
		var r = bm.Dinic();
		Console.WriteLine(r.Length);
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
