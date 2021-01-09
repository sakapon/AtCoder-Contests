using System;
using System.Collections.Generic;
using System.Linq;

class M
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, l) = ((int, long))Read2L();
		var a = ReadL();

		var r = Last(a.Min(), l, x =>
		{
			var ps = new List<long> { 0 };

			for (int i = 0; i < n; i++)
			{
				var ns = new List<long>();
				foreach (var p in ps)
				{
					var nv = p + a[i];
					if (nv > l) continue;

					if (nv >= x && (ns.Count == 0 || ns[0] != 0))
						ns.Insert(0, 0);
					ns.Add(nv);
				}
				ps = ns;
			}

			return ps[0] == 0;
		});
		Console.WriteLine(r);
	}

	static long Last(long l, long r, Func<long, bool> f)
	{
		long m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
	}
}
