using System;
using System.Collections.Generic;

class B2
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = ((long, int))Read2L();

		var a = Convert(n - 1, k);

		var (r, p) = (1L, 1L);

		for (int i = 0; i < a.Length; i++)
		{
			if (a[i] == k - 1) r = 0;
			r += a[i] * p;
			p *= k - 1;
		}
		return r;
	}

	static int[] Convert(long x, int b)
	{
		var r = new List<int>();
		for (; x > 0; x /= b) r.Add((int)(x % b));
		return r.ToArray();
	}
}
