using System;
using System.Collections.Generic;

namespace CoderLib8.Graphs.Arrays
{
	// 有向グラフ
	public static class DirectedGraphHelper
	{
		// Test: https://onlinejudge.u-aizu.ac.jp/courses/library/5/GRL/4
		// Test: https://atcoder.jp/contests/typical90/tasks/typical90_bs
		// 閉路検査としても利用できます。O(n + m)
		// 連結性、多重性および重み (e[2]) の有無を問いません。
		// 連結成分の順になるとは限りません。
		// 連結成分ごとに出力するには、先に DSU などで分割してから呼び出します。
		// 閉路があるとき、null。
		// DAG であるとき、ソートされた頂点集合。
		public static int[] TopologicalSort(int n, int[][] des)
		{
			var map = Array.ConvertAll(new bool[n], _ => new List<int>());
			var indeg = new int[n];
			foreach (var e in des)
			{
				map[e[0]].Add(e[1]);
				++indeg[e[1]];
			}

			var r = new List<int>();
			var q = new Queue<int>();
			for (int v = 0; v < n; ++v)
				if (indeg[v] == 0)
					q.Enqueue(v);

			while (q.Count > 0)
			{
				var v = q.Dequeue();
				r.Add(v);
				foreach (var nv in map[v])
				{
					if (--indeg[nv] > 0) continue;
					q.Enqueue(nv);
				}
			}

			if (r.Count < n) return null;
			return r.ToArray();
		}

		// Test: https://atcoder.jp/contests/practice2/tasks/practice2_g
		// 1-based の場合、頂点 0 は最後のグループに含まれます。
		public static (int gc, int[] gis) SCC(int n, int[][] es)
		{
			var u = new bool[n];
			var t = n;
			var map = Array.ConvertAll(u, _ => new List<int>());
			var mapr = Array.ConvertAll(u, _ => new List<int>());
			foreach (var e in es)
			{
				map[e[0]].Add(e[1]);
				mapr[e[1]].Add(e[0]);
			}

			var vs = new int[n];
			for (int v = 0; v < n; ++v) Dfs(v);

			Array.Clear(u, 0, n);
			var gis = new int[n];
			foreach (var v in vs) if (Dfsr(v)) ++t;
			return (t, gis);

			void Dfs(int v)
			{
				if (u[v]) return;
				u[v] = true;
				foreach (var nv in map[v]) Dfs(nv);
				vs[--t] = v;
			}

			bool Dfsr(int v)
			{
				if (u[v]) return false;
				u[v] = true;
				foreach (var nv in mapr[v]) Dfsr(nv);
				gis[v] = t;
				return true;
			}
		}

		public static List<int>[] SCCToGroups(int n, int[][] es)
		{
			var (gc, gis) = SCC(n, es);
			var gs = Array.ConvertAll(new bool[gc], _ => new List<int>());
			for (int v = 0; v < n; ++v) gs[gis[v]].Add(v);
			return gs;
		}

		public static HashSet<int>[] SCCToMap(int n, int[][] es)
		{
			var (gc, gis) = SCC(n, es);
			var map = Array.ConvertAll(new bool[gc], _ => new HashSet<int>());
			foreach (var e in es)
			{
				var g0 = gis[e[0]];
				var g1 = gis[e[1]];
				if (g0 != g1) map[g0].Add(g1);
			}
			return map;
		}
	}
}
