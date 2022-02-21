using System;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine().Select(c => c - '0').ToArray();
		var n = s.Length;
		var m = int.Parse(Console.ReadLine());
		var c = Read();

		// 0: =
		// 1: <
		var pm = 1 << 10;
		var dp = NewArray3(n + 1, pm, 2, min);
		dp[0][0][0] = 0;

		long v;
		for (int i = 0; i < n; i++)
		{
			for (int x = 0; x < pm; x++)
			{
				if ((v = dp[i][x][0]) != min)
				{
					int k = 0;
					for (; k < s[i]; k++)
					{
						var nx = (x, k) == (0, 0) ? 0 : (x | (1 << k));
						var nv = v * 10 + k;
						if (dp[i + 1][nx][1] == min) dp[i + 1][nx][1] = 0;
						dp[i + 1][nx][1] += nv;
					}

					{
						var nx = (x, k) == (0, 0) ? 0 : (x | (1 << k));
						var nv = v * 10 + k;
						if (dp[i + 1][nx][0] == min) dp[i + 1][nx][0] = 0;
						dp[i + 1][nx][0] += nv;
					}
				}

				if ((v = dp[i][x][1]) != min)
				{
					for (int k = 0; k < 10; k++)
					{
						var nx = (x, k) == (0, 0) ? 0 : (x | (1 << k));
						var nv = v * 10 + k;
						if (dp[i + 1][nx][1] == min) dp[i + 1][nx][1] = 0;
						dp[i + 1][nx][1] += nv;
					}
				}
			}

			for (int x = 0; x < pm; x++)
			{
				dp[i + 1][x][0] %= M;
				dp[i + 1][x][1] %= M;
			}
		}

		var f = c.Select(x => 1 << x).Aggregate((x, y) => x | y);
		return Enumerable.Range(0, pm).Where(x => (x & f) == f).Sum(x => dp[n][x].Where(v => v != min).Sum()) % M;
	}

	const long min = -1;
	const long M = 998244353;
	static T[][][] NewArray3<T>(int n1, int n2, int n3, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => Array.ConvertAll(new bool[n3], ___ => v)));
}
