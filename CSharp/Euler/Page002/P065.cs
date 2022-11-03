using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class P065
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var sqrt2 = Enumerable.Repeat(2L, 9).Prepend(1).ToArray();
		var e = ContinuedE().Take(100).ToArray();
		//return ConvergentBig(e);

		var num = ConvergentBig(e).num;
		return num.ToString().Sum(c => c - '0');
	}

	// e の連分数展開
	static IEnumerable<long> ContinuedE()
	{
		yield return 2;
		yield return 1;

		for (int k = 1; ; k++)
		{
			yield return k << 1;
			yield return 1;
			yield return 1;
		}
	}

	// a: 連分数の標準形
	static (long num, long denom) Convergent(long[] a)
	{
		var (u, v) = (a[^1], 1L);

		for (int i = a.Length - 2; i >= 0; i--)
		{
			(u, v) = (v + u * a[i], u);
		}
		return (u, v);
	}

	// a: 連分数の標準形
	static (BigInteger num, BigInteger denom) ConvergentBig(long[] a)
	{
		BigInteger u = a[^1], v = 1;

		for (int i = a.Length - 2; i >= 0; i--)
		{
			(u, v) = (v + u * a[i], u);
		}
		return (u, v);
	}
}
