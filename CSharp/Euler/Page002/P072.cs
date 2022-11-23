using System;
using System.Collections.Generic;
using System.Linq;
using EulerLib8.Numerics;

class P072
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		const int d_max = 1000000;

		var phi = Primes.Totients(d_max);
		phi[1] = 0;
		return phi.Sum(x => (long)x);
	}
}
