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

		var tps = ToPoints(t);

		if (Equals()) return true;

		s = RotateLeft(s);
		if (Equals()) return true;

		s = RotateLeft(s);
		if (Equals()) return true;

		s = RotateLeft(s);
		if (Equals()) return true;

		return false;

		(int i, int j)[] ToPoints(string[] s)
		{
			var l = new List<(int, int)>();

			for (int i = 0; i < n; i++)
			{
				for (int j = 0; j < n; j++)
				{
					if (s[i][j] == '#')
					{
						l.Add((i, j));
					}
				}
			}
			return l.ToArray();
		}

		bool Equals()
		{
			var sps = ToPoints(s);
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
