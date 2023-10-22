using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Collections;

class DB
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

		for (BitSet32 x = 0; x.Value < 1U << n; x++)
		{
			for (int i = 0; i < n; i++)
			{
				if (x[i]) continue;
				for (int j = i + 1; j < n; j++)
				{
					if (x[j]) continue;
					var nx = x.Add(i).Add(j);
					Chmax(ref dp[nx.Value], dp[x.Value] + map[i, j]);
				}
			}
		}

		if (n % 2 == 0)
		{
			return dp[^1];
		}
		else
		{
			BitSet32 f = (uint)dp.Length - 1;
			return Enumerable.Range(0, n).Max(i => dp[f.Remove(i).Value]);
		}
	}

	public static long Chmax(ref long x, long v) => x < v ? x = v : x;
}
