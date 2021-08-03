using System;
using System.Collections.Generic;
using CoderLib6.DataTrees;

namespace CoderLib6.Graphs
{
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/12/ALDS1_12_B
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/12/ALDS1_12_C
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/5/GRL/1
	// Test: https://judge.yosupo.jp/problem/shortest_path
	// Test: https://codeforces.com/contest/20/problem/C
	static class ShortestPath
	{
		// es: { from, to, cost }
		// 最小コスト: 到達不可能の場合、MaxValue。
		// 入辺: 到達不可能の場合、null。
		public static Tuple<long[], int[][]> Dijkstra(int n, int[][] es, bool directed, int sv, int ev = -1)
		{
			var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
			foreach (var e in es)
			{
				map[e[0]].Add(new[] { e[0], e[1], e[2] });
				if (!directed) map[e[1]].Add(new[] { e[1], e[0], e[2] });
			}

			var cs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
			var inEdges = new int[n][];
			var q = PQ<int>.CreateWithKey(v => cs[v]);
			cs[sv] = 0;
			q.Push(sv);

			while (q.Count > 0)
			{
				var vc = q.Pop();
				var v = vc.Value;
				if (v == ev) break;
				if (cs[v] < vc.Key) continue;

				foreach (var e in map[v])
				{
					if (cs[e[1]] <= cs[v] + e[2]) continue;
					cs[e[1]] = cs[v] + e[2];
					inEdges[e[1]] = e;
					q.Push(e[1]);
				}
			}
			return Tuple.Create(cs, inEdges);
		}

		// priority queue ではなく、queue を使うほうが速いことがあります。
		[Obsolete("最悪計算量は O(VE) です。")]
		public static Tuple<long[], int[][]> Dijklmna(int n, int[][] es, bool directed, int sv, int ev = -1)
		{
			var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
			foreach (var e in es)
			{
				map[e[0]].Add(new[] { e[0], e[1], e[2] });
				if (!directed) map[e[1]].Add(new[] { e[1], e[0], e[2] });
			}

			var cs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
			var inEdges = new int[n][];
			var q = new Queue<int>();
			cs[sv] = 0;
			q.Enqueue(sv);

			while (q.Count > 0)
			{
				var v = q.Dequeue();

				foreach (var e in map[v])
				{
					if (cs[e[1]] <= cs[v] + e[2]) continue;
					cs[e[1]] = cs[v] + e[2];
					inEdges[e[1]] = e;
					q.Enqueue(e[1]);
				}
			}
			return Tuple.Create(cs, inEdges);
		}

		// des: { from, to, cost }
		// 負閉路が存在する場合、(null, null)。
		// 最小コスト: 到達不可能の場合、MaxValue。
		// 入辺: 到達不可能の場合、null。
		public static Tuple<long[], int[][]> BellmanFord(int n, int[][] des, int sv)
		{
			var cs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
			var inEdges = new int[n][];
			cs[sv] = 0;

			var next = true;
			for (int k = 0; k < n && next; ++k)
			{
				next = false;
				foreach (var e in des)
				{
					if (cs[e[0]] == long.MaxValue || cs[e[1]] <= cs[e[0]] + e[2]) continue;
					cs[e[1]] = cs[e[0]] + e[2];
					inEdges[e[1]] = e;
					next = true;
				}
			}
			if (next) return Tuple.Create<long[], int[][]>(null, null);
			return Tuple.Create(cs, inEdges);
		}

		// es: { from, to, cost }
		// 負閉路が存在する場合、(null, null)。
		// 最小コスト: 到達不可能の場合、MaxValue。
		// 中間点: 到達不可能または直結の場合、-1。
		public static Tuple<long[][], int[][]> WarshallFloyd(int n, int[][] es, bool directed)
		{
			var cs = Array.ConvertAll(new bool[n], i => Array.ConvertAll(new bool[n], _ => long.MaxValue));
			var inters = Array.ConvertAll(new bool[n], i => Array.ConvertAll(new bool[n], _ => -1));
			for (int i = 0; i < n; ++i) cs[i][i] = 0;

			foreach (var e in es)
			{
				cs[e[0]][e[1]] = Math.Min(cs[e[0]][e[1]], e[2]);
				if (!directed) cs[e[1]][e[0]] = Math.Min(cs[e[1]][e[0]], e[2]);
			}

			for (int k = 0; k < n; ++k)
				for (int i = 0; i < n; ++i)
					for (int j = 0; j < n; ++j)
					{
						if (cs[i][k] == long.MaxValue || cs[k][j] == long.MaxValue) continue;
						var nc = cs[i][k] + cs[k][j];
						if (cs[i][j] <= nc) continue;
						cs[i][j] = nc;
						inters[i][j] = k;
					}
			for (int i = 0; i < n; ++i) if (cs[i][i] < 0) return Tuple.Create<long[][], int[][]>(null, null);
			return Tuple.Create(cs, inters);
		}

		public static int[] GetPathVertexes(int[][] inEdges, int ev)
		{
			var path = new Stack<int>();
			path.Push(ev);
			for (var e = inEdges[ev]; e != null; e = inEdges[e[0]])
				path.Push(e[0]);
			return path.ToArray();
		}

		public static int[][] GetPathEdges(int[][] inEdges, int ev)
		{
			var path = new Stack<int[]>();
			for (var e = inEdges[ev]; e != null; e = inEdges[e[0]])
				path.Push(e);
			return path.ToArray();
		}
	}
}
