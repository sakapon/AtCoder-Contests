using System;

namespace CoderLib6.Values
{
	static class ModuloHelper
	{
		//const long M = 998244353;
		const long M = 1000000007;
		static long MPow(long b, long i)
		{
			for (var r = 1L; ; b = b * b % M)
			{
				if (i % 2 > 0) r = r * b % M;
				if ((i /= 2) < 1) return r;
			}
		}
		static long MInv(long x) => MPow(x, M - 2);
		static long MInt(long x) => (x %= M) < 0 ? x + M : x;
		static long MNeg(long x) => MInt(-x);
		static long MAdd(long x, long y) => MInt(x + y);
		static long MSub(long x, long y) => MInt(x - y);
		static long MMul(long x, long y) => MInt(x * y);
		static long MDiv(long x, long y) => MInt(x * MInv(y));
	}
}
