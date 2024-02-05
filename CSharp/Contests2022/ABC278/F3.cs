using System;
using System.Collections.Generic;
using System.Linq;

class F3
{
	static void Main() => Console.WriteLine(Solve() ? "First" : "Second");
	static bool Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

		var dp = NewArray2(1 << n, n, false);

		// uint で x >= 0 は使えないことに注意
		for (uint x = (1U << n) - 1; x > 0; x--)
		{
			for (int i = 0; i < n; i++)
			{
				if ((x & (1U << i)) == 0) continue;

				dp[x][i] = true;

				for (int ni = 0; ni < n; ni++)
				{
					var nx = x | (1U << ni);
					if (nx == x) continue;
					if (s[i][^1] != s[ni][0]) continue;

					if (dp[nx][ni])
					{
						dp[x][i] = false;
						break;
					}
				}
			}
		}
		return Enumerable.Range(0, n).Any(i => dp[1 << i][i]);
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
