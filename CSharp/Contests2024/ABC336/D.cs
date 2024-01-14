class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var dp1 = DP(a);
		Array.Reverse(a);
		var dp2 = DP(a);
		Array.Reverse(dp2);
		return Enumerable.Range(0, n).Max(i => Math.Min(dp1[i], dp2[i]));

		int[] DP(int[] a)
		{
			var dp = new int[n];
			dp[0] = 1;

			for (int i = 1; i < n; i++)
			{
				var t = Math.Min(i + 1, dp[i - 1] + 1);
				dp[i] = Math.Min(a[i], t);
			}
			return dp;
		}
	}
}
