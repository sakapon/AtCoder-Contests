using System;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0], w = h[1];
		var ps = Array.ConvertAll(new int[n], _ => Read());

		var dp = NewArray1(n * 100 + 1, max);
		dp[0] = 0;

		foreach (var p in ps)
			for (int v = dp.Length - 1; v >= 0; v--)
			{
				if (dp[v] >= w) continue;
				dp[v + p[0]] = Math.Min(dp[v + p[0]], dp[v] + p[1]);
			}
		Console.WriteLine(Enumerable.Range(0, dp.Length).Last(v => dp[v] <= w));
	}

	const int max = 1 << 30;

	static T[] NewArray1<T>(int n, T v = default(T))
	{
		var a = new T[n];
		for (int i = 0; i < n; ++i) a[i] = v;
		return a;
	}
}
