using System;
using System.Linq;

class C2
{
	static void Main()
	{
		var A = 1000000;
		Console.ReadLine();
		var a = new int[A + 1];
		foreach (var x in Console.ReadLine().Split().Select(int.Parse)) a[x]++;

		var c = new long[A + 1];
		for (var d = 1; d <= A; d++) c[d] = MInv(d);
		for (var d = 1; d <= A / 2; d++)
			for (var m = 2 * d; m <= A; m += d) c[m] = MSub(c[m], c[d]);

		var h = MInv(2);
		long s = 0;
		for (var d = 1; d <= A; d++)
		{
			long si = 0, si2 = 0;
			for (var m = d; m <= A; m += d)
			{
				if (a[m] == 0) continue;
				si = MAdd(si, MMul(a[m], m));
				si2 = MAdd(si2, MMul(a[m], MMul(m, m)));
			}
			s = MAdd(s, MMul(MMul(MSub(MMul(si, si), si2), h), c[d]));
		}
		Console.WriteLine(s);
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
