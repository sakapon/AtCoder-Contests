using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Array.ConvertAll(new bool[n], _ => (V)Read2());
		var t = Array.ConvertAll(new bool[n], _ => (V)Read2());

		if (n == 1) return true;

		var tset = t.ToHashSet();
		var sd = Distances(n, s);
		var td = Distances(n, t);

		if (!sd.Select(_ => _.d2).OrderBy(x => x).SequenceEqual(td.Select(_ => _.d2).OrderBy(x => x))) return false;

		sd = MinGroup(sd);
		td = MinGroup(td);

		var (si, sj, _) = sd[0];
		for (int j2 = 0; j2 < td.Length; j2++)
		{
			var (ti, tj, _) = td[j2];
			if (Check(si, sj, ti, tj)) return true;
			if (Check(si, sj, tj, ti)) return true;
		}

		return false;

		// ベクトル si->sj と ti->tj が対応すると仮定してチェック
		bool Check(int si, int sj, int ti, int tj)
		{
			var sv = s[sj] - s[si];
			var tv = t[tj] - t[ti];

			var angle = tv.Angle - sv.Angle;
			var s2 = Array.ConvertAll(s, p => p.Rotate(angle));

			var delta = t[ti] - s2[si];
			s2 = Array.ConvertAll(s2, p => p + delta);

			if (!Array.TrueForAll(s2, p => IsAlmostInteger(p.X, 9))) return false;
			if (!Array.TrueForAll(s2, p => IsAlmostInteger(p.Y, 9))) return false;
			s2 = Array.ConvertAll(s2, p => new V(Math.Round(p.X), Math.Round(p.Y)));

			return tset.SetEquals(s2);
		}
	}

	static (int i, int j, double d2)[] Distances(int n, V[] s)
	{
		var r = new List<(int, int, double)>();
		for (int i = 0; i < n; i++)
		{
			for (int j = i + 1; j < n; j++)
			{
				var d2 = (s[i] - s[j]).NormSq;
				r.Add((i, j, d2));
			}
		}
		return r.ToArray();
	}

	static (int i, int j, double d2)[] MinGroup((int i, int j, double d2)[] sd)
	{
		var g = sd.GroupBy(_ => _.d2).OrderBy(g => g.Count()).ThenBy(g => g.Key).First();
		return g.ToArray();
	}

	public static bool IsAlmostInteger(double x, int digits = 12) =>
		Math.Round(x - Math.Round(x), digits) == 0;
}

struct V : IEquatable<V>
{
	public double X, Y;
	public V(double x, double y) => (X, Y) = (x, y);
	public void Deconstruct(out double x, out double y) => (x, y) = (X, Y);
	public override string ToString() => $"{X:F9} {Y:F9}";
	public static V Parse(string s) => Array.ConvertAll(s.Split(), double.Parse);

	public static implicit operator V(double[] v) => (v[0], v[1]);
	public static explicit operator double[](V v) => new[] { v.X, v.Y };
	public static implicit operator V((double x, double y) v) => new V(v.x, v.y);
	public static explicit operator (double, double)(V v) => (v.X, v.Y);

	public bool Equals(V other) => X == other.X && Y == other.Y;
	public static bool operator ==(V v1, V v2) => v1.Equals(v2);
	public static bool operator !=(V v1, V v2) => !v1.Equals(v2);
	public override bool Equals(object obj) => obj is V v && Equals(v);
	public override int GetHashCode() => (X, Y).GetHashCode();

	public static V operator -(V v) => (-v.X, -v.Y);
	public static V operator +(V v1, V v2) => (v1.X + v2.X, v1.Y + v2.Y);
	public static V operator -(V v1, V v2) => (v1.X - v2.X, v1.Y - v2.Y);
	public static V operator *(double c, V v) => v * c;
	public static V operator *(V v, double c) => (v.X * c, v.Y * c);
	public static V operator /(V v, double c) => (v.X / c, v.Y / c);

	public double NormL1 => Math.Abs(X) + Math.Abs(Y);
	public double Norm => Math.Sqrt(X * X + Y * Y);
	public double NormSq => X * X + Y * Y;
	public double Angle => Math.Atan2(Y, X);

	public V Rotate(double angle)
	{
		var cos = Math.Cos(angle);
		var sin = Math.Sin(angle);
		return (cos * X - sin * Y, sin * X + cos * Y);
	}
}
