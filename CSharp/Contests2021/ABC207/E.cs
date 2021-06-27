using System;
using System.Linq;

class E
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();

		var cs = new long[n + 1][];
		cs[0] = CumSumL(a);

		for (int i = 1; i <= n; i++)
		{
			cs[i] = new long[n + 1];
			for (int j = 1; j <= n; j++)
			{
				cs[i][j] = cs[0][j] % i;
			}
		}

		var dp = NewArray2<long>(n + 1, n + 1);
		dp[0][0] = 1;

		// b_i で j (mod i) の場合
		var ds = NewArray2<long>(n + 1, n + 1);
		ds[0][0] = 1;

		for (int i = 1; i <= n; i++)
		{
			for (int j = n; j > 0; j--)
			{
				var rem = cs[j][i];
				dp[i][j] = ds[j - 1][rem];

				if (j < n)
				{
					rem = cs[j + 1][i];
					ds[j][rem] += dp[i][j];
					ds[j][rem] %= M;
				}
			}
		}

		return dp[n].Sum() % M;
	}

	const long M = 1000000007;
	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));

	static long[] CumSumL(long[] a)
	{
		var s = new long[a.Length + 1];
		for (int i = 0; i < a.Length; ++i) s[i + 1] = s[i] + a[i];
		return s;
	}
}
