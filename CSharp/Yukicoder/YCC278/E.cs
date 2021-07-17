using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve())));
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		while (n % 2 == 0) n /= 2;
		while (n % 5 == 0) n /= 5;
		if (n == 1) return 1;

		var k = Totient(n);
		foreach (var d in Divisors(k))
			if (MPow(10, d, n) == 1) return d;
		return -1;
	}

	static long MPow(long b, long i, long M)
	{
		long r = 1;
		for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
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

	static long Totient(long n)
	{
		var r = n;
		for (long x = 2; x * x <= n && n > 1; ++x)
			if (n % x == 0)
			{
				r = r / x * (x - 1);
				while ((n /= x) % x == 0) ;
			}
		if (n > 1) r = r / n * (n - 1);
		return r;
	}
}
