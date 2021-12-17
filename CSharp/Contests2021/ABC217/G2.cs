using System;

class G2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();

		var dp = new long[n + 1];
		var t = new long[n + 1];
		dp[0] = 1;

		for (int i = 1; i <= n; i++)
		{
			Array.Clear(t, 0, t.Length);

			for (int j = 1; j <= n; j++)
			{
				t[j] += dp[j - 1];
				t[j] += dp[j] * (j - (i - 1) / m);
				t[j] %= M;
			}
			(dp, t) = (t, dp);
		}

		return string.Join("\n", dp[1..]);
	}

	const long M = 998244353;
}
