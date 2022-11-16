using System;
using System.Collections.Generic;

namespace CoderLib8.Graphs.SPPs.Arrays.Grid111
{
	// undirected
	public class GeneralGrid
	{
		protected readonly int h, w;
		public int Height => h;
		public int Width => w;
		public int VertexesCount => h * w;
		public GeneralGrid(int h, int w) { this.h = h; this.w = w; }

		public int ToVertexId(int i, int j) => w * i + j;
		public (int i, int j) FromVertexId(int v) => (v / w, v % w);

		public int[] GetAllNexts(int v)
		{
			var (i, j) = (v / w, v % w);
			var l = new List<int>();
			if (j > 0) l.Add(v - 1);
			if (j + 1 < w) l.Add(v + 1);
			if (i > 0) l.Add(v - w);
			if (i + 1 < h) l.Add(v + w);
			return l.ToArray();
		}

		public List<int>[] GetAllAdjacencyList()
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
			if (j > 0) l.Add(new[] { v, v - 1, s[i][j - 1] });
			if (j + 1 < w) l.Add(new[] { v, v + 1, s[i][j + 1] });
			if (i > 0) l.Add(new[] { v, v - w, s[i - 1][j] });
			if (i + 1 < h) l.Add(new[] { v, v + w, s[i + 1][j] });
			return l.ToArray();
		}

		public List<int[]>[] GetWeightedAdjacencyList()
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

		public List<int>[] GetUnweightedAdjacencyList()
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

		// 1 桁の整数が設定されている場合
		public List<int[]>[] GetWeightedAdjacencyList()
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
