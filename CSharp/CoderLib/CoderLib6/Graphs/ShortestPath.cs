﻿using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.Trees;

namespace CoderLib6.Graphs
{
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/12/ALDS1_12_C
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
		[Obsolete("最悪計算量は O(E^2) です。")]
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

		// dag: { from, to, weight }
		// 経路: 負閉路が存在する場合または到達不可能の場合、null を返します。
		// コスト: 負閉路が存在する場合は MinValue を、到達不可能の場合は MaxValue を返します。
		static int[] BellmanFord(int n, int sv, int ev, int[][] dag)
		{
			var map = Array.ConvertAll(new int[n + 1], _ => new List<int[]>());
			foreach (var e in dag)
			{
				map[e[0]].Add(new[] { e[1], e[2] });
			}

			var from = Enumerable.Repeat(-1, n + 1).ToArray();
			var u = Enumerable.Repeat(long.MaxValue, n + 1).ToArray();
			u[sv] = 0;

			var next = true;
			for (int k = 0; k <= n && next; k++)
			{
				next = false;
				for (int v = 0; v <= n; v++)
				{
					if (u[v] == long.MaxValue) continue;
					foreach (var e in map[v])
					{
						if (u[e[0]] <= u[v] + e[1]) continue;
						from[e[0]] = v;
						u[e[0]] = u[v] + e[1];
						next = true;
					}
				}
			}

			// コストを求める場合。
			//if (next) return long.MinValue;
			//return u[ev];

			if (next) return null;
			if (from[ev] == -1) return null;
			var path = new List<int>();
			for (var v = ev; v != -1; v = from[v])
				path.Add(v);
			path.Reverse();
			return path.ToArray();
		}

		// es: { from, to, weight }
		// 負閉路が存在する場合、null を返します。
		// 到達不可能のペアの値は MaxValue です。
		static long[][] WarshallFloyd(int n, int[][] es)
		{
			var d = new long[n + 1][];
			for (int i = 0; i <= n; i++)
			{
				d[i] = Array.ConvertAll(d, _ => long.MaxValue);
				d[i][i] = 0;
			}

			foreach (var e in es)
			{
				// 多重辺の場合に対応するため、Min を使います。
				d[e[0]][e[1]] = Math.Min(d[e[0]][e[1]], e[2]);
				// 有向グラフの場合、ここを削除します。
				d[e[1]][e[0]] = Math.Min(d[e[1]][e[0]], e[2]);
			}

			for (int k = 0; k <= n; k++)
				for (int i = 0; i <= n; i++)
					for (int j = 0; j <= n; j++)
						if (d[i][k] < long.MaxValue && d[k][j] < long.MaxValue)
							d[i][j] = Math.Min(d[i][j], d[i][k] + d[k][j]);

			if (Enumerable.Range(0, n + 1).Any(i => d[i][i] < 0)) return null;
			return d;
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
