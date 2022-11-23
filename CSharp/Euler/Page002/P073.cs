using System;
using System.Collections.Generic;
using System.Linq;
using EulerLib8.Numerics;

class P073
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		const int d_max = 12000;

		var r = 0;
		for (int d = 4; d <= d_max; d++)
		{
			var n = (int)Math.Ceiling(d / 3D);
			var n_max = (int)Math.Floor(d / 2D);

			for (; n <= n_max; n++)
			{
				if (Primes.Gcd(d, n) == 1) r++;
			}
		}
		return r;
	}
}
