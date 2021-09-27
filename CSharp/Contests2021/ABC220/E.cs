using System;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, d) = Read2();

		var r = 0L;

		if (d <= n - 1)
		{
			r += 2 * (MPow(2, n) - MPow(2, d));
			r = MInt(r);
		}

		for (int i = 1; i < d; i++)
		{
			var d0 = Math.Min(i, d - i);
			var d1 = Math.Max(i, d - i);

			if (d1 <= n - 1)
			{
				r += MPow(2, n + d0 - 1) - MPow(2, d - 1);
				r = MInt(r);
			}
		}

		return r;
	}

	const long M = 998244353;
	static long MPow(long b, long i)
	{
		long r = 1;
		for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
		return r;
	}
	static long MInt(long x) => (x %= M) < 0 ? x + M : x;
}
