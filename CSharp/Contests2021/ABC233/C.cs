using System;
using System.Collections.Generic;

class C
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, x) = Read2L();
		var a = Array.ConvertAll(new bool[n], _ => ReadL()[1..]);

		var d = new Dictionary<long, int>();
		d[1] = 1;

		for (int i = 0; i < n; i++)
		{
			var nd = new Dictionary<long, int>();

			foreach (var (dv, c) in d)
			{
				foreach (var av in a[i])
				{
					if (dv > x / av) continue;
					var nv = dv * av;

					if (nd.ContainsKey(nv))
						nd[nv] += c;
					else
						nd[nv] = c;
				}
			}

			d = nd;
		}

		return d.ContainsKey(x) ? d[x] : 0;
	}
}
