using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Graphs.SPPs.Int.WeightedGraph401;

class N
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var (h, w) = Read2();
		var s = Array.ConvertAll(new bool[h], _ => ReadL());

		s[0][0] = 1L << 50;
		s[^1][^1] = 1L << 50;

		var n = h * w;
		var grid = new IntWeightedGrid_N(s);
		var r = grid.Dijkstra(n, n + 1);
		var path = r[n + 1].GetPathVertexes();

		var g = NewArray2(h, w, '.');
		foreach (var v in path[1..^1])
		{
			var (i, j) = grid.FromVertexId(v);
			g[i][j] = '#';
		}

		Console.WriteLine(r[n + 1].Cost);
		foreach (var cs in g)
		{
			Console.WriteLine(new string(cs));
		}
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}

public class IntWeightedGrid_N : WeightedGrid
{
	readonly long[][] s;
	public long[][] Cells => s;
	public long[] this[int i] => s[i];
	public IntWeightedGrid_N(long[][] s) : base(s.Length, s[0].Length) { this.s = s; }

	public override List<(int to, long cost)> GetEdges(int v)
	{
		if (v == VertexesCount)
		{
			var l = new List<(int, long)>();
			for (int i = 1; i < h; i++)
			{
				var (ni, nj) = (i, 0);
				l.Add((w * ni + nj, s[ni][nj]));
			}
			for (int j = 1; j < w - 1; j++)
			{
				var (ni, nj) = (h - 1, j);
				l.Add((w * ni + nj, s[ni][nj]));
			}
			return l;
		}
		else
		{
			var (i, j) = (v / w, v % w);
			var l = new List<(int, long)>();
			foreach (var (di, dj) in NextsDelta8)
			{
				var (ni, nj) = (i + di, j + dj);
				if (0 <= ni && ni < h && 0 <= nj && nj < w) l.Add((w * ni + nj, s[ni][nj]));
			}
			if (i == 0 || j == w - 1)
			{
				l.Add((VertexesCount + 1, 0));
			}
			return l;
		}
	}

	public Vertex[] Dijkstra(int sv, int ev = -1)
	{
		var vs = new Vertex[VertexesCount + 2];
		for (int v = 0; v < vs.Length; ++v) vs[v] = new Vertex(v);

		vs[sv].Cost = 0;
		var q = new SortedSet<(long, int)> { (0, sv) };

		while (q.Count > 0)
		{
			var (c, v) = q.Min;
			q.Remove((c, v));
			if (v == ev) return vs;
			var vo = vs[v];

			foreach (var (nv, cost) in GetEdges(v))
			{
				var nvo = vs[nv];
				var nc = c + cost;
				if (nvo.Cost <= nc) continue;
				if (nvo.Cost != long.MaxValue) q.Remove((nvo.Cost, nv));
				q.Add((nc, nv));
				nvo.Cost = nc;
				nvo.Parent = vo;
			}
		}
		return vs;
	}
}
