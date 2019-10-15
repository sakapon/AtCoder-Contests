using System;
using System.Linq;

class D
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var ps = new int[h[0]].Select(_ => read()).ToArray();

		var dp = Enumerable.Repeat(-1L, h[1] + 1).ToArray();
		dp[0] = 0;
		foreach (var p in ps)
			for (int i = h[1]; i >= 0; i--)
				if (dp[i] != -1 && i + p[0] <= h[1])
					dp[i + p[0]] = Math.Max(dp[i + p[0]], dp[i] + p[1]);
		Console.WriteLine(dp.Max());
	}
}
