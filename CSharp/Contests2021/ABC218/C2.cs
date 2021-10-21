using System;
using System.Collections.Generic;
using System.Linq;

class C2
{
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Array.ConvertAll(new bool[n], _ => Console.ReadLine());
		var t = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

		var tps = t.ToPoints();

		for (int i = 0; i < 4; i++)
		{
			if (Equals()) return true;
			s = RotateLeft(s);
		}
		return false;

		bool Equals()
		{
			var sps = s.ToPoints();
			if (sps.Length != tps.Length) return false;

			return sps.Zip(tps, (p, q) => (p.i - q.i, p.j - q.j)).Distinct().Count() == 1;
		}
	}

	static string[] RotateLeft(string[] s)
	{
		var (h, w) = (s.Length, s[0].Length);
		var r = new string[w];
		for (int i = 0; i < w; ++i)
		{
			var cs = new char[h];
			for (int j = 0; j < h; ++j)
				cs[j] = s[j][w - 1 - i];
			r[i] = new string(cs);
		}
		return r;
	}
}

public static class GridHelper3
{
	// 座標リスト表現
	public static (int i, int j)[] ToPoints(this string[] s, char c = '#')
	{
		if (s.Length == 0) return new (int, int)[0];
		var (h, w) = (s.Length, s[0].Length);
		var l = new List<(int, int)>();
		for (int i = 0; i < h; ++i)
			for (int j = 0; j < w; ++j)
				if (s[i][j] == c)
					l.Add((i, j));
		return l.ToArray();
	}

	public static (int i, int j) Rotate90(this (int i, int j) p) => (-p.j, p.i);
	public static (int i, int j) Rotate180(this (int i, int j) p) => (-p.i, -p.j);
	public static (int i, int j) Rotate270(this (int i, int j) p) => (p.j, -p.i);

	public static (int i, int j) Rotate90(this (int i, int j) p, int h, int w) => (w - 1 - p.j, p.i);
	public static (int i, int j) Rotate180(this (int i, int j) p, int h, int w) => (h - 1 - p.i, w - 1 - p.j);
	public static (int i, int j) Rotate270(this (int i, int j) p, int h, int w) => (p.j, h - 1 - p.i);

	// 左上から並べるには、ソートします。
	public static (int i, int j)[] Rotate90(this (int i, int j)[] ps) => Array.ConvertAll(ps, Rotate90);
	public static (int i, int j)[] Rotate180(this (int i, int j)[] ps) => Array.ConvertAll(ps, Rotate180);
	public static (int i, int j)[] Rotate270(this (int i, int j)[] ps) => Array.ConvertAll(ps, Rotate270);
}
