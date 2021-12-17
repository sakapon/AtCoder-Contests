using System;
using System.Collections.Generic;
using System.Linq;

class A2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var x = Console.ReadLine().Split('.');
		var r = int.Parse(x[0]);
		if (x[1][0] >= '5') r++;
		return r;
	}
}
