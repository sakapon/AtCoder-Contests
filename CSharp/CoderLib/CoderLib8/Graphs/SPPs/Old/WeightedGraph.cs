using System;
using System.Collections.Generic;

namespace CoderLib8.Graphs.Arrays
{
	public abstract class WeightedGraph
	{
		public static int[][][] ToMap(int n, int[][] es, bool directed) => Array.ConvertAll(ToMapList(n, es, directed), l => l.ToArray());
		public static List<int[]>[] ToMapList(int n, int[][] es, bool directed)
		{
			var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
			foreach (var e in es)
			{
				map[e[0]].Add(e);
				if (!directed) map[e[1]].Add(new[] { e[1], e[0], e[2] });
			}
			return map;
		}
	}
}
