using System;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0], w = h[1];
		var ps = Array.ConvertAll(new int[n], _ => Read());

		var dp = new int[w + 1000];
		foreach (var p in ps)
			for (int i = w - 1; i >= 0; i--)
				dp[i + p[1]] = Math.Max(dp[i + p[1]], dp[i] + p[0]);
		Console.WriteLine(dp[w]);
	}
}
