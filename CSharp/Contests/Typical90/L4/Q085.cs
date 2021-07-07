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

		foreach (var a in ds)
		{
			var q = k / a;
			var ds2 = Divisors(q);

			foreach (var b in ds2)
			{
				if (a > b) continue;
				var c = q / b;
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
