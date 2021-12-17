using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ats = Array.ConvertAll(new bool[n], _ => Read2());
		var qc = int.Parse(Console.ReadLine());
		var x = Read();

		var sum = 0L;
		var min = long.MinValue;
		var max = long.MaxValue;

		foreach (var (a, t) in ats)
		{
			if (t == 1)
			{
				sum += a;
			}
			else if (t == 2)
			{
				min = Math.Max(min, a - sum);
				if (max < min) max = min;
			}
			else
			{
				max = Math.Min(max, a - sum);
				if (max < min) min = max;
			}
		}

		return string.Join("\n", x.Select(v => Math.Max(Math.Min(v, max), min) + sum));
	}
}
