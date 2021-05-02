using System;
using System.Collections.Generic;

namespace CoderLib8.Graphs.Arrays
{
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_c
	public class WeightedTree
	{
		static List<int[]>[] ToMap(int n, int[][] es, bool directed)
		{
			var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
			foreach (var e in es)
			{
				map[e[0]].Add(e);
				if (!directed) map[e[1]].Add(new[] { e[1], e[0], e[2] });
			}
			return map;
		}

		public List<int[]>[] Map { get; }
		public int[] Depths { get; }
		public long[] Costs { get; }
		public int[] Parents { get; }

		public WeightedTree(int n, int root, int[][] ues) : this(n, root, ToMap(n, ues, false)) { }
		public WeightedTree(int n, int root, List<int[]>[] map)
		{
			Map = map;
			Depths = Array.ConvertAll(Map, _ => -1);
			Costs = Array.ConvertAll(Map, _ => -1L);
			Parents = Array.ConvertAll(Map, _ => -1);

			Depths[root] = 0;
			Costs[root] = 0;
			Dfs(root, -1);

			void Dfs(int v, int pv)
			{
				foreach (var e in Map[v])
				{
					var nv = e[1];
					if (nv == pv) continue;
					Depths[nv] = Depths[v] + 1;
					Costs[nv] = Costs[v] + e[2];
					Parents[nv] = v;
					Dfs(nv, v);
				}
			}
		}
	}
}
