class F2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var r = 0L;
		var dp = new long[n];
		dp[0] = 1;

		for (int i = 0; i < n; i++)
		{
			dp[i] %= M;
			r += dp[i];

			for (int ni = i + a[i]; ni < n; ni += a[i])
			{
				dp[ni] += dp[i];

				if (a[ni] == a[i])
				{
					dp[ni] += dp[i];
					r -= dp[i];
					break;
				}
			}
		}
		return MInt(r);
	}

	const long M = 998244353;
	static long MInt(long x) => (x %= M) < 0 ? x + M : x;
}
