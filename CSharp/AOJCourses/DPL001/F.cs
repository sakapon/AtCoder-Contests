using System;
using System.Linq;

class F
{
	const int max = 1 << 30;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0], w = h[1];
		var ps = Array.ConvertAll(new int[n], _ => Read());

		var dp = new int[n * 100 + 1];
		for (int v = 0; v < dp.Length; v++)
			dp[v] = max;
		dp[0] = 0;

		foreach (var p in ps)
			for (int nw, v = dp.Length - 1; v >= 0; v--)
			{
				if (dp[v] == max || (nw = dp[v] + p[1]) > w) continue;
				dp[v + p[0]] = Math.Min(dp[v + p[0]], nw);
			}
		Console.WriteLine(Enumerable.Range(0, dp.Length).Last(v => dp[v] < max));
	}
}
