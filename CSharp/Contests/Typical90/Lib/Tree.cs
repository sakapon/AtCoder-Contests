using System;
using System.Collections.Generic;

namespace CoderLib8.Graphs.Arrays
{
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_c
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

		public int Count { get; }
		public int Root { get; }
		public List<int>[] Map { get; }
		public int[] Depths { get; }
		public int[] Parents { get; }

		// この Euler Tour では方向を記録しません。
		// order -> vertex
		public int[] Tour { get; }
		// vertex -> orders
		public List<int>[] TourMap { get; }

		public Tree(int n, int root, int[][] ues) : this(n, root, ToMap(n, ues, false)) { }
		public Tree(int n, int root, List<int>[] map)
		{
			Count = n;
			Root = root;
			Map = map;
			Depths = Array.ConvertAll(Map, _ => -1);
			Parents = Array.ConvertAll(Map, _ => -1);

			var tour = new List<int>();
			TourMap = Array.ConvertAll(Map, _ => new List<int>());

			Depths[root] = 0;
			Dfs(root, -1);

			Tour = tour.ToArray();

			void Dfs(int v, int pv)
			{
				TourMap[v].Add(tour.Count);
				tour.Add(v);

				foreach (var nv in Map[v])
				{
					if (nv == pv) continue;
					Depths[nv] = Depths[v] + 1;
					Parents[nv] = v;
					Dfs(nv, v);

					TourMap[v].Add(tour.Count);
					tour.Add(v);
				}
			}
		}
	}
}
