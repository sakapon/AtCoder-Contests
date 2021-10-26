using System;
using System.Collections.Generic;

class C2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
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

		var t = d.Count;
		var r = t;

		for (int i = k; i < n; i++)
		{
			if (d.ContainsKey(c[i]) && d[c[i]] != 0)
			{
				d[c[i]]++;
			}
			else
			{
				d[c[i]] = 1;
				t++;
			}

			if (--d[c[i - k]] == 0)
			{
				t--;
			}

			r = Math.Max(r, t);
		}

		return r;
	}
}
