using System;

class E2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, d) = Read2();

		var sum = 0L;
		var p2 = MPows(2, Math.Max(n, d + 1));

		for (int i = 0; i < n; i++)
		{
			if (n - i > d)
				sum += p2[d + 1] * p2[i] % M;

			var m = Math.Min(d - 1, 2 * (n - i - 1) - d + 1);
			if (m > 0)
				sum += p2[d - 1] * m % M * p2[i] % M;
		}

		return sum % M;
	}

	const long M = 998244353;

	static long[] MPows(long b, int n)
	{
		var p = new long[n + 1];
		p[0] = 1;
		for (int i = 0; i < n; ++i) p[i + 1] = p[i] * b % M;
		return p;
	}
}
