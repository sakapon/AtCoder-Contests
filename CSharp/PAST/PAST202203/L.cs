using System;
using System.Collections.Generic;
using System.Linq;

class L
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (k, m) = Read2();
		var ps = Array.ConvertAll(new bool[k], _ => Read2L());

		const int imax = 40;
		// 100...00 (0 が 2^i 文字) = 10^(2^i)
		var r10 = new long[imax];
		//  11...11 (1 が 2^i 文字)
		var r1 = new long[imax];

		r10[0] = 10 % m;
		r1[0] = 1 % m;

		for (int i = 1; i < imax; i++)
		{
			r10[i] = r10[i - 1] * r10[i - 1] % m;
			r1[i] = r1[i - 1] * (r10[i - 1] + 1) % m;
		}

		var r = 0L;
		foreach (var (c, d) in ps)
		{
			var v10 = 1L;
			var v1 = 0L;
			for (int i = 0; i < imax; i++)
			{
				if ((d & (1L << i)) != 0)
				{
					v10 = v10 * r10[i] % m;
					v1 = (v1 * r10[i] + r1[i]) % m;
				}
			}
			r = (r * v10 + v1 * c) % m;
		}
		return r;
	}
}
