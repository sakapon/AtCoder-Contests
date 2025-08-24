using System;
using System.Collections.Generic;
using System.Linq;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = Read();

		var r = new List<int>();

		var mi = 0;
		var max = 0;
		var v = new int[n + 1];

		foreach (var i in a)
		{
			if (++v[i] > max)
			{
				mi = i;
				max = v[i];
			}
			else if (v[i] == max && i < mi)
			{
				mi = i;
			}

			r.Add(mi);
		}

		return string.Join("\n", r);
	}
}
