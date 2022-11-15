using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using EulerLib8;

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
		var sqrt = BinarySearch.Last(0, v, x => x * x <= v);
		return sqrt.ToString()[..100].Sum(c => c - '0');
	}
}
