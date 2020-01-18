using System;
using System.Linq;

class B
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = new int[n]
			.Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray())
			.Select(x => new { l = x[0] - x[1], r = x[0] + x[1] })
			.OrderBy(x => x.l);

		var dp = Enumerable.Repeat(int.MaxValue, n + 1).ToArray();
		dp[0] = int.MinValue;
		foreach (var x in a)
		{
			var j = Last(0, n, i => dp[i] <= x.l) + 1;
			dp[j] = Math.Min(dp[j], x.r);
		}
		Console.WriteLine(Last(0, n, i => dp[i] < int.MaxValue));
	}

	static int Last(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
	}
}
