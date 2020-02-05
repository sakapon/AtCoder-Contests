using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Graphs
{
	[TestClass]
	public class GraphTest
	{
		static List<int>[] GetMap(int n, int[][] rs)
		{
			var map = Array.ConvertAll(new int[n + 1], _ => new List<int>());
			foreach (var r in rs)
			{
				map[r[0]].Add(r[1]);
				map[r[1]].Add(r[0]);
			}
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
			var map = GetMap(n, rs);

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
