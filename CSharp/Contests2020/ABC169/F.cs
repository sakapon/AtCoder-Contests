using System;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var (n, s) = Read2();
		var a = Read();

		var dp = new long[s + 1];
		dp[0] = MPow(2, n);

		for (int k = 0; k < n; k++)
		{
			for (int i = s - 1; i >= 0; i--)
			{
				if (dp[i] == 0) continue;
				var nv = i + a[k];
				if (nv > s) continue;
				dp[nv] += dp[i] * MHalf;
				dp[nv] %= M;
			}
		}
		Console.WriteLine(dp[s]);
	}

	const long M = 998244353;
	const long MHalf = (M + 1) / 2;
	static long MPow(long b, long i)
	{
		long r = 1;
		for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
		return r;
	}
}
