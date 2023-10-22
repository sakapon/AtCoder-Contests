using System;
using System.Linq;

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
			for (int i = 0; i < n; i++)
			{
				if (x.Contains(i)) continue;
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
			return Enumerable.Range(0, n).Max(i => dp[f.Remove(i)]);
		}
	}

	public static long Chmax(ref long x, long v) => x < v ? x = v : x;
}

public static class BitSetEx
{
	public static bool Contains(this uint x, int i) => (x & (1U << i)) != 0;
	public static uint Add(this uint x, int i) => x | (1U << i);
	public static uint Remove(this uint x, int i) => x & ~(1U << i);
	public static string ToBitString(this uint x) => Convert.ToString(x, 2);
}
