using System;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		var n = h[0];
		var c = Read();

		var dp = NewArray1(n + 10000, max);
		dp[0] = 0;

		foreach (var x in c)
			for (int i = 0; i < n; i++)
			{
				if (dp[i] == max) continue;
				dp[i + x] = Math.Min(dp[i + x], dp[i] + 1);
			}
		Console.WriteLine(dp[n]);
	}

	const int max = 1 << 30;

	static T[] NewArray1<T>(int n, T v = default(T))
	{
		var a = new T[n];
		for (int i = 0; i < n; ++i) a[i] = v;
		return a;
	}
}
