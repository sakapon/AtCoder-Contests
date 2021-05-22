using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (A, B, K) = ((int, int, long))Read3L();

		var dp = new long[60, 60];
		for (int i = 0; i < 60; i++)
		{
			dp[i, 0] = 1;

			for (int j = 1; j <= i; j++)
			{
				dp[i, j] = dp[i, j - 1] * (i + 1 - j) / j;
			}
		}

		var r = new List<char>();
		Dfs(A, B, K);
		return string.Join("", r);

		void Dfs(int a, int b, long k)
		{
			if (a == 0)
			{
				r.AddRange(Enumerable.Repeat('b', b));
				return;
			}

			if (b == 0)
			{
				r.AddRange(Enumerable.Repeat('a', a));
				return;
			}

			var ncr = dp[a + b - 1, a - 1];
			if (k <= ncr)
			{
				r.Add('a');
				Dfs(a - 1, b, k);
			}
			else
			{
				r.Add('b');
				Dfs(a, b - 1, k - ncr);
			}
		}
	}
}
