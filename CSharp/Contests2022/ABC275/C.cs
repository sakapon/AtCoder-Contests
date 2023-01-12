using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Array.ConvertAll(new bool[9], _ => Console.ReadLine().Select(c => c == '#').ToArray());

		var set = new HashSet<(int, int)>();

		for (int i = 0; i < 9; i++)
		{
			for (int j = 0; j < 9; j++)
			{
				if (s[i][j]) set.Add((i, j));
			}
		}
		var ps = set.ToArray();

		var r = 0;

		for (int x = 0; x < ps.Length; x++)
		{
			var (i1, j1) = ps[x];

			for (int y = x + 1; y < ps.Length; y++)
			{
				var (i2, j2) = ps[y];
				var (vi, vj) = (i2 - i1, j2 - j1);

				(vi, vj) = (vj, -vi);
				i2 += vi;
				j2 += vj;
				if (!set.Contains((i2, j2))) continue;

				(vi, vj) = (vj, -vi);
				i2 += vi;
				j2 += vj;
				if (!set.Contains((i2, j2))) continue;

				r++;
			}

			set.Remove((i1, j1));
		}

		return r;
	}
}
