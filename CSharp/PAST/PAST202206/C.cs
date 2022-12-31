using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = ((long, int))Read2();

		if (n == 1) return new string('o', m);

		var k = 1;
		var pn = n;
		while ((pn *= n) <= 1000000000L) k++;
		return k >= m ? new string('o', m) : new string('o', k) + new string('x', m - k);
	}
}
