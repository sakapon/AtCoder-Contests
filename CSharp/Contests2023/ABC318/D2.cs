using System;
using System.Collections.Generic;
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
			return f.SubSets(n).Max(x => dp[x]);
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
	public static uint ToUInt32(this string s) => Convert.ToUInt32(s, 2);

	public static int Min(this uint x, int n)
	{
		var i = 0;
		while (i < n && (x & (1U << i)) == 0) ++i;
		return i;
	}

	public static int Max(this uint x, int n)
	{
		var i = n - 1;
		while (i >= 0 && (x & (1U << i)) == 0) --i;
		return i;
	}

	public static IEnumerable<uint> SuperSets(this uint x, int n)
	{
		for (var f = 1U; f < (1U << n); f <<= 1)
			if ((x & f) == 0) yield return x | f;
	}

	public static IEnumerable<uint> SubSets(this uint x, int n)
	{
		for (var f = 1U; f < (1U << n); f <<= 1)
			if ((x & f) != 0) yield return x & ~f;
	}
}
