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
		var s = Console.ReadLine();

		var ys0 = new[] { "2000", "2002", "2020", "2022", "2200", "2202", "2220", "2222" };
		var ds0 = new[] { "/02/02", "/02/20", "/02/22" };

		var ys1 = new[] { "2111", "2112", "2121", "2122", "2211", "2212", "2221", "2222" };
		var ds1 = new[] { "/11/11", "/11/12", "/11/21", "/11/22", "/12/11", "/12/12", "/12/21", "/12/22" };

		var r = ys0.SelectMany(y => ds0.Select(d => y + d))
			.Concat(ys1.SelectMany(y => ds1.Select(d => y + d)))
			.Append("3000/03/03").ToArray();
		Array.Sort(r);
		return r.First(x => string.CompareOrdinal(s, x) <= 0);
	}
}
