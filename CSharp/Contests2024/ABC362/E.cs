class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var r = new long[n + 1];
		r[1] = n;

		for (int i = 0; i < n; i++)
		{
			for (int j = i + 1; j < n; j++)
			{
				var d = a[j] - a[i];

				var dp = new Dictionary<int, long[]>();
				var vs = new long[n + 1];
				vs[2] = 1;
				dp[a[j]] = vs;

				for (int k = j + 1; k < n; k++)
				{
					if (!dp.ContainsKey(a[k] - d)) continue;

					var v0 = dp[a[k] - d];
					if (dp.ContainsKey(a[k]))
					{
						var v1 = dp[a[k]];
						for (int l = n - 1; l >= 0; l--)
						{
							v1[l + 1] += v0[l];
							v1[l + 1] %= M;
						}
					}
					else
					{
						var v1 = new long[n + 1];
						for (int l = 0; l < n; l++)
						{
							v1[l + 1] = v0[l];
						}
						dp[a[k]] = v1;
					}
				}

				foreach (var v2 in dp.Values)
				{
					for (int l = 0; l <= n; l++)
					{
						r[l] += v2[l];
						r[l] %= M;
					}
				}
			}
		}

		return string.Join(" ", r[1..]);
	}

	const long M = 998244353;
}
