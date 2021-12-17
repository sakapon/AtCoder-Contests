using System;
using System.Collections.Generic;
using System.Linq;

class C3
{
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Array.ConvertAll(new bool[n], _ => Console.ReadLine());
		var t = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

		var sps = s.ToPointsV();
		var tps = t.ToPointsV();

		for (int i = 0; i < 4; i++)
		{
			if (Equals()) return true;
			sps = sps.Rotate90().OrderBy(p => p.X).ThenBy(p => p.Y).ToArray();
		}
		return false;

		bool Equals()
		{
			if (sps.Length != tps.Length) return false;
			var d = sps[0] - tps[0];
			return sps.Zip(tps, (p, q) => p - q).All(p => p == d);
		}
	}
}

public static class GridHelper4
{
	// 座標リスト表現
	public static IntV[] ToPointsV(this string[] s, char c = '#')
	{
		if (s.Length == 0) return new IntV[0];
		var (h, w) = (s.Length, s[0].Length);
		var l = new List<IntV>();
		for (int i = 0; i < h; ++i)
			for (int j = 0; j < w; ++j)
				if (s[i][j] == c)
					l.Add(new IntV(i, j));
		return l.ToArray();
	}

	// 左上から並べるには、ソートします。
	public static IntV[] Rotate90(this IntV[] ps) => Array.ConvertAll(ps, p => p.Rotate90());
	public static IntV[] Rotate180(this IntV[] ps) => Array.ConvertAll(ps, p => p.Rotate180());
	public static IntV[] Rotate270(this IntV[] ps) => Array.ConvertAll(ps, p => p.Rotate270());
}

public struct IntV : IEquatable<IntV>
{
	public static IntV Zero = new IntV();
	public static IntV UnitX = new IntV(1, 0);
	public static IntV UnitY = new IntV(0, 1);

	public long X, Y;
	public IntV(long x, long y) { X = x; Y = y; }
	public override string ToString() => $"{X} {Y}";
	public static IntV Parse(string s) => Array.ConvertAll(s.Split(), long.Parse);

	public static implicit operator IntV(long[] v) => new IntV(v[0], v[1]);
	public static explicit operator long[](IntV v) => new[] { v.X, v.Y };

	public bool Equals(IntV other) => X == other.X && Y == other.Y;
	public static bool operator ==(IntV v1, IntV v2) => v1.Equals(v2);
	public static bool operator !=(IntV v1, IntV v2) => !v1.Equals(v2);
	public override bool Equals(object obj) => obj is IntV && Equals((IntV)obj);
	public override int GetHashCode() => Tuple.Create(X, Y).GetHashCode();

	public static IntV operator -(IntV v) => new IntV(-v.X, -v.Y);
	public static IntV operator +(IntV v1, IntV v2) => new IntV(v1.X + v2.X, v1.Y + v2.Y);
	public static IntV operator -(IntV v1, IntV v2) => new IntV(v1.X - v2.X, v1.Y - v2.Y);

	public IntV Rotate90() => new IntV(-Y, X);
	public IntV Rotate180() => new IntV(-X, -Y);
	public IntV Rotate270() => new IntV(Y, -X);
}
