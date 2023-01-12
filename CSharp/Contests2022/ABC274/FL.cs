using System;
using System.Collections.Generic;
using System.Linq;

class FL
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int w, int x, int v) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, a) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Read3());

		var r = 0;

		// 魚 i を網の左端に固定します。
		for (int i = 0; i < n; i++)
		{
			var tw = 0;
			var l = new List<(Rational t, int w)>();
			foreach (var (w, x0, v0) in ps)
			{
				var x = x0 - ps[i].x;
				var v = v0 - ps[i].v;
				if (0 <= x && x <= a) tw += w;

				if (v == 0) continue;
				var t0 = new Rational(-x, v);
				var ta = new Rational(a - x, v);
				if (t0 > 0 || t0 == 0 && v < 0) l.Add((t0, v > 0 ? w : -w));
				if (ta > 0 || ta == 0 && v > 0) l.Add((ta, v < 0 ? w : -w));
			}
			r = Math.Max(r, tw);

			foreach (var (_, w) in l.OrderBy(p => p.t).ThenBy(p => -p.w))
			{
				tw += w;
				r = Math.Max(r, tw);
			}
		}
		return r;
	}
}

public struct Rational : IEquatable<Rational>, IComparable<Rational>
{
	// X / Y
	// 通分はしません。
	public long X, Y;
	public Rational(long x, long y) { if (y < 0) { x = -x; y = -y; } X = x; Y = y; }
	public override string ToString() => $"{X} / {Y}";
	public static implicit operator Rational(long v) => new Rational(v, 1);

	public bool Equals(Rational other) => X * other.Y == other.X * Y;
	public static bool operator ==(Rational v1, Rational v2) => v1.Equals(v2);
	public static bool operator !=(Rational v1, Rational v2) => !v1.Equals(v2);
	public override bool Equals(object obj) => obj is Rational v && Equals(v);
	public override int GetHashCode() => (X, Y).GetHashCode();

	public int CompareTo(Rational other) => (X * other.Y).CompareTo(other.X * Y);
	public static bool operator <(Rational v1, Rational v2) => v1.CompareTo(v2) < 0;
	public static bool operator >(Rational v1, Rational v2) => v1.CompareTo(v2) > 0;
	public static bool operator <=(Rational v1, Rational v2) => v1.CompareTo(v2) <= 0;
	public static bool operator >=(Rational v1, Rational v2) => v1.CompareTo(v2) >= 0;

	public static Rational operator -(Rational v) => new Rational(-v.X, v.Y);
}
