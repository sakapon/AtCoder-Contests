using System;
using System.Linq;

class B
{
	const int min = -1 << 30;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0], w = h[1];
		var ps = Array.ConvertAll(new int[n], _ => Read());

		var dp = new int[w + 1000];
		for (int i = 0; i < dp.Length; i++)
			dp[i] = min;
		dp[0] = 0;

		foreach (var p in ps)
			for (int i = w - 1; i >= 0; i--)
			{
				if (dp[i] == min) continue;
				dp[i + p[1]] = Math.Max(dp[i + p[1]], dp[i] + p[0]);
			}
		Console.WriteLine(dp.Take(w + 1).Max());
	}
}
