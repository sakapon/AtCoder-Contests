using System;
using System.Collections.Generic;

// Test: https://atcoder.jp/contests/atc001/tasks/dfs_a
// Test: https://atcoder.jp/contests/atc002/tasks/abc007_3
namespace CoderLib8.Graphs.SPPs.Arrays.PathCore111
{
	// undirected
	public class GridHelper
	{
		readonly int h, w;
		public int Height => h;
		public int Width => w;
		public GridHelper(int h, int w) { this.h = h; this.w = w; }

		public int ToVertexId(int i, int j) => w * i + j;
		public (int i, int j) FromVertexId(int v) => (v / w, v % w);

		public List<int>[] GetAdjacencyList()
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

		public List<int>[] GetAdjacencyList(string[] s, char wall = '#') => GetAdjacencyList(Array.ConvertAll(s, l => l.ToCharArray()), wall);
		public List<int>[] GetAdjacencyList(char[][] s, char wall = '#')
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

		public List<int[]>[] GetWeightedAdjacencyList(int[][] s)
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

		public List<int[]>[] GetWeightedAdjacencyList(string[] s, char wall = '#') => GetWeightedAdjacencyList(Array.ConvertAll(s, l => l.ToCharArray()), wall);
		public List<int[]>[] GetWeightedAdjacencyList(char[][] s, char wall = '#')
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
