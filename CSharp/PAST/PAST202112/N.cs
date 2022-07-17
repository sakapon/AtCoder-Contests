using System;
using System.Collections.Generic;
using System.Linq;

class N
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
		var p0 = pl[0];
		var ps = pl.Skip(1).Where(p => p != p0).OrderBy(p => (p - p0).Angle).ToArray();
		return Enumerable.Range(0, ps.Length).Select(i => V.Area(ps[i] - p0, ps[(i + 1) % ps.Length] - p0)).Where(a => a > 0).Sum() / 2;
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
}

struct V : IEquatable<V>
{
	public static V Zero = new V();
	public static V UnitX = new V(1, 0);
	public static V UnitY = new V(0, 1);

	public double X, Y;
	public V(double x, double y) { X = x; Y = y; }
	public override string ToString() => $"{X:F9} {Y:F9}";
	public static V Parse(string s) => Array.ConvertAll(s.Split(), double.Parse);

	public static implicit operator V(double[] v) => new V(v[0], v[1]);
	public static explicit operator double[](V v) => new[] { v.X, v.Y };

	public bool Equals(V other) => X == other.X && Y == other.Y;
	public static bool operator ==(V v1, V v2) => v1.Equals(v2);
	public static bool operator !=(V v1, V v2) => !v1.Equals(v2);
	public override bool Equals(object obj) => obj is V && Equals((V)obj);
	public override int GetHashCode() => Tuple.Create(X, Y).GetHashCode();

	public static V operator +(V v) => v;
	public static V operator -(V v) => new V(-v.X, -v.Y);
	public static V operator +(V v1, V v2) => new V(v1.X + v2.X, v1.Y + v2.Y);
	public static V operator -(V v1, V v2) => new V(v1.X - v2.X, v1.Y - v2.Y);
	public static V operator *(double c, V v) => v * c;
	public static V operator *(V v, double c) => new V(v.X * c, v.Y * c);
	public static V operator /(V v, double c) => new V(v.X / c, v.Y / c);

	public double Angle => Math.Atan2(Y, X);

	// 菱形の面積 (三角形の面積の 2 倍、符号あり)
	public static double Area(V v1, V v2) => v1.X * v2.Y - v2.X * v1.Y;
}
