using System;
using System.Collections.Generic;

// Test: https://atcoder.jp/contests/atc001/tasks/dfs_a
// Test: https://atcoder.jp/contests/atc002/tasks/abc007_3
namespace CoderLib8.Graphs.Arrays
{
	public static class UnweightedGridHelper
	{
		// undirected
		public static List<int>[] GetAdjacencyList(int h, int w)
		{
			var map = Array.ConvertAll(new bool[h * w], _ => new List<int>());
			for (int i = 0; i < h; ++i)
				for (int j = 1; j < w; ++j)
				{
					var v = i * w + j;
					map[v].Add(v - 1);
					map[v - 1].Add(v);
				}
			for (int j = 0; j < w; ++j)
				for (int i = 1; i < h; ++i)
				{
					var v = i * w + j;
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
					var v = i * w + j;
					if (s[i][j] == wall || s[i][j - 1] == wall) continue;
					map[v].Add(v - 1);
					map[v - 1].Add(v);
				}
			for (int j = 0; j < w; ++j)
				for (int i = 1; i < h; ++i)
				{
					var v = i * w + j;
					if (s[i][j] == wall || s[i - 1][j] == wall) continue;
					map[v].Add(v - w);
					map[v - w].Add(v);
				}
			return map;
		}
	}
}
