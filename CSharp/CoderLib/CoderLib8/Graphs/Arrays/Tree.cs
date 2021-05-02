using System;
using System.Collections.Generic;

namespace CoderLib8.Graphs.Arrays
{
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_z
	public class Tree
	{
		static List<int>[] ToMap(int n, int[][] es, bool directed)
		{
			var map = Array.ConvertAll(new bool[n], _ => new List<int>());
			foreach (var e in es)
			{
				map[e[0]].Add(e[1]);
				if (!directed) map[e[1]].Add(e[0]);
			}
			return map;
		}

		public List<int>[] Map { get; }
		public int[] Depths { get; }
		public int[] Parents { get; }

		public Tree(int n, int root, int[][] ues) : this(n, root, ToMap(n, ues, false)) { }
		public Tree(int n, int root, List<int>[] map)
		{
			Map = map;
			Depths = Array.ConvertAll(Map, _ => -1);
			Parents = Array.ConvertAll(Map, _ => -1);

			Depths[root] = 0;
			Dfs(root, -1);

			void Dfs(int v, int pv)
			{
				foreach (var nv in Map[v])
				{
					if (nv == pv) continue;
					Depths[nv] = Depths[v] + 1;
					Parents[nv] = v;
					Dfs(nv, v);
				}
			}
		}
	}
}
