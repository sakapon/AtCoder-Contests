using System;
using System.Collections.Generic;

class Q085
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var k = long.Parse(Console.ReadLine());

		var r = 0;
		var ds = Divisors(k);

		for (int i = 0; i < ds.Length; i++)
		{
			var a = ds[i];
			var q = k / a;

			for (int j = i; j < ds.Length; j++)
			{
				var b = ds[j];
				var c = Math.DivRem(q, b, out var rem);
				if (rem != 0) continue;
				if (b > c) continue;
				r++;
			}
		}
		return r;
	}

	static long[] Divisors(long n)
	{
		var r = new List<long>();
		for (long x = 1; x * x <= n; ++x) if (n % x == 0) r.Add(x);
		var i = r.Count - 1;
		if (r[i] * r[i] == n) --i;
		for (; i >= 0; --i) r.Add(n / r[i]);
		return r.ToArray();
	}
}
