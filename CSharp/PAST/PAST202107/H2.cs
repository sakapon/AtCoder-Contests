using System;
using System.Linq;

class H2
{
	const double max = double.MaxValue;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var sum = a.Sum();

		// j: b_i, k: sum
		var dp = NewArray3(n + 1, sum + 1, sum + 1, max);
		dp[1][0][0] = 0;

		for (int i = 1; i < n; i++)
		{
			for (int b = 0; b <= sum; b++)
			{
				for (int s = 0; s <= sum; s++)
				{
					if (dp[i][b][s] == max) continue;

					for (int nb = 0; nb <= sum; nb++)
					{
						var ns = s + nb;
						if (ns > sum) break;

						var d = nb - b;
						var v = dp[i][b][s] + Math.Sqrt(d * d + 1);
						dp[i + 1][nb][ns] = Math.Min(dp[i + 1][nb][ns], v);
					}
				}
			}
		}

		return dp[n][0][sum];
	}

	static T[][][] NewArray3<T>(int n1, int n2, int n3, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => Array.ConvertAll(new bool[n3], ___ => v)));
}
