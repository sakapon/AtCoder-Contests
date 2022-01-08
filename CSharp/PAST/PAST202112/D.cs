using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var b = Read();

		var r = Enumerable.Range(1, n)
			.OrderByDescending(i => a[i - 1] + b[i - 1])
			.ThenByDescending(i => a[i - 1])
			.ThenBy(i => i);
		return string.Join(" ", r);
	}
}
