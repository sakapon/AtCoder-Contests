using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static void Main()
	{
		var n = long.Parse(Console.ReadLine());

		var f = Factorize(n).GroupBy(p => p).ToDictionary(g => g.Key, g => g.Count());
		var r = 0;
		foreach (var kv in f)
		{
			var c = kv.Value;
			for (int i = 1; i <= c; i++)
			{
				r++;
				c -= i;
			}
		}
		Console.WriteLine(r);
	}

	static long[] Factorize(long n)
	{
		var r = new List<long>();
		for (long x = 2; x * x <= n && n > 1; ++x) while (n % x == 0) { r.Add(x); n /= x; }
		if (n > 1) r.Add(n);
		return r.ToArray();
	}
}
