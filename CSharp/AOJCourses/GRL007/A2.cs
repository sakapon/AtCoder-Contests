﻿using System;
using System.Collections.Generic;

class A2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		int nx = h[0], ny = h[1];
		var es = Array.ConvertAll(new bool[h[2]], _ => Read());

		var bm = new BipartiteMatchingFF(nx, ny);
		bm.AddEdges(es);
		Console.WriteLine(bm.FordFulkerson());
	}
}

public class BipartiteMatchingFF
{
	int n1, n2;
	List<int>[] map;
	int[] match;
	bool[] u;

	// 0 <= v1 < n1, 0 <= v2 < n2
	public BipartiteMatchingFF(int n1, int n2)
	{
		this.n1 = n1;
		this.n2 = n2;
		var n = n1 + n2;
		map = Array.ConvertAll(new bool[n], _ => new List<int>());
		match = new int[n];
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
		foreach (var u2 in map[v1])
		{
			var u1 = match[u2];
			if (u1 == -1 || !u[u1] && Dfs(u1))
			{
				match[v1] = u2;
				match[u2] = v1;
				return true;
			}
		}
		return false;
	}

	public int FordFulkerson()
	{
		match = Array.ConvertAll(map, _ => -1);
		var r = 0;

		for (int v = 0; v < n1; ++v)
		{
			if (match[v] != -1) continue;
			Array.Clear(u, 0, u.Length);
			if (Dfs(v)) ++r;
		}
		return r;
	}
}
