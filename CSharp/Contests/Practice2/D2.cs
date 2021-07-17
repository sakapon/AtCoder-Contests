using System;
using System.Collections.Generic;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, m) = Read2();
		var s = Array.ConvertAll(new bool[n], _ => Console.ReadLine().ToCharArray());

		var vs0 = new List<int>();
		var vs1 = new List<int>();
		var vs1Inv = Array.ConvertAll(new bool[n * m], _ => -1);

		// checker board
		for (int i = 0; i < n; i++)
			for (int j = 0; j < m; j++)
			{
				if (s[i][j] == '#') continue;

				var v = m * i + j;
				if ((i + j) % 2 == 0)
				{
					vs0.Add(v);
				}
				else
				{
					vs1Inv[v] = vs1.Count;
					vs1.Add(v);
				}
			}

		var bm = new BipartiteMatching(vs0.Count, vs1.Count);

		for (int i = 0; i < vs0.Count; i++)
		{
			var v = vs0[i];
			if (v - m >= 0 && vs1Inv[v - m] != -1) bm.AddEdge(i, vs1Inv[v - m]);
			if (v + m < n * m && vs1Inv[v + m] != -1) bm.AddEdge(i, vs1Inv[v + m]);
			if (v % m != 0 && vs1Inv[v - 1] != -1) bm.AddEdge(i, vs1Inv[v - 1]);
			if (v % m != m - 1 && vs1Inv[v + 1] != -1) bm.AddEdge(i, vs1Inv[v + 1]);
		}

		var res = bm.Dinic();

		foreach (var e in res)
		{
			var v0 = vs0[e[0]];
			var v1 = vs1[e[1]];
			var (i0, j0) = (v0 / m, v0 % m);
			var (i1, j1) = (v1 / m, v1 % m);

			if (i0 == i1)
			{
				s[i0][Math.Min(j0, j1)] = '>';
				s[i0][Math.Max(j0, j1)] = '<';
			}
			else
			{
				s[Math.Min(i0, i1)][j0] = 'v';
				s[Math.Max(i0, i1)][j0] = '^';
			}
		}

		Console.WriteLine(res.Length);
		foreach (var r in s) Console.WriteLine(new string(r));
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
