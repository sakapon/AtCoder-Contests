using System;
using System.Linq;

class F
{
	static void Main()
	{
		Func<int[]> read = () => $"0 {Console.ReadLine()}".Split().Select(int.Parse).ToArray();
		var a = read();
		var h = read();
		int n = a[1], k = a[2];

		var dp = new long[n - k + 1, n + 1];
		for (int i = 1; i <= n - k; i++)
			for (int j = i; j <= i + k; j++)
				dp[i, j] = Enumerable.Range(i - 1, j - i + 1).Min(x => dp[i - 1, x] + Math.Max(0, h[j] - h[i == 1 ? 0 : x]));
		Console.WriteLine(Enumerable.Range(n - k, k + 1).Min(j => dp[n - k, j]));
	}
}
