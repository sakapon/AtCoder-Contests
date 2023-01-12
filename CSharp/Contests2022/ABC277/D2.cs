using System;
using System.Collections.Generic;
using System.Linq;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = ReadL();

		if (a.Distinct().Count() == m) return 0;

		var sum = a.Sum();
		Array.Sort(a);
		var v = a.Concat(a).ToArray();
		a = a.Concat(a.Select(x => x + m)).ToArray();

		var si = Enumerable.Range(1, n).First(i => a[i] - a[i - 1] > 1);
		v = v[si..(si + n)];
		a = a[si..(si + n)];

		for (int i = 1; i < n; i++)
		{
			if (a[i] - a[i - 1] <= 1)
			{
				v[i] += v[i - 1];
			}
		}

		return sum - v.Max();
	}
}
