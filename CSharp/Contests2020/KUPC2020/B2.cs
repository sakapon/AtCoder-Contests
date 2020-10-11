using System;
using System.Linq;

class B2
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		int n = h[0], k = h[1];
		var v = new int[n].Select(_ => Read()).ToArray();

		const long M = 1000000007;
		var dp = new long[n, k];
		for (int j = 0; j < k; j++) dp[0, j] = 1;

		for (int i = 1; i < n; i++)
		{
			var s = 0L;
			for (int j = 0, j_ = 0; j < k; j++)
			{
				while (j_ < k && v[i - 1][j_] <= v[i][j])
					s = (s + dp[i - 1, j_++]) % M;
				dp[i, j] = s;
			}
		}
		Console.WriteLine(Enumerable.Range(0, k).Sum(j => dp[n - 1, j]) % M);
	}
}
