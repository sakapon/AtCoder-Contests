class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();
		var s = Console.ReadLine();

		var u = new bool[1 << k];
		for (int x = 0; x < 1 << k; x++)
		{
			var xs = Convert.ToString(x, 2).PadLeft(k, '0');
			u[x] = IsPalindrome(xs);
		}

		var dp = new long[1 << k];
		var dt = new long[1 << k];

		for (uint x = 0; x < 1 << k; x++)
		{
			if (u[x]) continue;
			if (MatchesLeft(x)) dp[x] = 1;
		}

		for (int i = k; i < n; i++)
		{
			for (uint x = 0; x < 1 << k; x++)
			{
				if (dp[x] == 0) continue;

				var nx = x >> 1;

				if (s[i] == '?' || s[i] == 'A')
				{
					if (!u[nx])
					{
						dt[nx] += dp[x];
						dt[nx] %= M;
					}
				}

				if (s[i] == '?' || s[i] == 'B')
				{
					nx |= 1U << k - 1;
					if (!u[nx])
					{
						dt[nx] += dp[x];
						dt[nx] %= M;
					}
				}
			}

			(dp, dt) = (dt, dp);
			Array.Clear(dt, 0, dt.Length);
		}

		return dp.Sum() % M;

		bool MatchesLeft(uint x)
		{
			for (int i = 0; i < k; i++)
			{
				if (s[i] == '?') continue;
				if ((s[i] == 'A') != ((x & (1U << i)) == 0)) return false;
			}
			return true;
		}
	}

	const long M = 998244353;

	static bool IsPalindrome(string s)
	{
		for (int i = 0; i < s.Length; ++i) if (s[i] != s[s.Length - 1 - i]) return false;
		return true;
	}
}
