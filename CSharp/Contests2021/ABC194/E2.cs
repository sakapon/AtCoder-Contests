using System;
using System.Collections.Generic;
using System.Linq;

class E2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = Read();

		var c = new int[n + 1];
		for (int i = 0; i < m; i++)
			c[a[i]]++;

		var r = 0;
		while (c[r] != 0) r++;

		for (int i = 0; i < n - m; i++)
		{
			c[a[i + m]]++;
			if (--c[a[i]] == 0) r = Math.Min(r, a[i]);
		}
		return r;
	}
}
