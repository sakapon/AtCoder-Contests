using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Numerics;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();
		var n = s.Length;

		var r = 0L;
		var p = 1L;
		for (int i = 1; i < n; i++)
		{
			p *= 26;
			r += p;
		}

		// ライブラリ改造
		r += s.ConvertFrom(26) + 1;
		return r;
	}
}
