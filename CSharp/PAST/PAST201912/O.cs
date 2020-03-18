using System;
using System.Linq;

class O
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Enumerable.Range(0, n)
			.SelectMany(i => Console.ReadLine().Split().Select(int.Parse).Select(x => new { i, x }))
			.OrderBy(_ => _.x)
			.Select(_ => _.i)
			.ToList();
		a.Insert(0, 0);

		var dp = new double[6 * n + 1];
		dp[dp.Length - 1] = 1;
		var e = new double[n];
		var M = 0.0;

		for (int i = dp.Length - 1; i > 0; i--)
		{
			e[a[i]] += dp[i] / 6;
			M = Math.Max(M, e[a[i]]);
			dp[i - 1] = M + 1;
		}
		Console.WriteLine(dp[0]);
	}
}
