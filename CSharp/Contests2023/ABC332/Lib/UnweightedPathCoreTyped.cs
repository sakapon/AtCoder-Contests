using System;
using System.Collections.Generic;

namespace CoderLib8.Graphs.SPPs.SPPs101
{
	// テンプレートとして使えます。
	public static class UnweightedPathCoreTyped
	{
		// 最短経路とは限りません。
		// 連結性のみを判定する場合は、DFS、BFS または Union-Find を利用します。
		public static HashSet<T> ConnectivityByDFS<T>(Func<T, T[]> nexts, T sv, T ev)
		{
			var u = new HashSet<T>();
			u.Add(sv);
			DFS(sv);
			return u;

			bool DFS(T v)
			{
				if (u.Comparer.Equals(v, ev)) return true;
				foreach (var nv in nexts(v))
				{
					if (u.Contains(nv)) continue;
					u.Add(nv);
					if (DFS(nv)) return true;
				}
				return false;
			}
		}

		public static HashSet<T> ConnectivityByDFS0<T>(Func<T, T[]> nexts, T sv, T ev)
		{
			var u = new HashSet<T>();
			var q = new Stack<T>();
			u.Add(sv);
			q.Push(sv);

			while (q.Count > 0)
			{
				var v = q.Pop();
				if (u.Comparer.Equals(v, ev)) return u;

				foreach (var nv in nexts(v))
				{
					if (u.Contains(nv)) continue;
					u.Add(nv);
					q.Push(nv);
				}
			}
			return u;
		}

		public static Dictionary<T, long> ShortestByBFS<T>(Func<T, T[]> nexts, T sv, T ev)
		{
			var costs = new Dictionary<T, long>();
			var q = new Queue<T>();
			costs[sv] = 0;
			q.Enqueue(sv);

			while (q.Count > 0)
			{
				var v = q.Dequeue();
				if (costs.Comparer.Equals(v, ev)) return costs;
				var nc = costs[v] + 1;

				foreach (var nv in nexts(v))
				{
					if (costs.ContainsKey(nv)) continue;
					costs[nv] = nc;
					q.Enqueue(nv);
				}
			}
			return costs;
		}
	}
}
