using System;

class B2
{
	static void Main()
	{
		var n = long.Parse(Console.ReadLine());
		var x = (FloorSqrtDec()(9 + 8 * n) - 1) / 2;
		Console.WriteLine(n + 1 - x);
	}

	static Func<decimal, long> FloorDec(long l, long r, Func<decimal, decimal> f) => y => Last(l, r, x => f(x) <= y);
	static Func<decimal, long> FloorSqrtDec() => FloorDec(-1, 1L << 45, x => x * x);

	static long Last(long l, long r, Func<long, bool> f)
	{
		long m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
	}
}
