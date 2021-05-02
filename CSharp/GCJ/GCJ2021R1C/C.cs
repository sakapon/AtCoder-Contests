using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select((_, i) => $"Case #{i + 1}: {Solve()}")));
	static object Solve()
	{
		var h = Console.ReadLine().Split();
		if (h[0] == h[1]) return 0;

		var s = h[0].ConvertFrom(2);
		var e = h[1].ConvertFrom(2);

		var dp = Array.ConvertAll(new bool[1 << 16], _ => 1 << 30);
		dp[s] = 0;
		var fmax = (1 << 16) - 1;

		var fs = new int[1 << 16];
		for (int k = 16; k > 0; k--)
		{
			var f = (1 << k) - 1;

			for (int x = 0; x <= f; x++)
			{
				fs[x] = f;
			}
		}
		//fs[0] = 0;

		for (int k = 0; k < 1024; k++)
		{
			var nk = k + 1;

			for (int x = 0; x < 1 << 16; x++)
			{
				if (dp[x] != k) continue;

				var nx = x << 1;
				if (nx <= fmax)
					dp[nx] = Math.Min(dp[nx], nk);

				nx = ~x & fs[x];
				dp[nx] = Math.Min(dp[nx], nk);
			}

			if (dp[e] == nk) return nk;
		}

		return "IMPOSSIBLE";
	}
}

public static class PositionalNotation
{
	public static int[] Convert(this long x, int b)
	{
		var r = new List<int>();
		for (; x > 0; x /= b) r.Add((int)(x % b));
		return r.ToArray();
	}

	public static long ConvertFrom(this int[] a, int b)
	{
		var r = 0L;
		for (int i = a.Length - 1; i >= 0; --i) r = r * b + a[i];
		return r;
	}

	const string AN = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
	static readonly Dictionary<char, int> ANMap = AN.Select((c, i) => new { c, i }).ToDictionary(_ => _.c, _ => _.i);

	public static string ConvertAsString(this long x, int b)
	{
		if (x == 0) return "0";
		if (x < 0) return "-" + ConvertAsString(-x, b);
		var r = Convert(x, b);
		Array.Reverse(r);
		return new string(Array.ConvertAll(r, d => AN[d]));
	}

	public static long ConvertFrom(this string s, int b)
	{
		if (s.StartsWith('-')) return -ConvertFrom(s.Substring(1), b);
		var a = Array.ConvertAll(s.ToCharArray(), c => ANMap[c]);
		Array.Reverse(a);
		return ConvertFrom(a, b);
	}
}
