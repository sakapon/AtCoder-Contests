using System;
using System.Linq;

class D
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Array.ConvertAll(new int[n], _ => int.Parse(Console.ReadLine()));

		var dp = new int[n];
		for (int i = 0; i < n; i++)
			dp[i] = 1 << 30;

		for (int i = 0; i < n; i++)
			dp[First(0, n, x => dp[x] >= a[i])] = a[i];
		Console.WriteLine(dp.Count(x => x < 1 << 30));
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
