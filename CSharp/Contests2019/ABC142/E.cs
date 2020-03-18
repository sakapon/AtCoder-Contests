using System;
using System.Linq;

class E
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var n = h[0];
		var p2 = Enumerable.Range(0, n + 1).Select(i => 1 << i).ToArray();
		var k = new int[h[1]].Select(_ => new { a = read()[0], f = read().Sum(x => p2[x - 1]) }).ToArray();

		var M = int.MaxValue;
		var dp = Enumerable.Repeat(M, p2[n]).ToArray();
		dp[0] = 0;
		for (int f = 0, f2; f < p2[n]; f++)
		{
			if (dp[f] == M) continue;
			foreach (var x in k)
			{
				if ((f2 = f | x.f) == f) continue;
				dp[f2] = Math.Min(dp[f2], dp[f] + x.a);
			}
		}
		Console.WriteLine(dp.Last() < M ? dp.Last() : -1);
	}
}
