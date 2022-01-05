using System;
using System.Collections.Generic;
using System.Linq;

class H
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine().Select(c => c - 'a').ToArray();
		var t = Console.ReadLine().Select(c => c - 'a').ToArray();
		var n = s.Length;
		var m = t.Length;

		var map = Enumerable.Range(0, 26)
			.Select(c => Enumerable.Range(0, m).Where(j => t[j] != c).ToArray())
			.ToArray();

		// s[i], t[j] まで使ったときの最大値
		var dp = NewArray2(n + 1, m + 1, -1);
		dp[0][0] = 0;

		for (int i = 0; i < n; i++)
		{
			var mapi = map[s[i]];

			for (int j = 0; j <= m; j++)
			{
				if (dp[i][j] == -1) continue;

				dp[i + 1][j] = Math.Max(dp[i + 1][j], dp[i][j]);

				var mi = First(0, mapi.Length, x => mapi[x] >= j);
				if (mi < mapi.Length)
				{
					var nj = mapi[mi] + 1;
					dp[i + 1][nj] = Math.Max(dp[i + 1][nj], dp[i][j] + 1);
				}
				else
				{
					dp[i + 1][m] = Math.Max(dp[i + 1][m], dp[i][j]);
				}
			}
		}

		return dp[n].Max();
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
