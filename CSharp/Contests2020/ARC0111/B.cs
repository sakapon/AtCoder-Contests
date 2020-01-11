using System;
using System.Linq;

class B
{
	static void Main()
	{
		var M = 1000000007;
		var n = int.Parse(Console.ReadLine());
		var x = Console.ReadLine().Split().Select(int.Parse).ToArray();

		var f = new long[n + 1];
		f[0] = 1;
		for (int i = 0; i < n; i++)
			f[i + 1] = f[i] * (i + 1) % M;

		var dp = new long[n, n];
		for (int i = 1; i < n; i++)
			for (int j = 1; j <= i; j++)
				dp[i, j] = (j * dp[i - 1, j - 1] + (i - j) * dp[i - 1, j] + f[i - 1]) % M;

		var r = 0L;
		for (int i = 1; i < n; i++)
			r = (r + dp[n - 1, i] * (x[i] - x[i - 1])) % M;
		Console.WriteLine(r);
	}
}
