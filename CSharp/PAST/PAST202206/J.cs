using System;
using System.Collections.Generic;
using System.Linq;

class J
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();
		var t = Console.ReadLine();

		var d = DateTime.Parse(s);
		var ed = DateTime.Parse(t);
		var last = new DateTime(9999, 12, 31);

		var r = 0;
		var day = TimeSpan.FromDays(1);

		for (; d <= ed; d += day)
		{
			if (d.DayOfWeek == DayOfWeek.Saturday || d.DayOfWeek == DayOfWeek.Sunday)
			{
				r++;
			}
			if (d == last) break;
		}
		return r;
	}
}
