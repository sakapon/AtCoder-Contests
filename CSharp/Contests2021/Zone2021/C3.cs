using System;
using System.Collections.Generic;
using System.Linq;

class C3
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read());

		var r5 = Enumerable.Range(0, 5).ToArray();
		var maxes = Array.ConvertAll(r5, k => ps.Max(p => p[k]));

		var r = 0;

		for (int i = 0; i < n; i++)
		{
			for (int j = i + 1; j < n; j++)
			{
				var mij = Array.ConvertAll(r5, k => Math.Max(ps[i][k], ps[j][k]));
				var k = Array.IndexOf(mij, mij.Min());
				mij[k] = maxes[k];
				r = Math.Max(r, mij.Min());
			}
		}
		return r;
	}
}
