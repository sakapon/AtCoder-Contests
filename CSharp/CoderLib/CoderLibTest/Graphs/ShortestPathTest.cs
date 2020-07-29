using System;
using System.Collections.Generic;
using System.Linq;
using CoderLibTest.Trees;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Graphs
{
	[TestClass]
	public class ShortestPathTest
	{
		// es: { from, to, weight }
		// 経路: 到達不可能の場合、null を返します。
		// コスト: 到達不可能の場合、MaxValue を返します。
		static int[] Dijkstra(int n, int sv, int ev, int[][] es)
		{
			var map = Array.ConvertAll(new int[n + 1], _ => new List<int[]>());
			foreach (var e in es)
			{
				map[e[0]].Add(new[] { e[1], e[2] });
				map[e[1]].Add(new[] { e[0], e[2] });
			}

			var from = Enumerable.Repeat(-1, n + 1).ToArray();
			var u = Enumerable.Repeat(long.MaxValue, n + 1).ToArray();
			var pq = PQ<int>.Create(v => u[v]);
			u[sv] = 0;
			pq.Push(sv);

			while (pq.Count > 0)
			{
				var v = pq.Pop();
				// すべての頂点を探索する場合、ここを削除します。
				if (v == ev) break;
				foreach (var e in map[v])
				{
					if (u[e[0]] <= u[v] + e[1]) continue;
					from[e[0]] = v;
					u[e[0]] = u[v] + e[1];
					pq.Push(e[0]);
				}
			}

			// コストを求める場合。
			//return u[ev];

			if (from[ev] == -1) return null;
			var path = new List<int>();
			for (var v = ev; v != -1; v = from[v])
				path.Add(v);
			path.Reverse();
			return path.ToArray();
		}
	}
}
