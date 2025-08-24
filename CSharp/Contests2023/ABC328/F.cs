using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var ps = Array.ConvertAll(new bool[m], _ => Read3());

		var rn = Enumerable.Range(0, n + 1).ToArray();
		var gs = rn.Select(i => new List<int> { i }).ToArray();
		var z = new long[n + 1];
		var r = new List<int>();

		for (int i = 0; i < m; i++)
		{
			var (a, b, d) = ps[i];

			if (gs[a] == gs[b])
			{
				if (z[a] - z[b] == d)
					r.Add(i + 1);
			}
			else
			{
				r.Add(i + 1);

				if (gs[a].Count < gs[b].Count)
				{
					(a, b) = (b, a);
					d = -d;
				}
				var l = gs[a];
				var t = gs[b];
				var v0 = z[a] - z[b] - d;

				foreach (var j in t)
				{
					l.Add(j);
					gs[j] = l;
					z[j] += v0;
				}
			}
		}

		return string.Join(" ", r);
	}
}
