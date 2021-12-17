using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long x, long y) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2L());

		var comp = Comparer<(long x, long y)>.Create((p, q) => (q.x * p.y).CompareTo(p.x * q.y));

		var r = 0;
		var tp = (1L, 0L);

		foreach (var p in ps.OrderBy(p => (p.x - 1, p.y), comp))
		{
			var (x, y) = p;

			if (comp.Compare(tp, (x, y - 1)) <= 0)
			{
				r++;
				tp = (x - 1, y);
			}
		}

		return r;
	}
}
