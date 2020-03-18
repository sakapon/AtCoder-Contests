using System;
using System.Linq;

class I
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		var n = h[0];
		var p2 = Enumerable.Range(0, n + 1).Select(i => 1 << i).ToArray();
		var a = new int[h[1]].Select(_ => Console.ReadLine().Split()).Select(x => new[] { x[0].Select((c, i) => c == 'Y' ? p2[i] : 0).Sum(), int.Parse(x[1]) }).ToArray();

		var M = long.MaxValue;
		var dp = Enumerable.Repeat(M, p2[n]).ToArray();
		dp[0] = 0;
		for (int f = 0, f2; f < p2[n]; f++)
		{
			if (dp[f] == M) continue;
			foreach (var x in a)
			{
				if ((f2 = f | x[0]) == f) continue;
				dp[f2] = Math.Min(dp[f2], dp[f] + x[1]);
			}
		}
		Console.WriteLine(dp.Last() < M ? dp.Last() : -1);
	}
}
