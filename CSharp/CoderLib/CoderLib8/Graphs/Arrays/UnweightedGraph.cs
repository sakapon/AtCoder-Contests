using System;
using System.Collections.Generic;

namespace CoderLib8.Graphs.Arrays
{
	public abstract class UnweightedGraph
	{
		public static int[][] ToMap(int n, int[][] es, bool directed) => Array.ConvertAll(ToMapList(n, es, directed), l => l.ToArray());
		public static List<int>[] ToMapList(int n, int[][] es, bool directed)
		{
			var map = Array.ConvertAll(new bool[n], _ => new List<int>());
			foreach (var e in es)
			{
				map[e[0]].Add(e[1]);
				if (!directed) map[e[1]].Add(e[0]);
			}
			return map;
		}

		// 辺 ID
		public static List<int[]>[] ToMapListWithId(int n, int[][] es, bool directed)
		{
			var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
			for (int i = 0; i < es.Length; ++i)
			{
				var e = es[i];
				map[e[0]].Add(new[] { e[0], e[1], i });
				if (!directed) map[e[1]].Add(new[] { e[1], e[0], i });
			}
			return map;
		}

		// 辺 ID
		public static List<int>[] ToMapListById(int n, int[][] es, bool directed)
		{
			var map = Array.ConvertAll(new bool[n], _ => new List<int>());
			for (int i = 0; i < es.Length; ++i)
			{
				var e = es[i];
				map[e[0]].Add(i);
				if (!directed) map[e[1]].Add(i);
			}
			return map;
		}
	}

	public class UnweightedEdgesGraph
	{
		int n;
		int[][] edges;

		public UnweightedEdgesGraph(int n, int[][] edges)
		{
			this.n = n;
			this.edges = edges;
		}

		public List<int>[] ToMap(bool directed)
		{
			var map = Array.ConvertAll(new bool[n], _ => new List<int>());
			foreach (var e in edges)
			{
				map[e[0]].Add(e[1]);
				if (!directed) map[e[1]].Add(e[0]);
			}
			return map;
		}

		public List<int>[] ToReverseMap()
		{
			var map = Array.ConvertAll(new bool[n], _ => new List<int>());
			foreach (var e in edges)
			{
				map[e[1]].Add(e[0]);
			}
			return map;
		}
	}
}
