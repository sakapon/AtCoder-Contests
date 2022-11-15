using System;
using System.Collections.Generic;
using System.Linq;
using EulerLib8;
using EulerLib8.Linq;

class P071
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		const int d_max = 1000000;

		var (d0, n0) = Enumerable.Range(1, d_max)
			.Select(d => (d, n: BinarySearch.Last(0, d, x => 7 * x < 3 * d)))
			.FirstMax(p => (double)p.n / p.d);
		return n0;
	}
}
