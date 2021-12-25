using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, x) = Read2L();
		var a = Array.ConvertAll(new bool[n], _ => ReadL()[1..]);

		var d = Array.ConvertAll(new bool[n + 1], _ => new Dictionary<long, int>());
		d[0][1] = 1;

		for (int i = 0; i < n; i++)
		{
			var nd = d[i + 1];

			foreach (var (v, c) in d[i])
			{
				foreach (var av in a[i])
				{
					try
					{
						checked
						{
							var nv = v * av;

							if (nd.ContainsKey(nv))
							{
								nd[nv] += c;
							}
							else
							{
								nd[nv] = c;
							}
						}
					}
					catch (Exception)
					{
					}
				}
			}
		}

		if (d[n].ContainsKey(x)) return d[n][x];
		return 0;
	}
}
