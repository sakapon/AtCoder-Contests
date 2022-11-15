using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using static EulerLib8.Common;

class P056
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var r = 0L;

		for (int a = 1; a < 100; a++)
		{
			for (int b = 1; b < 100; b++)
			{
				var v = BigInteger.Pow(a, b);
				ChMax(ref r, v.ToString().Sum(c => c - '0'));
			}
		}
		return r;
	}
}
