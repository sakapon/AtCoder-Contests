using System;
using System.Linq;

class R
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(long.Parse).ToArray();
		var n = (int)h[0]; var k = h[1];
		var a = Enumerable.Range(0, n).ToDictionary(i => i, i => Console.ReadLine().Split().Select((x, j) => new { x, j }).Where(_ => _.x == "1").Select(_ => _.j).ToArray());

		var dp = new long[n, k + 1];
		for (int i = 0; i < n; i++)
			dp[i, 0] = 1;
		for (int j = 1; j <= k; j++)
			for (int i = 0; i < n; i++)
				dp[i, j] = a[i].Sum(x => dp[x, j - 1]) % 1000000007;
		Console.WriteLine(Enumerable.Range(0, n).Sum(i => dp[i, k]));
	}
}
