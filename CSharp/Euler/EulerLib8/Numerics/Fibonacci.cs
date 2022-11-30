using System;
using System.Collections.Generic;
using System.Numerics;

namespace EulerLib8.Numerics
{
	public static class Fibonacci
	{
		public static IEnumerable<BigInteger> FibonacciSeq(BigInteger f0, BigInteger f1)
		{
			yield return f0;
			yield return f1;

			while (true)
			{
				(f0, f1) = (f1, f0 + f1);
				yield return f1;
			}
		}
	}
}
