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

		var sps = ToPoints(s);
		var tps = ToPoints(t);

		if (Equals()) return true;

		sps = RotateRight(sps);
		Array.Sort(sps);
		if (Equals()) return true;

		sps = RotateRight(sps);
		Array.Sort(sps);
		if (Equals()) return true;

		sps = RotateRight(sps);
		Array.Sort(sps);
		if (Equals()) return true;

		return false;

		bool Equals()
		{
			if (sps.Length != tps.Length) return false;
			var d = (sps[0].i - tps[0].i, sps[0].j - tps[0].j);
			return sps.Zip(tps, (p, q) => (p.i - q.i, p.j - q.j)).All(p => p == d);
		}
	}

	static (int i, int j)[] ToPoints(string[] s)
	{
		var (h, w) = (s.Length, s[0].Length);
		var l = new List<(int, int)>();
		for (int i = 0; i < h; ++i)
			for (int j = 0; j < w; ++j)
				if (s[i][j] == '#') l.Add((i, j));
		return l.ToArray();
	}

	static (int i, int j)[] RotateRight((int i, int j)[] ps)
	{
		return Array.ConvertAll(ps, p => (-p.j, p.i));
	}
}
