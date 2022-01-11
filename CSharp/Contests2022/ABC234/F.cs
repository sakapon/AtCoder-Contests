using System;
using System.Linq;

static class F
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();
		var n = s.Length;

		var d = s.GroupCounts('a');
		var mc = new MCombination(n);

		var dp = NewArray2<long>(26 + 1, n + 1);
		dp[0][0] = 1;

		for (int i = 0; i < 26; i++)
		{
			for (int j = 0; j <= n; j++)
			{
				if (dp[i][j] == 0) continue;

				for (int k = 0; k <= d[i]; k++)
				{
					dp[i + 1][j + k] += dp[i][j] * mc.MNcr(j + k, k);
					dp[i + 1][j + k] %= M;
				}
			}
		}

		return dp[^1][1..].Sum() % M;
	}

	const long M = 998244353;
	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));

	public static int[] GroupCounts(this string s, char start = 'A', int count = 26)
	{
		var r = new int[count];
		foreach (var c in s) ++r[c - start];
		return r;
	}
}

public class MCombination
{
	const long M = 998244353;
	//const long M = 1000000007;
	static long MPow(long b, long i)
	{
		long r = 1;
		for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
		return r;
	}
	static long MInv(long x) => MPow(x, M - 2);

	static long[] MFactorials(int n)
	{
		var f = new long[n + 1];
		f[0] = 1;
		for (int i = 1; i <= n; ++i) f[i] = f[i - 1] * i % M;
		return f;
	}

	// nPr, nCr を O(1) で求めるため、階乗を O(n) で求めておきます。
	long[] f, f_;
	public MCombination(int nMax)
	{
		f = MFactorials(nMax);
		f_ = Array.ConvertAll(f, MInv);
	}

	public long MFactorial(int n) => f[n];
	public long MInvFactorial(int n) => f_[n];
	public long MNpr(int n, int r) => n < r ? 0 : f[n] * f_[n - r] % M;
	public long MNcr(int n, int r) => n < r ? 0 : f[n] * f_[n - r] % M * f_[r] % M;

	// nMax >= 2n としておく必要があります。
	public long MCatalan(int n) => f[2 * n] * f_[n] % M * f_[n + 1] % M;
}
