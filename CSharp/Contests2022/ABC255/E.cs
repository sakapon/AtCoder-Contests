using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var s = ReadL();
		var x = ReadL();

		var a = new long[n];
		for (int i = 1; i < n; i++)
			a[i] = s[i - 1] - a[i - 1];

		var map = new Dictionary<long, int>();

		foreach (var ln in x)
		{
			for (int i = 0; i < n; i++)
			{
				var d = ln - a[i];
				if (i % 2 == 1) d *= -1;

				if (map.ContainsKey(d)) map[d]++;
				else map[d] = 1;
			}
		}

		return map.Values.Max();
	}
}
