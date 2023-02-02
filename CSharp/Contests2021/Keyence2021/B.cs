using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();
		var a = Read();

		Array.Sort(a);
		var b = new int[k];

		foreach (var g in a.GroupBy(x => x))
		{
			var v = g.Key;
			var count = Math.Min(k, g.Count());

			for (int i = 0; i < count; i++)
			{
				if (b[i] == v) b[i]++;
			}
		}
		return b.Sum();
	}
}
