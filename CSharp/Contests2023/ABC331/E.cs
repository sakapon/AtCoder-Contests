using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int c, int d) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m, l) = Read3();
		var a = ReadL();
		var b = ReadL();
		var ps = Array.ConvertAll(new bool[l], _ => Read2());

		var r = 0L;
		var bset = new SortedSet<long>(b);
		var bads = ps.ToLookup(p => p.c, p => p.d);

		for (int i = 0; i < n; i++)
		{
			foreach (var j in bads[i + 1]) bset.Remove(b[j - 1]);
			r = Math.Max(r, a[i] + bset.Max);
			foreach (var j in bads[i + 1]) bset.Add(b[j - 1]);
		}
		return r;
	}
}
