class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();
		var s = Console.ReadLine().Replace('A', '0').Replace('B', '1');

		var u = new HashSet<string>();
		var dp = new Dictionary<string, long>();
		var dt = new Dictionary<string, long>();

		for (int x = 0; x < 1 << k; x++)
		{
			var xs = Convert.ToString(x, 2).PadLeft(k, '0');
			if (IsPalindrome(xs)) u.Add(xs);
			else if (MatchesLeft(xs)) dp[xs] = 1;
		}

		for (int i = k; i < n; i++)
		{
			foreach (var (x, v) in dp)
			{
				if (s[i] != '1')
				{
					var nx = x[1..] + '0';
					if (!u.Contains(nx))
					{
						if (dt.ContainsKey(nx)) dt[nx] += v;
						else dt[nx] = v;
						dt[nx] %= M;
					}
				}

				if (s[i] != '0')
				{
					var nx = x[1..] + '1';
					if (!u.Contains(nx))
					{
						if (dt.ContainsKey(nx)) dt[nx] += v;
						else dt[nx] = v;
						dt[nx] %= M;
					}
				}
			}

			(dp, dt) = (dt, dp);
			dt.Clear();
		}

		return dp.Values.Sum() % M;

		bool MatchesLeft(string xs)
		{
			for (int i = 0; i < k; i++)
			{
				if (s[i] == '?') continue;
				if (s[i] != xs[i]) return false;
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
