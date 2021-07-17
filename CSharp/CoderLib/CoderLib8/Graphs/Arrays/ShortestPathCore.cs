using System;
using System.Collections.Generic;
using CoderLib8.DataTrees;

namespace CoderLib8.Graphs.Arrays
{
	// テンプレートとして使えます。
	public static class ShortestPathCore
	{
		// 経路の有無のみを判定する場合は、DFS を使います。
		public static bool[] Dfs(int n, Func<int, int[]> nexts, int sv, int ev = -1)
		{
			var u = new bool[n];
			var q = new Stack<int>();
			u[sv] = true;
			q.Push(sv);

			while (q.Count > 0)
			{
				var v = q.Pop();

				foreach (var nv in nexts(v))
				{
					if (u[nv]) continue;
					u[nv] = true;
					if (nv == ev) return u;
					q.Push(nv);
				}
			}
			return u;
		}

		public static long[] Bfs(int n, Func<int, int[]> nexts, int sv, int ev = -1)
		{
			var costs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
			var q = new Queue<int>();
			costs[sv] = 0;
			q.Enqueue(sv);

			while (q.Count > 0)
			{
				var v = q.Dequeue();
				var nc = costs[v] + 1;

				foreach (var nv in nexts(v))
				{
					if (costs[nv] <= nc) continue;
					costs[nv] = nc;
					if (nv == ev) return costs;
					q.Enqueue(nv);
				}
			}
			return costs;
		}

		public static long[] Dijkstra(int n, Func<int, int[][]> nexts, int sv, int ev = -1)
		{
			var costs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
			var q = PriorityQueue<int>.CreateWithKey(v => costs[v]);
			costs[sv] = 0;
			q.Push(sv);

			while (q.Any)
			{
				var (v, c) = q.Pop();
				if (v == ev) break;
				if (costs[v] < c) continue;

				foreach (var e in nexts(v))
				{
					var (nv, nc) = (e[1], c + e[2]);
					if (costs[nv] <= nc) continue;
					costs[nv] = nc;
					q.Push(nv);
				}
			}
			return costs;
		}

		public static long[] BfsMod(int mod, int n, Func<int, int[][]> nexts, int sv, int ev = -1)
		{
			var costs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
			var qs = Array.ConvertAll(new bool[mod], _ => new Queue<int>());
			costs[sv] = 0;
			qs[0].Enqueue(sv);

			for (long c = 0; Array.Exists(qs, q => q.Count > 0); ++c)
			{
				var q = qs[c % mod];
				while (q.Count > 0)
				{
					var v = q.Dequeue();
					if (v == ev) return costs;
					if (costs[v] < c) continue;

					foreach (var e in nexts(v))
					{
						var (nv, nc) = (e[1], c + e[2]);
						if (costs[nv] <= nc) continue;
						costs[nv] = nc;
						qs[nc % mod].Enqueue(nv);
					}
				}
			}
			return costs;
		}
	}
}
