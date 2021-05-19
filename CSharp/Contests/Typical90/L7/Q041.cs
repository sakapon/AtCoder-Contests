using System;
using System.Collections.Generic;
using System.Linq;

class Q041
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => IntV.Parse(Console.ReadLine()));

		var gs = ps.GroupBy(p => p.X).OrderBy(g => g.Key).Select(g => g.OrderBy(p => p.Y).ToArray()).ToArray();

		var ps_u = GetUpper();
		var ps_l = GetLower();

		var area2 = GetArea2(ps_u) - GetArea2(ps_l);

		var lattices = 0L;
		lattices += gs[0][^1].Y - gs[0][0].Y;
		lattices += gs[^1][^1].Y - gs[^1][0].Y;
		lattices += GetLattices(ps_u);
		lattices += GetLattices(ps_l);

		// Pick's theorem
		var inners = (area2 - lattices) / 2 + 1;
		return lattices + inners - n;

		IntV[] GetUpper()
		{
			var q = new Stack<IntV>();

			foreach (var g in gs)
			{
				var p = g[^1];

				while (q.Count >= 2)
				{
					var p1 = q.Pop();
					var p0 = q.Pop();

					if (IsConvexUp(p0, p1, p))
					{
						q.Push(p0);
						q.Push(p1);
						break;
					}
					q.Push(p0);
				}
				q.Push(p);
			}
			var ps2 = q.ToArray();
			Array.Reverse(ps2);
			return ps2;
		}

		IntV[] GetLower()
		{
			var q = new Stack<IntV>();

			foreach (var g in gs)
			{
				var p = g[0];

				while (q.Count >= 2)
				{
					var p1 = q.Pop();
					var p0 = q.Pop();

					if (IsConvexDown(p0, p1, p))
					{
						q.Push(p0);
						q.Push(p1);
						break;
					}
					q.Push(p0);
				}
				q.Push(p);
			}
			var ps2 = q.ToArray();
			Array.Reverse(ps2);
			return ps2;
		}
	}

	static long GetArea2(IntV[] ps)
	{
		return Enumerable.Range(0, ps.Length - 1).Sum(i => (ps[i + 1].X - ps[i].X) * (ps[i + 1].Y + ps[i].Y));
	}

	static long GetLattices(IntV[] ps)
	{
		return Enumerable.Range(0, ps.Length - 1).Select(i => ps[i + 1] - ps[i]).Sum(v => v.Y == 0 ? v.X : Gcd(v.X, Math.Abs(v.Y)));
	}

	static long Gcd(long a, long b) { for (long r; (r = a % b) > 0; a = b, b = r) ; return b; }

	static bool IsConvexUp(IntV p0, IntV p1, IntV p2)
	{
		var v0 = p1 - p0;
		var v1 = p2 - p1;
		return v1.X * v0.Y >= v0.X * v1.Y;
	}

	static bool IsConvexDown(IntV p0, IntV p1, IntV p2)
	{
		var v0 = p1 - p0;
		var v1 = p2 - p1;
		return v1.X * v0.Y <= v0.X * v1.Y;
	}
}

struct IntV : IEquatable<IntV>
{
	public static IntV Zero = (0, 0);
	public static IntV UnitX = (1, 0);
	public static IntV UnitY = (0, 1);

	public long X, Y;
	public IntV(long x, long y) => (X, Y) = (x, y);
	public void Deconstruct(out long x, out long y) => (x, y) = (X, Y);
	public override string ToString() => $"{X} {Y}";
	public static IntV Parse(string s) => Array.ConvertAll(s.Split(), long.Parse);

	public static implicit operator IntV(long[] v) => (v[0], v[1]);
	public static explicit operator long[](IntV v) => new[] { v.X, v.Y };
	public static implicit operator IntV((long x, long y) v) => new IntV(v.x, v.y);
	public static explicit operator (long, long)(IntV v) => (v.X, v.Y);

	public bool Equals(IntV other) => X == other.X && Y == other.Y;
	public static bool operator ==(IntV v1, IntV v2) => v1.Equals(v2);
	public static bool operator !=(IntV v1, IntV v2) => !v1.Equals(v2);
	public override bool Equals(object obj) => obj is IntV v && Equals(v);
	public override int GetHashCode() => (X, Y).GetHashCode();

	public static IntV operator -(IntV v) => (-v.X, -v.Y);
	public static IntV operator +(IntV v1, IntV v2) => (v1.X + v2.X, v1.Y + v2.Y);
	public static IntV operator -(IntV v1, IntV v2) => (v1.X - v2.X, v1.Y - v2.Y);

	public long NormL1 => Math.Abs(X) + Math.Abs(Y);
	public double Norm => Math.Sqrt(X * X + Y * Y);

	public static long Dot(IntV v1, IntV v2) => v1.X * v2.X + v1.Y * v2.Y;
	// 菱形の面積
	public static long Area(IntV v1, IntV v2) => Math.Abs(v1.X * v2.Y - v2.X * v1.Y);
	public static bool IsParallel(IntV v1, IntV v2) => v1.X * v2.Y == v2.X * v1.Y;
	public static bool IsOrthogonal(IntV v1, IntV v2) => Dot(v1, v2) == 0;
}
