using System;

class B
{
	static void Main()
	{
		var n = long.Parse(Console.ReadLine());
		Console.WriteLine(n + 1 - InverseFloor_NaturalSum()(n + 1));
	}

	static Func<long, long> InverseFloor(long l, long r, Func<long, long> f) => y => Last(l, r, x => f(x) <= y);
	static Func<long, long> InverseFloor_NaturalSum() => InverseFloor(-1, 1L << 31, n => n * (n + 1) / 2);

	static long Last(long l, long r, Func<long, bool> f)
	{
		long m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
	}
}
