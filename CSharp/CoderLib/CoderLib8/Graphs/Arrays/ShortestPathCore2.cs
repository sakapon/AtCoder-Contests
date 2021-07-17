using System;
using System.Collections.Generic;
using CoderLib8.DataTrees;

namespace CoderLib8.Graphs.Arrays
{
	// 経路復元を含んでいます。
	// テンプレートとして使えます。
	public static class ShortestPathCore2
	{
		// 経路の有無のみを判定する場合は、DFS を使います。
		public static (bool[] conns, int[] prevs) Dfs(int n, Func<int, int[]> nexts, int sv, int ev = -1)
		{
			var u = new bool[n];
			var prevs = Array.ConvertAll(u, _ => -1);
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
					prevs[nv] = v;
					if (nv == ev) return (u, prevs);
					q.Push(nv);
				}
			}
			return (u, prevs);
		}

		public static (long[] costs, int[] prevs) Bfs(int n, Func<int, int[]> nexts, int sv, int ev = -1)
		{
			var costs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
			var prevs = Array.ConvertAll(costs, _ => -1);
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
					prevs[nv] = v;
					if (nv == ev) return (costs, prevs);
					q.Enqueue(nv);
				}
			}
			return (costs, prevs);
		}

		public static (long[] costs, int[][] prevs) Dijkstra(int n, Func<int, int[][]> nexts, int sv, int ev = -1)
		{
			var costs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
			var prevs = new int[n][];
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
					prevs[nv] = e;
					q.Push(nv);
				}
			}
			return (costs, prevs);
		}

		public static (long[] costs, int[][] prevs) BfsMod(int mod, int n, Func<int, int[][]> nexts, int sv, int ev = -1)
		{
			var costs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
			var prevs = new int[n][];
			var qs = Array.ConvertAll(new bool[mod], _ => new Queue<int>());
			costs[sv] = 0;
			qs[0].Enqueue(sv);

			for (long c = 0; Array.Exists(qs, q => q.Count > 0); ++c)
			{
				var q = qs[c % mod];
				while (q.Count > 0)
				{
					var v = q.Dequeue();
					if (v == ev) return (costs, prevs);
					if (costs[v] < c) continue;

					foreach (var e in nexts(v))
					{
						var (nv, nc) = (e[1], c + e[2]);
						if (costs[nv] <= nc) continue;
						costs[nv] = nc;
						prevs[nv] = e;
						qs[nc % mod].Enqueue(nv);
					}
				}
			}
			return (costs, prevs);
		}
	}
}
