using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	const int max = 1 << 30;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();
		var a = Read();

		if (n == k) return $"0 {k}";

		Array.Sort(a);
		Array.Reverse(a);

		var next = new int[n + 1];
		foreach (var (i, j) in TwoPointers(n + 1, n + 1, (i, j) => j == n || a[i] >= a[j] * 2))
		{
			next[i] = j;
		}

		var dp = NewArray2(n + 1, 31, max);
		dp[0][0] = 0;

		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < 30; j++)
			{
				if (dp[i][j] == max) continue;

				dp[i + 1][j] = Math.Min(dp[i + 1][j], dp[i][j] + 1);
				dp[next[i]][j + 1] = Math.Min(dp[next[i]][j + 1], dp[i][j]);
			}
		}

		var mt = Enumerable.Range(0, 31).First(j => dp[n][j] <= k);
		return $"{mt} {dp[n][mt]}";
	}

	static IEnumerable<(int i, int j)> TwoPointers(int n1, int n2, Func<int, int, bool> predicate)
	{
		for (int i = 0, j = 0; i < n1 && j < n2; ++i)
			for (; j < n2; ++j)
				if (predicate(i, j)) { yield return (i, j); break; }
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
