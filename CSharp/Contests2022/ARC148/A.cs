using System;
using System.Collections.Generic;
using System.Linq;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var c0 = a.Count(x => (x & 1) == 0);
		if (c0 == 0 || c0 == n) return 1;

		Array.Sort(a);
		if (Enumerable.Range(0, n - 1).Select(i => a[i + 1] - a[i]).Aggregate(Gcd) != 1) return 1;
		return 2;
	}

	static int Gcd(int a, int b) { if (b == 0) return a; for (int r; (r = a % b) > 0; a = b, b = r) ; return b; }
}
