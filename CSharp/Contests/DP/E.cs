using System;
using System.Linq;

class E
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var ps = new int[h[0]].Select(_ => read()).ToArray();

		var V = h[0] * 1000;
		var dp = Enumerable.Repeat(int.MaxValue, V + 1).ToArray();
		dp[0] = 0;
		foreach (var p in ps)
			for (int w, i = V; i >= 0; i--)
				if (dp[i] != int.MaxValue && i + p[1] <= V && (w = dp[i] + p[0]) <= h[1])
					dp[i + p[1]] = Math.Min(dp[i + p[1]], w);
		Console.WriteLine(Enumerable.Range(0, V + 1).Reverse().First(x => dp[x] != int.MaxValue));
	}
}
