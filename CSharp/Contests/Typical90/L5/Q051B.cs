using System;
using System.Collections.Generic;
using System.Linq;

class Q051B
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k, p) = ((int, int, long))Read3L();
		var a = ReadL();

		var n2 = n / 2;
		var g1 = GetAll(n2, a[..n2]);
		var g2 = GetAll(n - n2, a[n2..]);

		foreach (var g in g2)
			Array.Reverse(g);

		var r = 0L;

		for (int i = 0; i <= k && i <= n2; i++)
		{
			var j = k - i;
			if (j < 0 || n - n2 < j) continue;

			var b1 = g1[i];
			var b2 = g2[j];

			r += TwoPointers(b1.Length, b2.Length, (x, y) => b1[x] + b2[y] <= p).Sum(_ => (long)b2.Length - _.j);
		}

		return r;
	}

	static long[][] GetAll(int n, long[] a)
	{
		var r = new long[n + 1][];

		var pn = 1 << n;
		var dp = NewArray2(n + 1, pn, -1L);
		dp[0][0] = 0;

		for (int i = 0; i < n; i++)
		{
			var l = new List<long>();
			for (int x = 0; x < pn; x++)
			{
				if (dp[i][x] == -1) continue;

				l.Add(dp[i][x]);

				for (int j = 0; j < n; j++)
				{
					if ((x & (1 << j)) != 0) continue;

					var nx = x | (1 << j);
					if (dp[i + 1][nx] != -1) continue;
					dp[i + 1][nx] = dp[i][x] + a[j];
				}
			}

			r[i] = l.ToArray();
			Array.Sort(r[i]);
		}

		r[n] = new[] { dp[n][^1] };
		return r;
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));

	static IEnumerable<(int i, int j)> TwoPointers(int n1, int n2, Func<int, int, bool> predicate)
	{
		for (int i = 0, j = 0; i < n1 && j < n2; ++i)
			for (; j < n2; ++j)
				if (predicate(i, j)) { yield return (i, j); break; }
	}
}
