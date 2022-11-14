using System;
using System.Collections.Generic;

// Test: https://atcoder.jp/contests/atc001/tasks/dfs_a
// Test: https://atcoder.jp/contests/atc002/tasks/abc007_3
namespace CoderLib8.Graphs.Arrays
{
	public class GeneralGrid
	{
		readonly int h, w;
		public int Height => h;
		public int Width => w;
		public GeneralGrid(int h, int w) { this.h = h; this.w = w; }

		public int ToVertexId(int i, int j) => w * i + j;
		public (int i, int j) FromVertexId(int v) => (v / w, v % w);

		public int[] GetNexts(int v)
		{
			var (i, j) = (v / w, v % w);
			var l = new List<int>();
			if (j > 0) l.Add(v - 1);
			if (j + 1 < w) l.Add(v + 1);
			if (i > 0) l.Add(v - w);
			if (i + 1 < h) l.Add(v + w);
			return l.ToArray();
		}
	}

	public class IntGrid
	{
		readonly int h, w;
		readonly int[][] s;
		public int Height => h;
		public int Width => w;
		public int[][] Raw => s;

		public IntGrid(int[][] s)
		{
			h = s.Length;
			w = s[0].Length;
			this.s = s;
		}

		public int ToVertexId(int i, int j) => w * i + j;
		public (int i, int j) FromVertexId(int v) => (v / w, v % w);

		public int[][] GetWeightedNexts(int v)
		{
			var (i, j) = (v / w, v % w);
			var l = new List<int[]>();
			if (j > 0) l.Add(new[] { v, v - 1, s[i][j - 1] });
			if (j + 1 < w) l.Add(new[] { v, v + 1, s[i][j + 1] });
			if (i > 0) l.Add(new[] { v, v - w, s[i - 1][j] });
			if (i + 1 < h) l.Add(new[] { v, v + w, s[i + 1][j] });
			return l.ToArray();
		}
	}

	public class CharGrid
	{
		readonly int h, w;
		readonly char[][] s;
		readonly char wall;
		public int Height => h;
		public int Width => w;
		public char[][] Raw => s;

		public CharGrid(char[][] s, char wall = '#')
		{
			h = s.Length;
			w = s[0].Length;
			this.s = s;
			this.wall = wall;
		}
		public CharGrid(string[] s, char wall = '#') : this(Array.ConvertAll(s, l => l.ToCharArray()), wall) { }

		public int ToVertexId(int i, int j) => w * i + j;
		public (int i, int j) FromVertexId(int v) => (v / w, v % w);

		public int[] GetNexts(int v)
		{
			var (i, j) = (v / w, v % w);
			var l = new List<int>();
			if (j > 0 && s[i][j - 1] != wall) l.Add(v - 1);
			if (j + 1 < w && s[i][j + 1] != wall) l.Add(v + 1);
			if (i > 0 && s[i - 1][j] != wall) l.Add(v - w);
			if (i + 1 < h && s[i + 1][j] != wall) l.Add(v + w);
			return l.ToArray();
		}

		// 1 桁の整数が設定されている場合
		public int[][] GetWeightedNexts(int v)
		{
			var (i, j) = (v / w, v % w);
			var l = new List<int[]>();
			if (j > 0 && s[i][j - 1] != wall) l.Add(new[] { v, v - 1, s[i][j - 1] - '0' });
			if (j + 1 < w && s[i][j + 1] != wall) l.Add(new[] { v, v + 1, s[i][j + 1] - '0' });
			if (i > 0 && s[i - 1][j] != wall) l.Add(new[] { v, v - w, s[i - 1][j] - '0' });
			if (i + 1 < h && s[i + 1][j] != wall) l.Add(new[] { v, v + w, s[i + 1][j] - '0' });
			return l.ToArray();
		}
	}

	public static class UnweightedGridHelper
	{
		// undirected
		public static List<int>[] GetAdjacencyList(int h, int w)
		{
			var map = Array.ConvertAll(new bool[h * w], _ => new List<int>());
			for (int i = 0; i < h; ++i)
				for (int j = 1; j < w; ++j)
				{
					var v = w * i + j;
					map[v].Add(v - 1);
					map[v - 1].Add(v);
				}
			for (int j = 0; j < w; ++j)
				for (int i = 1; i < h; ++i)
				{
					var v = w * i + j;
					map[v].Add(v - w);
					map[v - w].Add(v);
				}
			return map;
		}

		// undirected
		public static List<int>[] GetAdjacencyList(int h, int w, string[] s, char wall = '#')
		{
			var map = Array.ConvertAll(new bool[h * w], _ => new List<int>());
			for (int i = 0; i < h; ++i)
				for (int j = 1; j < w; ++j)
				{
					if (s[i][j] == wall || s[i][j - 1] == wall) continue;
					var v = w * i + j;
					map[v].Add(v - 1);
					map[v - 1].Add(v);
				}
			for (int j = 0; j < w; ++j)
				for (int i = 1; i < h; ++i)
				{
					if (s[i][j] == wall || s[i - 1][j] == wall) continue;
					var v = w * i + j;
					map[v].Add(v - w);
					map[v - w].Add(v);
				}
			return map;
		}
	}

	public static class WeightedGridHelper
	{
		// undirected
		public static List<int[]>[] GetAdjacencyList(int h, int w, int[][] s)
		{
			var map = Array.ConvertAll(new bool[h * w], _ => new List<int[]>());
			for (int i = 0; i < h; ++i)
				for (int j = 1; j < w; ++j)
				{
					var v = w * i + j;
					map[v].Add(new[] { v, v - 1, s[i][j - 1] });
					map[v - 1].Add(new[] { v - 1, v, s[i][j] });
				}
			for (int j = 0; j < w; ++j)
				for (int i = 1; i < h; ++i)
				{
					var v = w * i + j;
					map[v].Add(new[] { v, v - w, s[i - 1][j] });
					map[v - w].Add(new[] { v - w, v, s[i][j] });
				}
			return map;
		}

		// undirected
		public static List<int[]>[] GetAdjacencyList(int h, int w, string[] s, char wall = '#')
		{
			var map = Array.ConvertAll(new bool[h * w], _ => new List<int[]>());
			for (int i = 0; i < h; ++i)
				for (int j = 1; j < w; ++j)
				{
					if (s[i][j] == wall || s[i][j - 1] == wall) continue;
					var v = w * i + j;
					map[v].Add(new[] { v, v - 1, s[i][j - 1] - '0' });
					map[v - 1].Add(new[] { v - 1, v, s[i][j] - '0' });
				}
			for (int j = 0; j < w; ++j)
				for (int i = 1; i < h; ++i)
				{
					if (s[i][j] == wall || s[i - 1][j] == wall) continue;
					var v = w * i + j;
					map[v].Add(new[] { v, v - w, s[i - 1][j] - '0' });
					map[v - w].Add(new[] { v - w, v, s[i][j] - '0' });
				}
			return map;
		}
	}
}
