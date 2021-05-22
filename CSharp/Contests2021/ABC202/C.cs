using System;
using System.Collections.Generic;
using System.Linq;

class C
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
		var c = Read();

		var ac = Tally(a, n);

		var r = 0L;

		for (int i = 0; i < n; i++)
		{
			r += ac[b[c[i] - 1]];
		}

		return r;
	}

	static int[] Tally(int[] a, int max)
	{
		var r = new int[max + 1];
		foreach (var x in a) ++r[x];
		return r;
	}
}
