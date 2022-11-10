using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class P080
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		return Enumerable.Range(1, 100)
			.Where(x =>
			{
				var r = Math.Sqrt(x);
				return Math.Round(r) != r;
			})
			.Sum(SqrtDigitsSum);
	}

	static int SqrtDigitsSum(int x)
	{
		var v = x * BigInteger.Pow(10, 200);
		var sqrt = Last(0, v, x => x * x <= v);
		return sqrt.ToString()[..100].Sum(c => c - '0');
	}

	static BigInteger Last(BigInteger l, BigInteger r, Func<BigInteger, bool> f)
	{
		BigInteger m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
	}
}
