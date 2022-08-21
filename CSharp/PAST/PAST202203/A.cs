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
		var a = Read();

		var b = new[] { a[0] * a[1], a[1] * a[2], a[2] * a[0] };
		Array.Sort(b);
		return $"{b[0]} {b[2]}";
	}
}
