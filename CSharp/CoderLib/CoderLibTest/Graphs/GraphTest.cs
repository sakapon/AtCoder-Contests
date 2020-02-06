using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Graphs
{
	[TestClass]
	public class GraphTest
	{
		static List<int>[] UndirectedMap(int n, int[][] rs)
		{
			var map = Array.ConvertAll(new int[n + 1], _ => new List<int>());
			foreach (var r in rs)
			{
				map[r[0]].Add(r[1]);
				map[r[1]].Add(r[0]);
			}
			return map;
		}

		// キーが含まれない可能性があります。
		static Dictionary<int, int[]> UndirectedMap(int[][] rs) => rs.Concat(rs.Select(r => new[] { r[1], r[0] })).GroupBy(r => r[0], r => r[1]).ToDictionary(g => g.Key, g => g.ToArray());

		static List<int>[] DirectedMap(int n, int[][] rs)
		{
			var map = Array.ConvertAll(new int[n + 1], _ => new List<int>());
			foreach (var r in rs)
				map[r[0]].Add(r[1]);
			return map;
		}

		// 値が null の可能性があります。
		static List<int>[] DirectedMap2(int n, int[][] rs)
		{
			var map = new List<int>[n + 1];
			foreach (var r in rs)
				if (map[r[0]] == null) map[r[0]] = new List<int> { r[1] };
				else map[r[0]].Add(r[1]);
			return map;
		}

		// キーが含まれない可能性があります。
		static Dictionary<int, int[]> DirectedMap(int[][] rs) => rs.GroupBy(r => r[0], r => r[1]).ToDictionary(g => g.Key, g => g.ToArray());

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
			var map = UndirectedMap(n, rs);

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
		#endregion
	}
}
