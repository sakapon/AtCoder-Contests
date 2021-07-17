using System;
using System.Collections.Generic;

namespace CoderLib8.Graphs.Int.Old
{
	// パスの復元が必要となる場合は、Map プロパティを使用します。
	public class MaxFlowFF
	{
		public class Edge
		{
			public int From, To, RevIndex;
			public long Capacity;
			public Edge(int from, int to, long capacity, int revIndex) { From = from; To = to; Capacity = capacity; RevIndex = revIndex; }
		}

		List<Edge>[] map;
		public Edge[][] Map;
		bool[] u;

		public MaxFlowFF(int n)
		{
			map = Array.ConvertAll(new bool[n], _ => new List<Edge>());
			u = new bool[n];
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

		long Dfs(int v, int ev, long fMin)
		{
			if (v == ev) return fMin;
			u[v] = true;
			foreach (var e in Map[v])
			{
				if (u[e.To] || e.Capacity == 0) continue;
				var delta = Dfs(e.To, ev, Math.Min(fMin, e.Capacity));
				if (delta > 0)
				{
					e.Capacity -= delta;
					Map[e.To][e.RevIndex].Capacity += delta;
					u[v] = false;
					return delta;
				}
			}
			u[v] = false;
			return 0;
		}

		public long FordFulkerson(int sv, int ev)
		{
			Map = Array.ConvertAll(map, l => l.ToArray());

			long M = 0, t;
			while ((t = Dfs(sv, ev, long.MaxValue)) > 0) M += t;
			return M;
		}
	}
}
