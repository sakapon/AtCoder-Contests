using System;
using System.Collections.Generic;
using System.Linq;

class D2
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static (long, long, long, long) Read4L() { var a = ReadL(); return (a[0], a[1], a[2], a[3]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var (sx, sy, tx, ty) = Read4L();
		var cs = Array.ConvertAll(new bool[n], _ => Read3L()).ToList();

		cs.Add((sx, sy, 0));
		cs.Add((tx, ty, 0));

		var uf = new UF(n + 2);

		for (int i = 0; i < cs.Count; i++)
		{
			for (int j = i + 1; j < cs.Count; j++)
			{
				if (Intersect(cs[i], cs[j]))
				{
					uf.Unite(i, j);
				}
			}
		}
		return uf.AreUnited(n, n + 1);
	}

	static bool Intersect((long x, long y, long r) c1, (long x, long y, long r) c2)
	{
		var dx = c1.x - c2.x;
		var dy = c1.y - c2.y;
		var d2 = dx * dx + dy * dy;

		if ((c1.r + c2.r) * (c1.r + c2.r) < d2) return false;
		return (c1.r - c2.r) * (c1.r - c2.r) <= d2;
	}
}
