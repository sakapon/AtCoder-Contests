using System;
using System.Collections.Generic;

namespace CoderLib8.Graphs.SPPs.Arrays.PathCore111
{
	public static class PathCore
	{
		public static T[][] ToArrays<T>(this List<T>[] map) => Array.ConvertAll(map, l => l.ToArray());

		public static int[][] ToMap(int n, int[][] es, bool twoWay) => ToListMap(n, es, twoWay).ToArrays();
		public static List<int>[] ToListMap(int n, int[][] es, bool twoWay)
		{
			var map = Array.ConvertAll(new bool[n], _ => new List<int>());
			foreach (var e in es)
			{
				map[e[0]].Add(e[1]);
				if (twoWay) map[e[1]].Add(e[0]);
			}
			return map;
		}

		public static int[][][] ToWeightedMap(int n, int[][] es, bool twoWay) => ToWeightedListMap(n, es, twoWay).ToArrays();
		public static List<int[]>[] ToWeightedListMap(int n, int[][] es, bool twoWay)
		{
			var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
			foreach (var e in es)
			{
				map[e[0]].Add(e);
				if (twoWay) map[e[1]].Add(new[] { e[1], e[0], e[2] });
			}
			return map;
		}

		// 最短経路とは限りません。
		// 連結性のみを判定する場合は、DFS または Union-Find を利用します。
		public static bool[] ConnectivityByDFS(this int[][] map, int sv, int ev = -1)
		{
			var n = map.Length;
			var u = new bool[n];
			var q = new Stack<int>();
			u[sv] = true;
			q.Push(sv);

			while (q.Count > 0)
			{
				var v = q.Pop();
				if (v == ev) return u;

				foreach (var nv in map[v])
				{
					if (u[nv]) continue;
					u[nv] = true;
					q.Push(nv);
				}
			}
			return u;
		}

		public static long[] ShortestByBFS(this int[][] map, int sv, int ev = -1)
		{
			var n = map.Length;
			var costs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
			var q = new Queue<int>();
			costs[sv] = 0;
			q.Enqueue(sv);

			while (q.Count > 0)
			{
				var v = q.Dequeue();
				if (v == ev) return costs;
				var nc = costs[v] + 1;

				foreach (var nv in map[v])
				{
					if (costs[nv] <= nc) continue;
					costs[nv] = nc;
					q.Enqueue(nv);
				}
			}
			return costs;
		}

		public static long[] Dijkstra(this int[][][] map, int sv, int ev = -1)
		{
			var n = map.Length;
			var costs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
			costs[sv] = 0;
			var q = new SortedSet<(long, int)> { (0, sv) };

			while (q.Count > 0)
			{
				var (c, v) = q.Min;
				q.Remove((c, v));
				if (v == ev) return costs;

				foreach (var e in map[v])
				{
					var (nv, nc) = (e[1], c + e[2]);
					if (costs[nv] <= nc) continue;
					if (costs[nv] != long.MaxValue) q.Remove((costs[nv], nv));
					q.Add((nc, nv));
					costs[nv] = nc;
				}
			}
			return costs;
		}

		// Dijkstra 法の特別な場合です。
		public static long[] ShortestByModBFS(this int[][][] map, int mod, int sv, int ev = -1)
		{
			var n = map.Length;
			var costs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
			var qs = Array.ConvertAll(new bool[mod], _ => new Queue<int>());
			costs[sv] = 0;
			qs[0].Enqueue(sv);
			var qc = 1;

			for (long c = 0; qc > 0; ++c)
			{
				var q = qs[c % mod];
				while (q.Count > 0)
				{
					var v = q.Dequeue();
					--qc;
					if (v == ev) return costs;
					if (costs[v] < c) continue;

					foreach (var e in map[v])
					{
						var (nv, nc) = (e[1], c + e[2]);
						if (costs[nv] <= nc) continue;
						costs[nv] = nc;
						qs[nc % mod].Enqueue(nv);
						++qc;
					}
				}
			}
			return costs;
		}
	}
}
