using System;
using System.Linq;

class D
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		var M = 998244353;
		c = new long[n + 1];
		c[0] = 1;

		var r = 1L;
		var t = 0;
		for (int i = 0; i < n; i++)
		{
			t++;
			if (i == n - 1 || s[i] != s[i + 1])
			{
				r = r * ModCatalan(t, M) % M;
				t = 0;
			}
		}
		Console.WriteLine(r);
	}

	static long[] c;
	static long ModCatalan(int n, int mod) => c[n] != 0 ? c[n] : (c[n] = Enumerable.Range(0, n).Sum(i => ModCatalan(i, mod) * ModCatalan(n - 1 - i, mod)) % mod);
}
