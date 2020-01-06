using System;
using System.Linq;

class E
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		int n = h[0], t = h[1];
		var ds = new int[n].Select(_ => read()).OrderBy(x => x[0]).ToArray();

		var M = 0;
		var dp = Enumerable.Repeat(-1, t).ToArray();
		dp[0] = 0;

		foreach (var d in ds)
			for (int i = t - 1; i >= 0; i--)
			{
				if (dp[i] == -1) continue;
				var i2 = i + d[0];
				var v = dp[i] + d[1];
				if (i2 < t) dp[i2] = Math.Max(dp[i2], v);
				M = Math.Max(M, v);
			}
		Console.WriteLine(M);
	}
}
