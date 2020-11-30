﻿using System;
using System.Collections.Generic;

class AL
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (h, w) = Read2();
		var c = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		var sv = GridShortestPath.FindChar(h, w, c, 's');
		var gv = GridShortestPath.FindChar(h, w, c, 'g');
		var r = GridShortestPath.UndirectedBfs(h, w, c, sv, gv);
		return r.GetByP(gv) < int.MaxValue;
	}
}

struct P : IEquatable<P>
{
	public static P Zero = new P();
	public static P UnitX = new P(1, 0);
	public static P UnitY = new P(0, 1);

	public int i, j;
	public P(int _i, int _j) { i = _i; j = _j; }
	public override string ToString() => $"{i} {j}";

	public static implicit operator P((int i, int j) v) => new P(v.i, v.j);
	public static explicit operator (int, int)(P v) => (v.i, v.j);

	public bool Equals(P other) => i == other.i && j == other.j;
	public static bool operator ==(P v1, P v2) => v1.Equals(v2);
	public static bool operator !=(P v1, P v2) => !v1.Equals(v2);
	public override bool Equals(object obj) => obj is P && Equals((P)obj);
	public override int GetHashCode() => Tuple.Create(i, j).GetHashCode();

	public static P operator -(P v) => new P(-v.i, -v.j);
	public static P operator +(P v1, P v2) => new P(v1.i + v2.i, v1.j + v2.j);
	public static P operator -(P v1, P v2) => new P(v1.i - v2.i, v1.j - v2.j);

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
	// 境界チェックおよび壁チェックが含まれます。
	public static int[][] Bfs(int h, int w, Func<P, IEnumerable<P>> toNexts, P sv, P ev, Func<P, bool> isWall = null)
	{
		var cs = Array.ConvertAll(new bool[h], _ => Array.ConvertAll(new bool[w], __ => int.MaxValue));
		var q = new Queue<P>();
		cs.SetByP(sv, 0);
		q.Enqueue(sv);

		while (q.Count > 0)
		{
			var v = q.Dequeue();
			var nc = cs.GetByP(v) + 1;
			foreach (var nv in toNexts(v))
			{
				if (!nv.IsInRange(h, w)) continue;
				if (isWall?.Invoke(nv) == true) continue;
				if (cs.GetByP(nv) <= nc) continue;
				cs.SetByP(nv, nc);
				if (nv == ev) return cs;
				q.Enqueue(nv);
			}
		}
		return cs;
	}

	public static int[][] Bfs(int h, int w, P[][] es, bool directed, P sv, P ev)
	{
		var map = Array.ConvertAll(new bool[h], _ => Array.ConvertAll(new bool[w], __ => new List<P>()));
		foreach (var e in es)
		{
			map.GetByP(e[0]).Add(e[1]);
			if (!directed) map.GetByP(e[1]).Add(e[0]);
		}
		return Bfs(h, w, v => map.GetByP(v), sv, ev);
	}

	[Obsolete]
	public static int[][] UndirectedBfs0(int h, int w, string[] s, P sv, P ev)
	{
		var map = Array.ConvertAll(new bool[h], _ => Array.ConvertAll(new bool[w], __ => new List<P>()));

		for (int i = 0; i < h; i++)
			for (int j = 0; j < w; j++)
			{
				var v = new P(i, j);
				if (i > 0)
				{
					var nv = new P(i - 1, j);
					map.GetByP(v).Add(nv);
					map.GetByP(nv).Add(v);
				}
				if (j > 0)
				{
					var nv = new P(i, j - 1);
					map.GetByP(v).Add(nv);
					map.GetByP(nv).Add(v);
				}
			}
		return Bfs(h, w, v => map.GetByP(v), sv, ev, v => s[v.i][v.j] == '#');
	}

	// 典型的な無向グリッド BFS
	// ev: 終点を指定しない場合、new P(-1, -1)
	public static int[][] UndirectedBfs(int h, int w, string[] s, P sv, P ev)
	{
		return Bfs(h, w, v => v.Nexts(), sv, ev, v => s[v.i][v.j] == '#');
	}

	public static P FindChar(int h, int w, string[] s, char c)
	{
		for (int i = 0; i < h; i++)
			for (int j = 0; j < w; j++)
				if (s[i][j] == c) return new P(i, j);
		return new P(-1, -1);
	}
}
