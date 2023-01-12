using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Graphs.SPPs.Int.UnweightedGraph401;

class I2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		var n = h * w;
		var grid = new CharUnweightedGrid_I2(s);
		var av = grid.FindVertexId('a');
		var sv = grid.FindVertexId('s');
		var ev = grid.FindVertexId('g');

		var r = grid.ShortestByBFS(av * n + sv);
		var min = r[(ev * n)..((ev + 1) * n)].Min(v => v.Cost);
		if (min == long.MaxValue) return -1;
		return min;
	}
}

public abstract class UnweightedGrid_I2 : UnweightedGraph
{
	protected readonly int h, w;
	public int Height => h;
	public int Width => w;
	public UnweightedGrid_I2(int h, int w) : base(h * w * h * w) { this.h = h; this.w = w; }

	public int ToVertexId(int i, int j) => w * i + j;
	public (int i, int j) FromVertexId(int v) => (v / w, v % w);

	public static (int di, int dj)[] NextsDelta { get; } = new[] { (0, -1), (0, 1), (-1, 0), (1, 0) };
}

public class CharUnweightedGrid_I2 : UnweightedGrid_I2
{
	readonly char[][] s;
	readonly char wall;
	public char[][] Cells => s;
	public char[] this[int i] => s[i];
	public CharUnweightedGrid_I2(char[][] s, char wall = '#') : base(s.Length, s[0].Length) { this.s = s; this.wall = wall; }
	public CharUnweightedGrid_I2(string[] s, char wall = '#') : this(ToArrays(s), wall) { }

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

	public override List<int> GetEdges(int v)
	{
		var n = h * w;
		var (av, sv) = (v / n, v % n);
		var (i, j) = (sv / w, sv % w);
		var (ai, aj) = (av / w, av % w);

		var l = new List<int>();
		foreach (var (di, dj) in NextsDelta)
		{
			var (ni, nj) = (i + di, j + dj);
			if (0 <= ni && ni < h && 0 <= nj && nj < w && s[ni][nj] != wall)
			{
				if ((ni, nj) == (ai, aj))
				{
					var (nai, naj) = (ni + di, nj + dj);
					if (0 <= nai && nai < h && 0 <= naj && naj < w && s[nai][naj] != wall)
					{
						l.Add((w * nai + naj) * n + av);
					}
				}
				else
				{
					l.Add(av * n + w * ni + nj);
				}
			}
		}
		return l;
	}
}
