using System;

class D
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Array.ConvertAll(new int[n], _ => int.Parse(Console.ReadLine()));

		var dp = NewArray1(n + 1, max);
		for (int i = 0; i < n; i++)
			dp[First(0, n, x => dp[x] >= a[i])] = a[i];
		Console.WriteLine(Array.IndexOf(dp, max));
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}

	const int max = 1 << 30;

	static T[] NewArray1<T>(int n, T v = default(T))
	{
		var a = new T[n];
		for (int i = 0; i < n; ++i) a[i] = v;
		return a;
	}
}
