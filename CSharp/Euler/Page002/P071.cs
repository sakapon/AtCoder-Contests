using System;
using System.Collections.Generic;
using System.Linq;
using EulerLib8.Linq;

class P071
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		const int d_max = 1000000;

		var (d0, n0) = Enumerable.Range(1, d_max)
			.Select(d => (d, n: d * 3 % 7 == 0 ? d * 3 / 7 - 1 : Math.Floor(d * 3D / 7)))
			.FirstMax(p => p.n / p.d);
		return n0;
	}
}
