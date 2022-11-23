using System;
using System.Collections.Generic;

// Test: https://atcoder.jp/contests/atc001/tasks/dfs_a
// Test: https://atcoder.jp/contests/atc002/tasks/abc007_3

// undirected
// 隣接する頂点を、定義された変化量から求めます。
namespace CoderLib8.Graphs.SPPs.Arrays.Grid113
{
	public class GeneralGrid
	{
		protected readonly int h, w;
		public int Height => h;
		public int Width => w;
		public int VertexesCount => h * w;
		public GeneralGrid(int h, int w) { this.h = h; this.w = w; }

		public int ToVertexId(int i, int j) => w * i + j;
		public (int i, int j) FromVertexId(int v) => (v / w, v % w);

		public static (int, int)[] NextsDelta { get; } = new[] { (0, -1), (0, 1), (-1, 0), (1, 0) };
		public static (int, int)[] NextsDelta8 { get; } = new[] { (0, -1), (0, 1), (-1, 0), (1, 0), (-1, -1), (-1, 1), (1, -1), (1, 1) };

		public int[] GetAllNexts(int v)
		{
			var (i, j) = (v / w, v % w);
			var l = new List<int>();
			foreach (var (di, dj) in NextsDelta)
			{
				var (ni, nj) = (i + di, j + dj);
				if (0 <= ni && ni < h && 0 <= nj && nj < w) l.Add(w * ni + nj);
			}
			return l.ToArray();
		}
	}

	public class IntGrid : GeneralGrid
	{
		readonly int[][] s;
		public int[][] Raw => s;
		public int[] this[int i] => s[i];
		public IntGrid(int[][] s) : base(s.Length, s[0].Length) { this.s = s; }

		public int[][] GetWeightedNexts(int v)
		{
			var (i, j) = (v / w, v % w);
			var l = new List<int[]>();
			foreach (var (di, dj) in NextsDelta)
			{
				var (ni, nj) = (i + di, j + dj);
				if (0 <= ni && ni < h && 0 <= nj && nj < w) l.Add(new[] { v, w * ni + nj, s[ni][nj] });
			}
			return l.ToArray();
		}
	}

	public class CharGrid : GeneralGrid
	{
		readonly char[][] s;
		readonly char wall;
		public char[][] Raw => s;
		public char[] this[int i] => s[i];
		public CharGrid(char[][] s, char wall = '#') : base(s.Length, s[0].Length) { this.s = s; this.wall = wall; }
		public CharGrid(string[] s, char wall = '#') : this(ToArrays(s), wall) { }

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

		public int[] GetUnweightedNexts(int v)
		{
			var (i, j) = (v / w, v % w);
			var l = new List<int>();
			foreach (var (di, dj) in NextsDelta)
			{
				var (ni, nj) = (i + di, j + dj);
				if (0 <= ni && ni < h && 0 <= nj && nj < w && s[ni][nj] != wall) l.Add(w * ni + nj);
			}
			return l.ToArray();
		}

		// 1 桁の整数が設定されている場合
		public int[][] GetWeightedNexts(int v)
		{
			var (i, j) = (v / w, v % w);
			var l = new List<int[]>();
			foreach (var (di, dj) in NextsDelta)
			{
				var (ni, nj) = (i + di, j + dj);
				if (0 <= ni && ni < h && 0 <= nj && nj < w) l.Add(new[] { v, w * ni + nj, s[ni][nj] - '0' });
			}
			return l.ToArray();
		}
	}
}
