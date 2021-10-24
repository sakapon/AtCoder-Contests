using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => IntV.Parse(Console.ReadLine()));

		var r = 0;
		for (int i = 0; i < n; i++)
		{
			for (int j = i + 1; j < n; j++)
			{
				for (int k = j + 1; k < n; k++)
				{
					var v1 = ps[i] - ps[j];
					var v2 = ps[i] - ps[k];
					if (!IntV.IsParallel(v1, v2)) r++;
				}
			}
		}
		return r;
	}
}

public struct IntV : IEquatable<IntV>
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

	public static bool IsParallel(IntV v1, IntV v2) => v1.X * v2.Y == v2.X * v1.Y;
}
