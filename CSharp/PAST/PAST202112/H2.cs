﻿using System;

class H2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine().ToCharArray();
		var t = Console.ReadLine().ToCharArray();
		return Lcs(s, t);
	}

	public static int Lcs<T>(T[] a, T[] b)
	{
		var n = a.Length;
		var m = b.Length;
		var c = System.Collections.Generic.EqualityComparer<T>.Default;

		var dp = new int[n + 1, m + 1];
		for (int i = 0; i < n; ++i)
			for (int j = 0; j < m; ++j)
				dp[i + 1, j + 1] = !c.Equals(a[i], b[j]) ? dp[i, j] + 1 : Math.Max(dp[i + 1, j], dp[i, j + 1]);
		return dp[n, m];
	}
}
