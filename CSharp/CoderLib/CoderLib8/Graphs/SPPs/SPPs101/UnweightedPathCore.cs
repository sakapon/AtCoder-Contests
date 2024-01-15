using System;
using System.Collections.Generic;

namespace CoderLib8.Graphs.SPPs.SPPs101
{
	// テンプレートとして使えます。
	public static class UnweightedPathCore
	{
		// 経路の有無のみを判定する場合は、DFS を使います。最短とは限りません。
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
	}
}
