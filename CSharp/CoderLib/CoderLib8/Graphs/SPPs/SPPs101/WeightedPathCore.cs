using System;
using System.Collections.Generic;
using CoderLib8.DataTrees;

namespace CoderLib8.Graphs.SPPs.SPPs101
{
	// テンプレートとして使えます。
	public static class WeightedPathCore
	{
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
