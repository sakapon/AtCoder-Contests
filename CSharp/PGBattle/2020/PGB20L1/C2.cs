using System;
using System.Collections.Generic;
using System.Linq;

class C2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var r = new int[2 * n - 1];
		var p = Enumerable.Range(0, 2 * n).GroupBy(i => a[i]).Select(g => g.ElementAt(1) - g.ElementAt(0) - 1);

		foreach (var x in p)
		{
			r[x]++;
		}
		for (int i = 1; i < r.Length; i++)
		{
			r[i] += r[i - 1];
		}
		return string.Join("\n", r);
	}
}
