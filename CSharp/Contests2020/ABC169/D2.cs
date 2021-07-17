using System;
using System.Collections.Generic;
using System.Linq;

class D2
{
	static void Main()
	{
		var n = long.Parse(Console.ReadLine());
		int ArgSum(int s) => Enumerable.Range(1, 99).TakeWhile(i => i * (i + 1) / 2 <= s).Last();
		Console.WriteLine(Factorize(n).GroupBy(p => p).Sum(g => ArgSum(g.Count())));
	}

	static long[] Factorize(long n)
	{
		var r = new List<long>();
		for (long x = 2; x * x <= n && n > 1; ++x) while (n % x == 0) { r.Add(x); n /= x; }
		if (n > 1) r.Add(n);
		return r.ToArray();
	}
}
