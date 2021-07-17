using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main()
	{
		var (a, b) = Read2L();

		var n = (int)(b - a + 1);
		var rn = Enumerable.Range(0, n).Select(i => a + i).ToArray();
		var pn = GetPrimes(n);
		var fn = new int[n];

		for (int i = 0; i < n; i++)
			for (int j = 0; j < pn.Length; j++)
				if (rn[i] % pn[j] == 0)
					fn[i] |= 1 << j;

		var dp = new long[1 << pn.Length];
		dp[0] = 1;

		for (int i = 0; i < n; i++)
		{
			for (int x = 0; x < dp.Length; x++)
			{
				if ((x & fn[i]) == 0)
				{
					// つねに coprime である数 (fn[i] == 0) の場合は 2 倍になります。
					dp[x | fn[i]] += dp[x];
				}
			}
		}
		Console.WriteLine(dp.Sum());
	}

	static int[] GetPrimes(int n)
	{
		var b = new bool[n + 1];
		for (int p = 2; p * p <= n; ++p) if (!b[p]) for (int x = p * p; x <= n; x += p) b[x] = true;
		var r = new List<int>();
		for (int x = 2; x <= n; ++x) if (!b[x]) r.Add(x);
		return r.ToArray();
	}
}
