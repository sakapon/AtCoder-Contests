using System;
using System.Collections.Generic;
using System.Numerics;

namespace EulerLib8.Numerics
{
	public class BigCombination
	{
		static BigInteger[] Factorials(int nMax)
		{
			var f = new BigInteger[nMax + 1];
			f[0] = 1;
			for (int i = 1; i <= nMax; ++i) f[i] = f[i - 1] * i;
			return f;
		}

		// nPr, nCr を O(1) で求めるため、階乗を O(n) で求めておきます。
		readonly BigInteger[] f;
		public BigCombination(int nMax)
		{
			f = Factorials(nMax);
		}

		public BigInteger Factorial(int n) => f[n];
		public BigInteger Npr(int n, int r) => n < r ? 0 : f[n] / f[n - r];
		public BigInteger Ncr(int n, int r) => n < r ? 0 : f[n] / f[n - r] / f[r];

		// nMax >= 2n としておく必要があります。
		public BigInteger Catalan(int n) => f[2 * n] / f[n] / f[n + 1];
	}
}
