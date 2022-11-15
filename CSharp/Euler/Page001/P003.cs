using System;
using System.Collections.Generic;
using System.Linq;
using EulerLib8.Numerics;

class P003
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		const long n = 600851475143;
		return Primes.Factorize(n)[^1];
	}
}
