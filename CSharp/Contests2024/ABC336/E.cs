using CoderLib8.Values;

class E
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine().Select(c => c - '0').ToArray();
		var n = s.Length;

		const int smax = 130;

		// i: 桁数
		// j: 桁和
		// k: mod m
		// 値: 個数

		// 同じ
		var dp0 = new SeqArray2<long>(smax + 1, smax);
		var dt0 = new SeqArray2<long>(smax + 1, smax);
		// 未満
		var dp1 = new SeqArray2<long>(smax + 1, smax);
		var dt1 = new SeqArray2<long>(smax + 1, smax);

		return Enumerable.Range(1, smax).Sum(ForSum);

		// m: 桁和
		long ForSum(int m)
		{
			dp0.Clear();
			dp1.Clear();
			dp0[0, 0] = 1;

			for (int i = 0; i < n; i++)
			{
				for (int j = 0; j < smax; j++)
				{
					for (int k = 0; k < m; k++)
					{
						if (dp0[j, k] == 0 && dp1[j, k] == 0) continue;

						for (int d = 0; d < 10; d++)
						{
							var nj = j + d;
							if (nj >= smax) continue;
							var nk = (10 * k + d) % m;

							dt1[nj, nk] += dp1[j, k];

							if (d == s[i])
								dt0[nj, nk] += dp0[j, k];
							else if (d < s[i])
								dt1[nj, nk] += dp0[j, k];
						}
					}
				}

				(dp0, dt0) = (dt0, dp0);
				(dp1, dt1) = (dt1, dp1);
				dt0.Clear();
				dt1.Clear();
			}
			return dp0[m, 0] + dp1[m, 0];
		}
	}
}
