﻿using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.DataTrees;

// Test: https://atcoder.jp/contests/abc237/tasks/abc237_e
namespace CoderLib8.Graphs.Typed
{
	public class SppWeightedGraph<TVertex>
	{
		public struct Edge
		{
			public TVertex From, To;
			public long Cost;
			public Edge(TVertex from, TVertex to, long cost) { From = from; To = to; Cost = cost; }
			public Edge GetReverse() => new Edge(To, From, Cost);
		}

		static readonly Edge[] EmptyEdges = new Edge[0];

		Dictionary<TVertex, List<Edge>> map = new Dictionary<TVertex, List<Edge>>();

		public Dictionary<TVertex, Edge[]> GetMap() => map.ToDictionary(p => p.Key, p => p.Value.ToArray());

		public void AddEdge(TVertex from, TVertex to, long cost, bool directed) => AddEdge(new Edge(from, to, cost), directed);
		public void AddEdge(Edge e, bool directed)
		{
			var l = map.ContainsKey(e.From) ? map[e.From] : (map[e.From] = new List<Edge>());
			l.Add(e);

			if (directed) return;
			l = map.ContainsKey(e.To) ? map[e.To] : (map[e.To] = new List<Edge>());
			l.Add(e.GetReverse());
		}

		public void AddEdges(IEnumerable<Edge> es, bool directed)
		{
			foreach (var e in es) AddEdge(e, directed);
		}

		public Dictionary<TVertex, long> Dijkstra(TVertex sv, TVertex ev) => Dijkstra(v => map.ContainsKey(v) ? map[v].ToArray() : EmptyEdges, sv, ev);

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
