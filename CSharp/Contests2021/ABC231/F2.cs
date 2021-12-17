using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.DataTrees.Bsts;

class F2
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

		var gs = Enumerable.Range(0, n)
			.Select(i => (a: a[i], b: b[i]))
			.GroupBy(p => p)
			.OrderBy(g => g.Key.a)
			.ThenBy(g => -g.Key.b)
			.ToArray();

		var r = 0L;
		var set = new WBMultiSet<int>();

		foreach (var g in gs)
		{
			var v = g.Key.b;
			var c = g.LongCount();

			r += c * set.GetCount(x => x >= v, x => true);
			r += c * c;

			while (c-- > 0)
				set.Add(v);
		}

		return r;
	}
}
