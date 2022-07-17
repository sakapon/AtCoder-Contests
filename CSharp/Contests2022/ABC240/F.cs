using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve())));
	static object Solve()
	{
		var (n, m) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Read2L());

		var r = ps[0].Item1;
		var b0 = 0L;
		var a0 = 0L;

		foreach (var (c, count) in ps)
		{
			// b(x) = b0 + c * (x - x0)
			var bl = b0 + c;
			var br = b0 + c * count;

			// 上に凸
			if (b0 > 0 && br < 0)
			{
				var count1 = -b0 / c;
				var b1 = b0 + c * count1;
				var a1 = a0 + (bl + b1) * count1 / 2;
				r = Math.Max(r, a1);
			}

			a0 += (bl + br) * count / 2;
			r = Math.Max(r, a0);
			b0 += c * count;
		}

		return r;
	}
}
