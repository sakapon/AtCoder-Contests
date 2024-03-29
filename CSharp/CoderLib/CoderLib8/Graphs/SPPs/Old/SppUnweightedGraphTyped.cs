﻿using System;
using System.Collections.Generic;
using System.Linq;

// Test: https://atcoder.jp/contests/abc204/tasks/abc204_c
// Test: https://atcoder.jp/contests/abc238/tasks/abc238_e
namespace CoderLib8.Graphs.Typed
{
	public class SppUnweightedGraph<TVertex>
	{
		static readonly TVertex[] EmptyVertexes = new TVertex[0];

		Dictionary<TVertex, List<TVertex>> map = new Dictionary<TVertex, List<TVertex>>();

		public Dictionary<TVertex, TVertex[]> GetMap() => map.ToDictionary(p => p.Key, p => p.Value.ToArray());

		public void AddEdge(TVertex[] e, bool directed) => AddEdge(e[0], e[1], directed);
		public void AddEdge(TVertex from, TVertex to, bool directed)
		{
			var l = map.ContainsKey(from) ? map[from] : (map[from] = new List<TVertex>());
			l.Add(to);

			if (directed) return;
			l = map.ContainsKey(to) ? map[to] : (map[to] = new List<TVertex>());
			l.Add(from);
		}

		public void AddEdges(IEnumerable<TVertex[]> es, bool directed)
		{
			foreach (var e in es) AddEdge(e[0], e[1], directed);
		}

		public HashSet<TVertex> ConnectionByDfs(TVertex sv, TVertex ev) => ConnectionByDfs(v => map.ContainsKey(v) ? map[v].ToArray() : EmptyVertexes, sv, ev);

		// 終点を指定しないときは、ev に null, -1 などを指定します。
		public static HashSet<TVertex> ConnectionByDfs(Func<TVertex, TVertex[]> nexts, TVertex sv, TVertex ev)
		{
			var comp = EqualityComparer<TVertex>.Default;
			var u = new HashSet<TVertex>();
			var q = new Stack<TVertex>();
			u.Add(sv);
			q.Push(sv);

			while (q.Count > 0)
			{
				var v = q.Pop();

				foreach (var nv in nexts(v))
				{
					if (u.Contains(nv)) continue;
					u.Add(nv);
					if (comp.Equals(nv, ev)) return u;
					q.Push(nv);
				}
			}
			return u;
		}

		public static HashSet<TVertex> ConnectionByDfs2(Func<TVertex, TVertex[]> nexts, TVertex sv, TVertex ev)
		{
			var comp = EqualityComparer<TVertex>.Default;
			var u = new HashSet<TVertex>();
			u.Add(sv);
			Dfs(sv);
			return u;

			bool Dfs(TVertex v)
			{
				foreach (var nv in nexts(v))
				{
					if (u.Contains(nv)) continue;
					u.Add(nv);
					if (comp.Equals(nv, ev)) return true;
					if (Dfs(nv)) return true;
				}
				return false;
			}
		}
	}
}
