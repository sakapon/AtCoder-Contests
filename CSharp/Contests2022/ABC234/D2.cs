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
		var (n, k) = Read2();
		var p = Read();

		var r = new List<int>();
		var q = Enumerable.Range(0, n).OrderBy(i => p[i]).ToArray();

		var qi = 0;
		for (int i = k; i <= n; i++)
		{
			if (i > k && p[q[qi]] < p[i - 1]) qi++;
			while (q[qi] >= i) qi++;
			r.Add(p[q[qi]]);
		}
		return string.Join("\n", r);
	}
}
