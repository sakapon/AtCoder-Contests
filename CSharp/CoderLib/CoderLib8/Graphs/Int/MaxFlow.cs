using System;
using System.Collections.Generic;

namespace CoderLib8.Graphs.Int
{
	public class MaxFlow
	{
		public class Edge
		{
			public int From, To, RevIndex;
			public long Capacity;
			public Edge(int from, int to, long capacity, int revIndex) { From = from; To = to; Capacity = capacity; RevIndex = revIndex; }
		}

		List<Edge>[] map;
		int[] depth;
		Queue<int> q = new Queue<int>();

		public MaxFlow(int n)
		{
			map = Array.ConvertAll(new bool[n], _ => new List<Edge>());
			depth = new int[n];
		}

		public void AddEdge(int from, int to, long capacity)
		{
			map[from].Add(new Edge(from, to, capacity, map[to].Count));
			map[to].Add(new Edge(to, from, 0, map[from].Count - 1));
		}

		// { from, to, capacity }
		public void AddEdges(int[][] des)
		{
			foreach (var e in des) AddEdge(e[0], e[1], e[2]);
		}
		public void AddEdges(long[][] des)
		{
			foreach (var e in des) AddEdge((int)e[0], (int)e[1], e[2]);
		}

		void Bfs(int sv)
		{
			Array.Fill(depth, int.MaxValue);
			depth[sv] = 0;
			q.Enqueue(sv);

			while (q.Count > 0)
			{
				var v = q.Dequeue();
				var nd = depth[v] + 1;

				foreach (var e in map[v])
				{
					if (e.Capacity == 0) continue;
					if (depth[e.To] <= nd) continue;
					depth[e.To] = nd;
					q.Enqueue(e.To);
				}
			}
		}

		long Dfs(int v, int ev, long fMin)
		{
			if (v == ev) return fMin;
			foreach (var e in map[v])
			{
				if (e.Capacity == 0) continue;
				if (depth[v] >= depth[e.To]) continue;
				var delta = Dfs(e.To, ev, Math.Min(fMin, e.Capacity));
				if (delta > 0)
				{
					e.Capacity -= delta;
					map[e.To][e.RevIndex].Capacity += delta;
					return delta;
				}
			}
			return 0;
		}

		public long Dinic(int sv, int ev)
		{
			long M = 0, t;
			while (true)
			{
				Bfs(sv);
				if (depth[ev] == int.MaxValue) break;
				while ((t = Dfs(sv, ev, long.MaxValue)) > 0) M += t;
			}
			return M;

			// パスの復元が必要となる場合
			//return (M, map);
		}
	}
}
