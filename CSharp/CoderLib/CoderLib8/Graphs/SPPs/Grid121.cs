using System;
using System.Collections.Generic;

namespace CoderLib8.Graphs.SPPs.Arrays.PathCore121
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
}
