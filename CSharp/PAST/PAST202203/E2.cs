using System;
using System.Collections.Generic;
using System.Linq;

class E2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();

		for (var d = DateTime.ParseExact(s, "yyyy/MM/dd", null); ; d = d.AddDays(1))
		{
			var ds = d.ToString("yyyy/MM/dd");
			if (ds.Distinct().Count() == 3) return ds;
		}
	}
}
