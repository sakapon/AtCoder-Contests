using System;
using System.Collections.Generic;

class B3
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = ((long, int))Read2L();

		var a = Convert(n - 1, k);
		var pk = PowsL(k - 1, a.Length);

		var r = 0L;

		for (int i = a.Length - 1; i >= 0; i--)
		{
			r += a[i] * pk[i];
			if (a[i] == k - 1) break;
			if (i == 0) r++;
		}
		return r;
	}

	static int[] Convert(long x, int b)
	{
		var r = new List<int>();
		for (; x > 0; x /= b) r.Add((int)(x % b));
		return r.ToArray();
	}

	static long[] PowsL(long b, int n)
	{
		var p = new long[n + 1];
		p[0] = 1;
		for (int i = 0; i < n; ++i) p[i + 1] = p[i] * b;
		return p;
	}
}
