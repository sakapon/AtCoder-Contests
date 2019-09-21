using System;
using System.Linq;
using System.Numerics;

class C
{
	static void Main()
	{
		Console.ReadLine();
		var a = Console.ReadLine().Split().Select(int.Parse).GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
		var A = a.Max(p => p.Key);

		var c = new long[A + 1];
		for (var d = 1; d <= A; d++) c[d] = MInv(d);
		for (var d = 1; d <= A / 2; d++)
			for (var m = 2 * d; m <= A; m += d) c[m] = MSub(c[m], c[d]);

		var h = MInv(2);
		BigInteger s = 0;
		for (var d = 1; d <= A; d++)
		{
			BigInteger si = 0, si2 = 0;
			for (var m = d; m <= A; m += d)
			{
				if (!a.ContainsKey(m)) continue;
				si += (BigInteger)a[m] * m;
				si2 += (BigInteger)a[m] * m * m;
			}
			s += (si * si - si2) * h * c[d] % M;
		}
		Console.WriteLine(s % M);
	}

	const int M = 998244353;
	static long MPow(long b, int i)
	{
		for (var r = 1L; ; b = b * b % M)
		{
			if (i % 2 > 0) r = r * b % M;
			if ((i /= 2) < 1) return r;
		}
	}
	static long MInv(long x) => MPow(x, M - 2);
	static long MInt(long x) => (x %= M) < 0 ? x + M : x;
	static long MAdd(long x, long y) => (x + y) % M;
	static long MSub(long x, long y) => MInt(x - y);
	static long MMul(long x, long y) => x * y % M;
	static long MDiv(long x, long y) => x * MInv(y) % M;
}
