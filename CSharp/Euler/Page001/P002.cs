using System;
using System.Collections.Generic;
using System.Linq;

class P002
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = 4000000;

		var r = 2;
		var (t1, t2) = (1, 2);
		int t;

		while ((t = t1 + t2) <= n)
		{
			if (t % 2 == 0) r += t;
			(t1, t2) = (t2, t);
		}
		return r;
	}
}
