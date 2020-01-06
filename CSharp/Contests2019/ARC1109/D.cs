using System;
using System.Linq;

class D
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var qs = new int[h[1]].Select(_ => read()).OrderBy(x => x[1]).ThenBy(x => x[0]).ThenBy(x => x[2]).ToArray();

		var dp = Enumerable.Repeat(-1L, h[0] + 1).ToArray();
		dp[1] = 0;

		foreach (var q in qs)
		{
			if (dp[q[0]] == -1) continue;
			var d = dp[q[0]] + q[2];

			for (int i = q[1]; i > 0; i--)
			{
				if (dp[i] != -1 && dp[i] <= d) break;
				dp[i] = d;
			}
		}
		Console.WriteLine(dp[h[0]]);
	}
}
