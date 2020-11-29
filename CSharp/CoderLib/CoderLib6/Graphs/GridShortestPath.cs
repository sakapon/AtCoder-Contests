using System;
using System.Collections.Generic;

namespace CoderLib6.Graphs
{
	struct P
	{
		public int i, j;
		public P(int _i, int _j) { i = _i; j = _j; }
		public bool IsInRange(int h, int w) => 0 <= i && i < h && 0 <= j && j < w;
		public P[] Nexts() => new[] { new P(i - 1, j), new P(i + 1, j), new P(i, j - 1), new P(i, j + 1) };
	}

	static class GridShortestPath
	{
		// 2次元配列に2次元インデックスでアクセスします。
		public static T GetByP<T>(this T[][] a, P p) => a[p.i][p.j];
		public static void SetByP<T>(this T[][] a, P p, T value) => a[p.i][p.j] = value;

		// 辺のコストがすべて等しい場合
		// ev: 終点を指定しない場合、new P(-1, -1)
		public static int[][] Bfs(int h, int w, P[][] es, bool directed, P sv, P ev)
		{
			var map = Array.ConvertAll(new bool[h], _ => Array.ConvertAll(new bool[w], __ => new List<P>()));
			foreach (var e in es)
			{
				map.GetByP(e[0]).Add(e[1]);
				if (!directed) map.GetByP(e[1]).Add(e[0]);
			}

			var cs = Array.ConvertAll(new bool[h], _ => Array.ConvertAll(new bool[w], __ => int.MaxValue));
			var q = new Queue<P>();
			cs.SetByP(sv, 0);
			q.Enqueue(sv);

			while (q.Count > 0)
			{
				var v = q.Dequeue();
				var nc = cs.GetByP(v) + 1;
				foreach (var nv in map.GetByP(v))
				{
					if (cs.GetByP(nv) <= nc) continue;
					cs.SetByP(nv, nc);
					if (nv.Equals(ev)) return cs;
					q.Enqueue(nv);
				}
			}
			return cs;
		}

		// 典型的な無向グリッドBFS
		// ev: 終点を指定しない場合、new P(-1, -1)
		public static int[][] GridBfs(int h, int w, string[] s, P sv, P ev)
		{
			var es = new List<P[]>();
			for (int i = 0; i < h; i++)
				for (int j = 0; j < w; j++)
				{
					if (s[i][j] == '#') continue;
					var v = new P(i, j);
					if (i > 0 && s[i - 1][j] != '#') es.Add(new[] { v, new P(i - 1, j) });
					if (j > 0 && s[i][j - 1] != '#') es.Add(new[] { v, new P(i, j - 1) });
				}
			return Bfs(h, w, es.ToArray(), false, sv, ev);
		}
	}
}
