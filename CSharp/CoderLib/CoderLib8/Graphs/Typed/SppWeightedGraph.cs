using System;
using System.Collections.Generic;
using CoderLib8.DataTrees;

namespace CoderLib8.Graphs.Typed
{
	public class SppWeightedGraph<TVertex>
	{
		public struct Edge
		{
			public TVertex From, To;
			public long Cost;
			public Edge(TVertex from, TVertex to, long cost) { From = from; To = to; Cost = cost; }
		}

		public Dictionary<TVertex, List<Edge>> Map = new Dictionary<TVertex, List<Edge>>();

		public void AddEdge(TVertex from, TVertex to, long cost, bool directed)
		{
			if (!Map.ContainsKey(from)) Map[from] = new List<Edge>();
			Map[from].Add(new Edge(from, to, cost));

			if (directed) return;
			if (!Map.ContainsKey(to)) Map[to] = new List<Edge>();
			Map[to].Add(new Edge(to, from, cost));
		}

		static readonly Edge[] EmptyEdges = new Edge[0];
		public Dictionary<TVertex, long> Dijkstra(TVertex sv, TVertex ev) => Dijkstra(v => Map.ContainsKey(v) ? Map[v].ToArray() : EmptyEdges, sv, ev);

		// 終点を指定しないときは、ev に null, -1 などを指定します。
		public static Dictionary<TVertex, long> Dijkstra(Func<TVertex, Edge[]> nexts, TVertex sv, TVertex ev)
		{
			var comp = EqualityComparer<TVertex>.Default;
			var costs = new Dictionary<TVertex, long>();
			var q = PriorityQueue<TVertex>.CreateWithKey(v => costs[v]);
			costs[sv] = 0;
			q.Push(sv);

			while (q.Any)
			{
				var (v, c) = q.Pop();
				if (comp.Equals(v, ev)) break;
				if (costs[v] < c) continue;

				foreach (var e in nexts(v))
				{
					var (nv, nc) = (e.To, c + e.Cost);
					if (costs.ContainsKey(nv) && costs[nv] <= nc) continue;
					costs[nv] = nc;
					q.Push(nv);
				}
			}
			return costs;
		}
	}
}
