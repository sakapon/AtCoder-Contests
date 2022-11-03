using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class P100
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		// n: total
		// m: blue
		// n * (n-1) = 2 * m * (m-1)
		// m / n > 1 / √2 > (m-1) / (n-1) 
		// (1 / √2) * n < m < (1 / √2) * n + 1 - 1 / √2

		BigInteger nmin = 1000000;
		double c = Math.Sqrt(2) / 2;

		for (BigInteger n = nmin; ; n++)
		{
			double mmin = c * (long)n;
			double mmax = mmin + 1 - c;
			BigInteger n2 = n * (n - 1) / 2;

			for (BigInteger m = (BigInteger)Math.Ceiling(mmin); (long)m < mmax; m++)
			{
				if (n2 == m * (m - 1))
				{
					return n;
				}
			}
		}
		throw new InvalidOperationException();
	}
}
