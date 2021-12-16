using System;
using System.Linq;

class E2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => IntV.Parse(Console.ReadLine()));

		var r = 0;
		IntV tp = (1L, 0L);

		foreach (var p in ps.OrderBy(p => p - IntV.UnitX))
		{
			if (tp.CompareTo(p - IntV.UnitY) <= 0)
			{
				r++;
				tp = p - IntV.UnitX;
			}
		}

		return r;
	}
}

public struct IntV : IEquatable<IntV>, IComparable<IntV>
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

	// Y / X の順序
	public int CompareTo(IntV other) => (other.X * Y).CompareTo(X * other.Y);
}
