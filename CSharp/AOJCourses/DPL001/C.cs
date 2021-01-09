using System;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0], w = h[1];
		var ps = Array.ConvertAll(new int[n], _ => Read());

		var dp = NewArray1(w + 1000, min);
		dp[0] = 0;

		foreach (var p in ps)
			for (int i = 0; i < w; i++)
			{
				if (dp[i] == min) continue;
				dp[i + p[1]] = Math.Max(dp[i + p[1]], dp[i] + p[0]);
			}
		Console.WriteLine(dp.Take(w + 1).Max());
	}

	const int min = -1 << 30;

	static T[] NewArray1<T>(int n, T v = default(T))
	{
		var a = new T[n];
		for (int i = 0; i < n; ++i) a[i] = v;
		return a;
	}
}
