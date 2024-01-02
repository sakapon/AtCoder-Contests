using System;
using System.Collections.Generic;
using System.Linq;
using WBTrees;

class F2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var ps = ReadL();
		var ls = Read();
		var ds = Read();

		Array.Sort(ds, ls);
		var set = new WBMultiSet<long>();
		set.Initialize(ps);

		var r = ps.Sum();

		for (int i = m - 1; i >= 0; i--)
		{
			var l = ls[i];
			var d = ds[i];

			var node = set.RemoveFirst(x => x >= l);
			if (node != null) r -= d;
		}

		return r;
	}
}
