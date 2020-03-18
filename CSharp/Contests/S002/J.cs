using System;
using System.Linq;

class J
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();

		var dp = new int[n + 1, 2];
		dp[1, 0] = a[0][0];
		dp[1, 1] = a[0][1];
		for (int i = 1; i < n; i++)
		{
			dp[i + 1, 0] = Math.Max(Gcd(dp[i, 0], a[i][0]), Gcd(dp[i, 1], a[i][0]));
			dp[i + 1, 1] = Math.Max(Gcd(dp[i, 0], a[i][1]), Gcd(dp[i, 1], a[i][1]));
		}
		Console.WriteLine(Math.Max(dp[n, 0], dp[n, 1]));
	}

	static int Gcd(int x, int y) { for (int r; (r = x % y) > 0; x = y, y = r) ; return y; }
}
