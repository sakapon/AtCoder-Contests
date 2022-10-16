using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, s0) = Read2();
		long s = s0;
		var d = Read();

		var p10 = new long[15];
		p10[0] = 1;
		for (int i = 1; i < 15; i++)
		{
			p10[i] = p10[i - 1] * 10;
		}

		var mins = Array.ConvertAll(d, x => p10[x - 1]);
		var maxs = Array.ConvertAll(d, x => p10[x] - 1);

		var min = mins.Sum();
		var max = maxs.Sum();
		if (s < min || max < s) return -1;

		s -= min;

		for (int i = 0; i < n && s > 0; i++)
		{
			var delta = Math.Min(maxs[i] - mins[i], s);
			mins[i] += delta;
			s -= delta;
		}
		return string.Join(" ", mins);
	}
}
