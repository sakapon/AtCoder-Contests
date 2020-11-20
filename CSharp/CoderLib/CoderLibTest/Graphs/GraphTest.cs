using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.Graphs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Graphs
{
	[TestClass]
	public class GraphTest
	{
		// キーが含まれない可能性があります。
		static Dictionary<int, int[]> DirectedMap(int[][] es) => es.GroupBy(e => e[0], e => e[1]).ToDictionary(g => g.Key, g => g.ToArray());
		// キーが含まれない可能性があります。
		static Dictionary<int, int[]> UndirectedMap(int[][] es) => es.Concat(es.Select(e => new[] { e[1], e[0] })).GroupBy(e => e[0], e => e[1]).ToDictionary(g => g.Key, g => g.ToArray());

		// 値が null の可能性があります。
		static List<int>[] DirectedMap2(int n, int[][] es)
		{
			var map = new List<int>[n + 1];
			foreach (var e in es)
				if (map[e[0]] == null) map[e[0]] = new List<int> { e[1] };
				else map[e[0]].Add(e[1]);
			return map;
		}

		#region Test Methods

		[TestMethod]
		public void Tree_BFS()
		{
			var n = 5;
			var rs = new[]
			{
				new[] { 3, 1 },
				new[] { 3, 5 },
				new[] { 4, 3 },
				new[] { 2, 3 },
			};
			var map = GraphHelper.EdgesToMap1(n + 1, rs, false);

			var u = new int[n + 1];
			var q = new Queue<int>();
			u[1] = 1;
			q.Enqueue(1);

			while (q.Any())
			{
				var p = q.Dequeue();
				var v = u[p] + 1;
				foreach (var x in map[p])
				{
					if (u[x] > 0) continue;
					u[x] = v;
					q.Enqueue(x);
				}
			}
			Console.WriteLine(string.Join("\n", u.Select((x, i) => $"{i}: {x}")));
		}

		[TestMethod]
		public void Tree_DFS()
		{
			var n = 5;
			var rs = new[]
			{
				new[] { 3, 1 },
				new[] { 3, 5 },
				new[] { 3, 4 },
				new[] { 2, 3 },
			};
			map = GraphHelper.EdgesToMap1(n + 1, rs, true);
			u = new int[n + 1];
			Dfs(2, 0);
			Console.WriteLine(string.Join("\n", u.Select((x, i) => $"{i}: {x}")));
		}

		static List<int>[] map;
		static int[] u;
		static void Dfs(int p, int p0)
		{
			foreach (var p2 in map[p])
			{
				if (p2 == p0) continue;
				u[p2] = u[p] + 1;
				Dfs(p2, p);
			}
		}
		#endregion
	}
}
