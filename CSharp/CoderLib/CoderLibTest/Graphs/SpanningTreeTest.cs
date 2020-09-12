using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.Trees;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Graphs
{
	[TestClass]
	public class SpanningTreeTest
	{
		static int[][] Kruskal(int n, int[][] es)
		{
			var uf = new UF(n + 1);
			var minEdges = new List<int[]>();

			foreach (var e in es.OrderBy(e => e[2]))
			{
				if (uf.AreUnited(e[0], e[1])) continue;
				uf.Unite(e[0], e[1]);
				minEdges.Add(e);
			}
			return minEdges.ToArray();
		}

		static int[][] Prim(int n, int sv, int[][] es)
		{
			var map = Array.ConvertAll(new int[n + 1], _ => new List<int[]>());
			foreach (var e in es)
			{
				map[e[0]].Add(new[] { e[0], e[1], e[2] });
				map[e[1]].Add(new[] { e[1], e[0], e[2] });
			}

			var u = new bool[n + 1];
			u[sv] = true;
			var pq = PQ<int[]>.Create(e => e[2], map[sv].ToArray());
			var minEdges = new List<int[]>();

			while (pq.Count > 0 && minEdges.Count < n)
			{
				var e = pq.Pop();
				if (u[e[1]]) continue;
				u[e[1]] = true;
				minEdges.Add(e);
				foreach (var ne in map[e[1]])
					if (ne[1] != e[0])
						pq.Push(ne);
			}
			return minEdges.ToArray();
		}
	}
}
