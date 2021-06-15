﻿using System;
using System.Collections.Generic;

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
	}
}
