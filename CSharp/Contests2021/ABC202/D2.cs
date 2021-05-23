using System;

class D2
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (a, b, k) = ((int, int, long))Read3L();

		var ncr = GetNcr();

		var r = "";
		while (a > 0 || b > 0)
		{
			var c = a == 0 ? 0 : ncr[a + b - 1, a - 1];
			if (k <= c)
			{
				r += 'a';
				a--;
			}
			else
			{
				r += 'b';
				b--;
				k -= c;
			}
		}
		return r;
	}

	static long[,] GetNcr()
	{
		var n = 66;
		var dp = new long[n + 1, n + 1];
		for (int i = 0; i <= n; i++)
		{
			dp[i, 0] = dp[i, i] = 1;
			for (int j = 1; j < i; j++)
				dp[i, j] = dp[i - 1, j - 1] + dp[i - 1, j];
		}
		return dp;
	}
}
