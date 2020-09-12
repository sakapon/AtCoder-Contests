using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.Trees;
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
				// 有向グラフの場合、ここを削除します。
				map[e[1]].Add(new[] { e[0], e[2] });
			}

			var from = Enumerable.Repeat(-1, n + 1).ToArray();
			var d = Enumerable.Repeat(long.MaxValue, n + 1).ToArray();
			var u = new bool[n + 1];
			var pq = PQ<(int, long v)>.Create(_ => _.v);
			d[sv] = 0;
			pq.Push((sv, d[sv]));

			while (pq.Count > 0)
			{
				var (v, _) = pq.Pop();
				// すべての頂点を探索する場合、ここを削除します。
				if (v == ev) break;
				if (u[v]) continue;
				u[v] = true;

				foreach (var e in map[v])
				{
					if (d[e[0]] <= d[v] + e[1]) continue;
					from[e[0]] = v;
					d[e[0]] = d[v] + e[1];
					pq.Push((e[0], d[e[0]]));
				}
			}

			// コストを求める場合。
			//return d[ev];

			if (from[ev] == -1) return null;
			var path = new List<int>();
			for (var v = ev; v != -1; v = from[v])
				path.Add(v);
			path.Reverse();
			return path.ToArray();
		}

		// priority queue ではなく、queue を使うほうが速いことがあります。
		static int[] Dijklmna(int n, int sv, int ev, int[][] es)
		{
			var map = Array.ConvertAll(new int[n + 1], _ => new List<int[]>());
			foreach (var e in es)
			{
				map[e[0]].Add(new[] { e[1], e[2] });
				// 有向グラフの場合、ここを削除します。
				map[e[1]].Add(new[] { e[0], e[2] });
			}

			var from = Enumerable.Repeat(-1, n + 1).ToArray();
			var u = Enumerable.Repeat(long.MaxValue, n + 1).ToArray();
			var q = new Queue<int>();
			u[sv] = 0;
			q.Enqueue(sv);

			while (q.Count > 0)
			{
				var v = q.Dequeue();
				// すべての頂点を探索する場合、ここを削除します。
				if (v == ev) break;
				foreach (var e in map[v])
				{
					if (u[e[0]] <= u[v] + e[1]) continue;
					from[e[0]] = v;
					u[e[0]] = u[v] + e[1];
					q.Enqueue(e[0]);
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
				d[e[0]][e[1]] = e[2];
				// 有向グラフの場合、ここを削除します。
				d[e[1]][e[0]] = e[2];
			}

			for (int k = 0; k <= n; k++)
				for (int i = 0; i <= n; i++)
					for (int j = 0; j <= n; j++)
						if (d[i][k] < long.MaxValue && d[k][j] < long.MaxValue)
							d[i][j] = Math.Min(d[i][j], d[i][k] + d[k][j]);

			if (Enumerable.Range(0, n + 1).Any(i => d[i][i] < 0)) return null;
			return d;
		}
	}
}
