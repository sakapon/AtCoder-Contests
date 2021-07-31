using System;
using System.Collections.Generic;
using System.Linq;

class G
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var p = long.Parse(Console.ReadLine());

		var ds = Divisors(p - 1);

		var r = 1L;

		foreach (var d in ds)
		{
			var t = Totient(d) % M;
			r += d % M * t;
			r %= M;
		}

		return r;
	}

	const long M = 998244353;

	// すべての約数 O(√n)
	static long[] Divisors(long n)
	{
		var r = new List<long>();
		for (long x = 1; x * x <= n; ++x) if (n % x == 0) r.Add(x);
		var i = r.Count - 1;
		if (r[i] * r[i] == n) --i;
		for (; i >= 0; --i) r.Add(n / r[i]);
		return r.ToArray();
	}

	// オイラーの φ 関数 O(√n)
	// Factorize をもとにしています。
	// 候補 x を 2 または奇数に限定することで高速化できます。
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
