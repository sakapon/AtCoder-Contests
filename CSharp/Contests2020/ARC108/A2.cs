using System;

class A2
{
	static decimal[] ReadDec() => Array.ConvertAll(Console.ReadLine().Split(), decimal.Parse);
	static (decimal, decimal) Read2Dec() { var a = ReadDec(); return (a[0], a[1]); }
	static void WriteYesNo(bool b) => Console.WriteLine(b ? "Yes" : "No");
	static void Main()
	{
		var (s, p) = Read2Dec();
		WriteYesNo(IsSquareNumberDec(s * s - 4 * p));
	}

	static Func<decimal, long> FloorDec(long l, long r, Func<decimal, decimal> f) => y => Last(l, r, x => f(x) <= y);
	static Func<decimal, long> FloorSqrtDec() => FloorDec(-1, 1L << 45, x => x * x);

	static bool IsSquareNumberDec(decimal x)
	{
		decimal r = FloorSqrtDec()(x);
		return r * r == x;
	}

	static long Last(long l, long r, Func<long, bool> f)
	{
		long m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
	}
}
