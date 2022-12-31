using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Graphs.SPPs.Int.UnweightedGraph401;
using CoderLib8.Graphs.SPPs.Int.WeightedGraph401;
using WVertex = CoderLib8.Graphs.SPPs.Int.WeightedGraph401.Vertex;

class I
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		var grid = new CharWeightedGrid_I(s);
		var ev = grid.FindVertexId('g');
		var r = grid.Dijkstra_I();
		var min = r[(ev * 4)..((ev + 1) * 4)].Min(v => v.Cost);
		if (min == long.MaxValue) return -1;
		return min;
	}
}

public class CharWeightedGrid_I : WeightedGrid
{
	readonly char[][] s;
	readonly char wall;
	public char[][] Cells => s;
	public char[] this[int i] => s[i];
	CharUnweightedGrid ugrid;
	public CharWeightedGrid_I(char[][] s, char wall = '#') : base(s.Length, s[0].Length)
	{
		this.s = s; this.wall = wall;
		ugrid = new CharUnweightedGrid(s);
	}
	public CharWeightedGrid_I(string[] s, char wall = '#') : this(ToArrays(s), wall) { }

	public static char[][] ToArrays(string[] s) => Array.ConvertAll(s, l => l.ToCharArray());

	public (int i, int j) FindCell(char c)
	{
		for (int i = 0; i < h; ++i)
			for (int j = 0; j < w; ++j)
				if (s[i][j] == c) return (i, j);
		return (-1, -1);
	}

	public int FindVertexId(char c)
	{
		for (int i = 0; i < h; ++i)
			for (int j = 0; j < w; ++j)
				if (s[i][j] == c) return w * i + j;
		return -1;
	}

	public override List<(int to, long cost)> GetEdges(int v)
	{
		v = Math.DivRem(v, 4, out var K);
		var (i, j) = (v / w, v % w);

		// k は NextsDelta の順に対応します。
		var (DI, DJ) = NextsDelta[K];

		var l = new List<(int, long)>();

		{
			// 荷物の隣に移動する
			var (si, sj) = (i + DI, j + DJ);
			s[i][j] = wall;
			var uvs = ugrid.ShortestByBFS(ToVertexId(si, sj));
			s[i][j] = '.';

			for (int k = 0; k < 4; k++)
			{
				var (di, dj) = NextsDelta[k];
				var (ni, nj) = (i + di, j + dj);
				var nv = w * ni + nj;
				if (0 <= ni && ni < h && 0 <= nj && nj < w && uvs[nv].IsConnected)
				{
					l.Add((v * 4 + k, uvs[nv].Cost));
				}
			}
		}
		{
			// 荷物を押す
			var (ni, nj) = (i - DI, j - DJ);
			if (0 <= ni && ni < h && 0 <= nj && nj < w && s[ni][nj] != wall) l.Add(((w * ni + nj) * 4 + K, 1));
		}
		return l;
	}

	public WVertex[] Dijkstra_I()
	{
		var vs = new WVertex[VertexesCount * 4];
		for (int v = 0; v < vs.Length; ++v) vs[v] = new WVertex(v);

		var sv = FindVertexId('s');
		var (ai, aj) = FindCell('a');
		var av = ToVertexId(ai, aj);
		s[ai][aj] = wall;
		var uvs = ugrid.ShortestByBFS(sv);
		s[ai][aj] = '.';

		var q = new SortedSet<(long, int)>();

		// k は NextsDelta の順に対応します。
		for (int k = 0; k < 4; k++)
		{
			var (di, dj) = NextsDelta[k];
			var (ni, nj) = (ai + di, aj + dj);
			var nv = w * ni + nj;
			if (0 <= ni && ni < h && 0 <= nj && nj < w && uvs[nv].IsConnected)
			{
				var sav = av * 4 + k;
				vs[sav].Cost = uvs[nv].Cost;
				q.Add((vs[sav].Cost, sav));
			}
		}

		while (q.Count > 0)
		{
			var (c, v) = q.Min;
			q.Remove((c, v));
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
