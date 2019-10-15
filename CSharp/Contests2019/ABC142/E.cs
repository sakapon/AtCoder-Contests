using System;
using System.Linq;

class E
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var n = h[0];
		var p2 = Enumerable.Range(0, n + 1).Select(i => (int)Math.Pow(2, i)).ToArray();
		var ks = new int[h[1]].Select(_ => new { a = read()[0], c = read().Select(x => p2[x - 1]).Aggregate((x, y) => x | y) }).ToArray();

		var dp = Enumerable.Repeat(int.MaxValue, p2[n]).ToArray();
		dp[0] = 0;
		for (int i = 0, j; i < p2[n]; i++)
		{
			if (dp[i] == int.MaxValue) continue;
			foreach (var k in ks) if ((j = i | k.c) != i) dp[j] = Math.Min(dp[j], dp[i] + k.a);
		}
		var m = dp[p2[n] - 1];
		Console.WriteLine(m == int.MaxValue ? -1 : m);
	}
}
