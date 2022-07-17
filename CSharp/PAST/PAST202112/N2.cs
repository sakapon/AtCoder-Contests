using System;
using System.Collections.Generic;
using System.Linq;

class N2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var S = Array.ConvertAll(new bool[n], _ => V.Parse(Console.ReadLine()));
		var T = Array.ConvertAll(new bool[m], _ => V.Parse(Console.ReadLine()));

		// T_j T_{j+1} の左側なら負値
		var SP_T = NewArray2<int>(n, m);
		for (int i = 0; i < n; i++)
			for (int j = 0; j < m; j++)
				SP_T[i][j] = WhichSide(S[i], T[j], T[(j + 1) % m]);

		// S_i S_{i+1} の左側なら負値
		var TP_S = NewArray2<int>(m, n);
		for (int j = 0; j < m; j++)
			for (int i = 0; i < n; i++)
				TP_S[j][i] = WhichSide(T[j], S[i], S[(i + 1) % n]);

		var pl = new List<V>();

		// 頂点
		for (int i = 0; i < n; i++)
			if (SP_T[i].All(d => d <= 0))
				pl.Add(S[i]);
		for (int j = 0; j < m; j++)
			if (TP_S[j].All(d => d <= 0))
				pl.Add(T[j]);

		// 辺の交点
		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < m; j++)
			{
				var ni = (i + 1) % n;
				var nj = (j + 1) % m;

				if (SP_T[i][j] * SP_T[ni][j] == -1 && TP_S[j][i] * TP_S[nj][i] == -1)
				{
					pl.Add(Intersection(S[i], S[ni], T[j], T[nj]));
				}
			}
		}

		if (pl.Count == 0) return 0;

		// 原点の Angle が 0 となることを利用します。
		var ps = pl.OrderBy(p => p.X).ThenBy(p => p.Y).ToArray();
		var p0 = ps[^1];
		ps = ps.OrderBy(p => (p - p0).Angle).ToArray();
		return Enumerable.Range(0, ps.Length).Select(i => Area(ps[i] - p0, ps[(i + 1) % ps.Length] - p0)).Sum() / 2;
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));

	// P がベクトル P1P2 の左側なら負値
	static int WhichSide(V p, V p1, V p2)
	{
		var v = p - p1;
		var v0 = p2 - p1;
		return Math.Sign(v.X * v0.Y - v0.X * v.Y);
	}

	static V Intersection(V p1, V p2, V q1, V q2)
	{
		var vp = p2 - p1;
		var vq = q2 - q1;
		if (vp.X * vq.Y == vp.Y * vq.X) return q2;
		return Intersection(
			vp.Y, -vp.X, vp.X * p1.Y - vp.Y * p1.X,
			vq.Y, -vq.X, vq.X * q1.Y - vq.Y * q1.X
		);
	}

	// 2 直線の交点
	// a1 b2 != a2 b1
	// ax + by + c = 0
	static V Intersection(double a1, double b1, double c1, double a2, double b2, double c2)
	{
		var d = a1 * b2 - a2 * b1;
		return new V((b1 * c2 - b2 * c1) / d, -(a1 * c2 - a2 * c1) / d);
	}

	// ベクトル P1P2 と x 軸による台形の面積の 2 倍 (符号あり)
	static double Area(V p1, V p2)
	{
		return (p1.X - p2.X) * (p1.Y + p2.Y);
	}
}
