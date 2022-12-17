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
		var w = int.Parse(Console.ReadLine());

		var r1 = Enumerable.Range(1, 100).ToArray();
		var r2 = r1.Select(x => x * 100);
		var r3 = r1.Select(x => x * 10000);
		return "300\n" + string.Join(" ", r1.Concat(r2).Concat(r3));
	}
}
