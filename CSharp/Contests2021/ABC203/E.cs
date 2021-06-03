using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int x, int y) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var ps = Array.ConvertAll(new bool[m], _ => Read2());

		var u = new bool[2 * m + 3];
		u[m + 1] = true;
		var d = new Dictionary<int, bool>();

		var q = ps.GroupBy(p => p.x).OrderBy(g => g.Key);
		foreach (var g in q)
		{
			d.Clear();
			foreach (var (_, y) in g)
			{
				var j = y - n + m + 1;
				if (j < 1 || 2 * m + 1 < j) continue;

				d[j] = u[j - 1] || u[j + 1];
			}

			foreach (var (j, b) in d)
			{
				u[j] = b;
			}
		}

		return u.Count(b => b);
	}
}
