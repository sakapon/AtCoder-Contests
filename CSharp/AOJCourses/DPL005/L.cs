using System;

class L
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		int n = h[0], k = h[1];
		Console.WriteLine(PartitionNonZero(n, k));
	}

	const long M = 1000000007;
	static long[,] PartitionDP(int n, int k)
	{
		var dp = new long[n + 1, k + 1];
		for (int j = 1; j <= k; ++j) dp[0, j] = 1;

		for (int i = 1; i <= n; ++i)
			for (int j = 1; j <= k; ++j)
				dp[i, j] = (dp[i, j - 1] + (j <= i ? dp[i - j, j] : 0)) % M;
		return dp;
	}
	static long Partition(int n, int k) => PartitionDP(n, k)[n, k];
	static long PartitionNonZero(int n, int k) => n < k ? 0 : Partition(n - k, k);
}
