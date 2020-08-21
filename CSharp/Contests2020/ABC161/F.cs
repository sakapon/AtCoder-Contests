using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static void Main()
	{
		var n = long.Parse(Console.ReadLine());

		var r = Divisors(n - 1).Length - 1;

		r += Divisors(n).Skip(1).Count(k =>
		{
			var x = n;
			while (x % k == 0) x /= k;
			return x % k == 1;
		});
		Console.WriteLine(r);
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
