using System;
using System.Collections.Generic;
using System.Linq;

class C2
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		Console.ReadLine();
		var a = ReadL();
		var b = ReadL();

		if (a.Intersect(b).Any()) return 0;

		var d = new Dictionary<long, bool>();
		d[-1L << 40] = false;
		d[1L << 40] = false;
		foreach (var v in a) d[v] = false;
		foreach (var v in b) d[v] = true;
		var sl = new SortedList<long, bool>(d);

		var r = 1L << 40;
		foreach (var v in a)
		{
			var i = sl.IndexOfKey(v);
			var v1 = sl.Keys[i - 1];
			var v2 = sl.Keys[i + 1];

			if (sl[v1]) r = Math.Min(r, v - v1);
			if (sl[v2]) r = Math.Min(r, v2 - v);
		}
		return r;
	}
}
