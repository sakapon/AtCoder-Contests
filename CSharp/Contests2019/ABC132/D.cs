using System;
using System.Linq;

class D
{
	static void Main()
	{
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();

		var M = 1000000007;
		for (int i = 1; i <= a[1]; i++)
			Console.WriteLine((long)ModNcr(a[0] - a[1] + 1, i, M) * ModNcr(a[1] - 1, i - 1, M) % M);
	}

	static int ModPow(int b, int i, int mod)
	{
		for (var r = 1; ; b = (int)((long)b * b % mod))
		{
			if (i % 2 > 0) r = (int)((long)r * b % mod);
			if ((i /= 2) < 1) return r;
		}
	}
	static int ModInv(int x, int p) => ModPow(x, p - 2, p);
	static int ModFactorial(int n, int mod) => n == 0 ? 1 : (int)Enumerable.Range(1, n).Select(x => (long)x).Aggregate((x, y) => x * y % mod);
	static int ModNpr(int n, int r, int mod) => r == 0 ? 1 : (int)Enumerable.Range(n - r + 1, r).Select(x => (long)x).Aggregate((x, y) => x * y % mod);
	static int ModNcr(int n, int r, int p) => n - r < r ? ModNcr(n, n - r, p) : (int)((long)ModNpr(n, r, p) * ModInv(ModFactorial(r, p), p) % p);
}
