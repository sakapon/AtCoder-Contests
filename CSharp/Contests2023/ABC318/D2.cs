using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Collections;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n - 1], _ => Read());

		var map = new long[n, n];
		for (int i = 0; i < n - 1; i++)
			for (int j = 0; j < n - 1 - i; j++)
				map[i, j + i + 1] = ps[i][j];

		var dp = new long[1 << n];

		for (uint x = 0; x < 1U << n; x++)
		{
			// i: 未使用の頂点のうち最初のもの
			var i = (~x).Min(n);
			for (int j = i + 1; j < n; j++)
			{
				if (x.Contains(j)) continue;
				var nx = x.Add(i).Add(j);
				Chmax(ref dp[nx], dp[x] + map[i, j]);
			}

			// n が奇数のとき、1 つの頂点は使用されません。
			if (n % 2 == 1)
			{
				i = (~x.Add(i)).Min(n);
				for (int j = i + 1; j < n; j++)
				{
					if (x.Contains(j)) continue;
					var nx = x.Add(i).Add(j);
					Chmax(ref dp[nx], dp[x] + map[i, j]);
				}
			}
		}

		if (n % 2 == 0)
		{
			return dp[^1];
		}
		else
		{
			var f = (1U << n) - 1;
			return f.NextSubsets(n).Max(x => dp[x]);
		}
	}

	public static long Chmax(ref long x, long v) => x < v ? x = v : x;
}
