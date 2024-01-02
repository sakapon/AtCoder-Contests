using System;
using System.Collections.Generic;
using System.Linq;

class E2
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
		var pset = Array.ConvertAll(new bool[l], _ => Read2()).ToHashSet();

		var js = Enumerable.Range(0, m).OrderBy(j => -b[j]).ToArray();
		var r = 0L;

		for (int i = 0; i < n; i++)
		{
			foreach (var j in js)
			{
				if (pset.Contains((i + 1, j + 1))) continue;
				r = Math.Max(r, a[i] + b[j]);
				break;
			}
		}
		return r;
	}
}
