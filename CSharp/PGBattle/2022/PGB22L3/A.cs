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
		var n = int.Parse(Console.ReadLine());
		var b = Read();

		var min = b.Min();
		var max = b.Max();
		var d = (max - min) / n;

		var a = Enumerable.Range(0, n + 1).Select(i => min + d * i).ToArray();
		return a.Except(b).First();
	}
}
