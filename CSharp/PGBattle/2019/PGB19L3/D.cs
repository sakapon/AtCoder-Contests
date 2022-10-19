using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();

		var r = 0L;

		for (int i = 1; i < n - 1; i++)
		{
			if (a[i - 1] < a[i] && a[i] > a[i + 1])
			{
				var m = Math.Max(a[i - 1], a[i + 1]);
				r += a[i] - m;
				a[i] = m;
			}
			else if (a[i - 1] > a[i] && a[i] < a[i + 1])
			{
				var m = Math.Min(a[i - 1], a[i + 1]);
				r += m - a[i];
				a[i] = m;
			}
		}

		var rn = Enumerable.Range(0, n - 1).ToArray();
		r += rn.Sum(i => Math.Abs(a[i + 1] - a[i]));
		return r;
	}
}
