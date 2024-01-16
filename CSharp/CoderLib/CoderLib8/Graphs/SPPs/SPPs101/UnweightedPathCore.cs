using System;
using System.Collections.Generic;

namespace CoderLib8.Graphs.SPPs.SPPs101
{
	// テンプレートとして使えます。
	public static class UnweightedPathCore
	{
		// 最短経路とは限りません。
		// 連結性のみを判定する場合は、DFS、BFS または Union-Find を利用します。
		public static bool[] ConnectivityByDFS(int n, Func<int, int[]> nexts, int sv, int ev = -1)
		{
			var u = new bool[n];
			u[sv] = true;
			DFS(sv);
			return u;

			bool DFS(int v)
			{
				if (v == ev) return true;
				foreach (var nv in nexts(v))
				{
					if (u[nv]) continue;
					u[nv] = true;
					if (DFS(nv)) return true;
				}
				return false;
			}
		}

		public static bool[] ConnectivityByDFS0(int n, Func<int, int[]> nexts, int sv, int ev = -1)
		{
			var u = new bool[n];
			var q = new Stack<int>();
			u[sv] = true;
			q.Push(sv);

			while (q.Count > 0)
			{
				var v = q.Pop();
				if (v == ev) return u;

				foreach (var nv in nexts(v))
				{
					if (u[nv]) continue;
					u[nv] = true;
					q.Push(nv);
				}
			}
			return u;
		}

		public static long[] ShortestByBFS(int n, Func<int, int[]> nexts, int sv, int ev = -1, long maxCost = long.MaxValue)
		{
			var costs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
			var q = new Queue<int>();
			costs[sv] = 0;
			q.Enqueue(sv);

			while (q.Count > 0)
			{
				var v = q.Dequeue();
				if (v == ev) return costs;
				var nc = costs[v] + 1;
				if (nc > maxCost) return costs;

				foreach (var nv in nexts(v))
				{
					if (costs[nv] <= nc) continue;
					costs[nv] = nc;
					q.Enqueue(nv);
				}
			}
			return costs;
		}
	}
}
