using System;
using System.Collections.Generic;
using System.Linq;
using EulerLib8.Numerics;

class P007
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var ps = Primes.GetPrimes(200000);
		return ps[10000];
	}
}
