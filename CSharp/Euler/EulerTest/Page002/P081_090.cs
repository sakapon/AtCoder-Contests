using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Graphs.SPPs.Int.WeightedGraph401;
using EulerLib8.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static EulerLib8.Common;

namespace EulerTest.Page002
{
	[TestClass]
	public class P081_090
	{
		#region Test Methods
		[TestMethod] public void T081() => TestHelper.Execute();
		[TestMethod] public void T082() => TestHelper.Execute();
		[TestMethod] public void T083() => TestHelper.Execute();
		[TestMethod] public void T084() => TestHelper.Execute();
		[TestMethod] public void T085() => TestHelper.Execute();
		[TestMethod] public void T086() => TestHelper.Execute();
		[TestMethod] public void T087() => TestHelper.Execute();
		[TestMethod] public void T088() => TestHelper.Execute();
		[TestMethod] public void T089() => TestHelper.Execute();
		[TestMethod] public void T090() => TestHelper.Execute();
		#endregion

		public static object P081()
		{
			var s = GetText(Url081).Split('\n', StringSplitOptions.RemoveEmptyEntries)
				.Select(l => Array.ConvertAll(l.Split(','), int.Parse))
				.ToArray();
			var h = s.Length;
			var w = s[0].Length;
			var n = h * w;

			var graph = GetWeightedAdjacencyList(h, w, s);
			var r = graph.Dijkstra(0, n - 1);
			return s[0][0] + r[n - 1].Cost;
		}

		public static WeightedGraph GetWeightedAdjacencyList(int h, int w, int[][] s)
		{
			var graph = new ListWeightedGraph(h * w);
			for (int i = 0; i < h; ++i)
				for (int j = 1; j < w; ++j)
				{
					var v = w * i + j;
					graph.AddEdge(v - 1, v, false, s[i][j]);
				}
			for (int j = 0; j < w; ++j)
				for (int i = 1; i < h; ++i)
				{
					var v = w * i + j;
					graph.AddEdge(v - w, v, false, s[i][j]);
				}
			return graph;
		}

		public static object P082()
		{
			var s = GetText(Url082).Split('\n', StringSplitOptions.RemoveEmptyEntries)
				.Select(l => Array.ConvertAll(l.Split(','), int.Parse))
				.ToArray();
			var h = s.Length;
			var w = s[0].Length;
			var n = h * w;

			var graph = new ListWeightedGraph(n + 2);

			for (int i = 0; i < h; ++i)
				for (int j = 1; j < w; ++j)
				{
					var v = w * i + j;
					graph.AddEdge(v - 1, v, false, s[i][j]);
				}
			for (int j = 0; j < w; ++j)
				for (int i = 1; i < h; ++i)
				{
					var v = w * i + j;
					graph.AddEdge(v, v - w, false, s[i - 1][j]);
					graph.AddEdge(v - w, v, false, s[i][j]);
				}

			for (int i = 0; i < h; ++i)
			{
				graph.AddEdge(n, i * w, false, s[i][0]);
				graph.AddEdge(i * w + w - 1, n + 1, false, 0);
			}

			var r = graph.Dijkstra(n, n + 1);
			return r[n + 1].Cost;
		}

		public static object P083()
		{
			var s = GetText(Url083).Split('\n', StringSplitOptions.RemoveEmptyEntries)
				.Select(l => Array.ConvertAll(l.Split(','), int.Parse))
				.ToArray();
			var grid = new IntWeightedGrid(s);
			var n = grid.VertexesCount;

			var r = grid.Dijkstra(0, n - 1);
			return s[0][0] + r[n - 1].Cost;
		}

		public static object P084()
		{
			return 0;
		}

		public static object P085()
		{
			const int n = 2000000;
			const int w_max = 2000;

			var r = (area: 0, count: 0L);
			for (int h = 1; h <= w_max; h++)
			{
				for (int w = 1; w <= w_max; w++)
				{
					var count = Count(h, w);
					ArgHelper.ChFirstMin(ref r, (h * w, count), p => Math.Abs(p.count - n));
					if (count > n) break;
				}
			}
			return r.area;

			static long Count(long h, long w) => h * (h + 1) / 2 * w * (w + 1) / 2;
		}

		public static object P086()
		{
			return 0;
		}

		public static object P087()
		{
			return 0;
		}

		public static object P088()
		{
			return 0;
		}

		public static object P089()
		{
			return 0;
		}

		public static object P090()
		{
			return 0;
		}

		const string Url081 = "https://projecteuler.net/project/resources/p081_matrix.txt";
		const string Url082 = "https://projecteuler.net/project/resources/p082_matrix.txt";
		const string Url083 = "https://projecteuler.net/project/resources/p083_matrix.txt";
	}
}
