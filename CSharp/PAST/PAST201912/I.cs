using System;
using System.Linq;

class I
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		var n = h[0];
		var p2 = Enumerable.Range(0, n + 1).Select(i => (int)Math.Pow(2, i)).ToArray();
		var ps = new int[h[1]].Select(_ => Console.ReadLine().Split()).Select(s => new { x = s[0].Select((c, i) => c == 'Y' ? p2[i] : 0).Sum(), c = int.Parse(s[1]) }).ToArray();

		var dp = Enumerable.Repeat(long.MaxValue, p2[n]).ToArray();
		dp[0] = 0;
		for (int i = 0, j; i < p2[n]; i++)
		{
			if (dp[i] == long.MaxValue) continue;
			foreach (var p in ps)
				if ((j = i | p.x) > i) dp[j] = Math.Min(dp[j], dp[i] + p.c);
		}
		var m = dp[p2[n] - 1];
		Console.WriteLine(m == long.MaxValue ? -1 : m);
	}
}
