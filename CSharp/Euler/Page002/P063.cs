using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class P063
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var r = 0L;

		for (int n = 1; ; n++)
		{
			var t = Enumerable.Range(1, 9).Count(i => BigInteger.Pow(i, n).ToString().Length == n);
			if (t == 0) break;
			r += t;
		}
		return r;
	}
}
