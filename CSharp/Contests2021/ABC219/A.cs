using System;
using System.Collections.Generic;
using System.Linq;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var x = int.Parse(Console.ReadLine());

		var h = new[] { 0, 40, 70, 90, 1000 }.First(v => v > x);
		if (h == 1000) return "expert";
		return h - x;
	}
}
