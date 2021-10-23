using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();
		var c = Read();

		var d = new Dictionary<int, int>();

		for (int i = 0; i < k; i++)
		{
			if (d.ContainsKey(c[i]))
			{
				d[c[i]]++;
			}
			else
			{
				d[c[i]] = 1;
			}
		}

		var r = d.Count;

		for (int i = k; i < n; i++)
		{
			if (d.ContainsKey(c[i]))
			{
				d[c[i]]++;
			}
			else
			{
				d[c[i]] = 1;
			}

			d[c[i - k]]--;
			if (d[c[i - k]] == 0)
			{
				d.Remove(c[i - k]);
			}

			r = Math.Max(r, d.Count);
		}

		return r;
	}
}
