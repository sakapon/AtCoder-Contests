using System;
using System.Collections.Generic;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var s = CumSumL(a);
		var imap = GroupIndexes(s);

		var dp = new long[n + 1];
		dp[1] = 1;

		for (int i = 2; i <= n; i++)
		{
			var l = imap[s[i - 1]];
			var li = Last(-1, l.Count - 1, x => l[x] < i - 1);

			var dup = li < 0 ? 0 : dp[l[li]];
			dp[i] = (dp[i - 1] * 2 - dup + M) % M;
		}

		return dp[n];
	}

	const long M = 998244353;

	public static long[] CumSumL(int[] a)
	{
		var s = new long[a.Length + 1];
		for (int i = 0; i < a.Length; ++i) s[i + 1] = s[i] + a[i];
		return s;
	}

	public static Dictionary<T, List<int>> GroupIndexes<T>(T[] a)
	{
		var d = new Dictionary<T, List<int>>();
		for (int i = 0; i < a.Length; ++i)
			if (d.ContainsKey(a[i])) d[a[i]].Add(i);
			else d[a[i]] = new List<int> { i };
		return d;
	}

	static int Last(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
	}
}
