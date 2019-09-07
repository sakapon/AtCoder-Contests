using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class Modulo2Test
{
	const int M = 1000000007;
	static long MPow(long b, int i)
	{
		for (var r = 1L; ; b = b * b % M)
		{
			if (i % 2 > 0) r = r * b % M;
			if ((i /= 2) < 1) return r;
		}
	}
	static long MInv(long x) => MPow(x, M - 2);
	static long MAdd(long x, long y) => (x + y) % M;
	static long MMul(long x, long y) => x * y % M;
	static long MDiv(long x, long y) => x * MInv(y) % M;
}
