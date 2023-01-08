using System;
using System.Collections.Generic;
using System.Linq;

class D2
{
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve())));
	static object Solve()
	{
		var n = long.Parse(Console.ReadLine());

		for (long x = 2; x < 3000000; x++)
		{
			if (n % (x * x) == 0)
			{
				var q = n / (x * x);
				return $"{x} {q}";
			}

			if (n % x == 0)
			{
				var p2 = n / x;
				var p = First(1, (1L << 30) * 5 / 2, x => x * x >= p2);
				return $"{p} {x}";
			}
		}

		return -1;
	}

	static long First(long l, long r, Func<long, bool> f)
	{
		long m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
