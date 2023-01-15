using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.Values;

class M
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());

		var r = 0L;
		var p = MHalf;
		var mc = new MCombination(2 * n);

		for (int i = 1; i < n; i++)
		{
			p *= MQuarter;
			p %= Mod;
			r += mc.MNcr(2 * i, i) * p;
			r %= Mod;
		}
		return r;
	}

	const long Mod = 998244353;
	const long MHalf = (Mod + 1) / 2;
	const long MQuarter = MHalf * MHalf % Mod;
}
