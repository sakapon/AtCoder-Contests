using System;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, t) = Read2();
		var a = Read();

		var n2 = n / 2;

		var s1 = CreateSums(a.Take(n2).ToArray(), t);
		var s2 = CreateSums(a.Skip(n2).ToArray(), t);

		Array.Sort(s2);
		Array.Reverse(s2);

		var r = 0;
		for (int i = 0; i < s1.Length; i++)
		{
			var j = First(0, s2.Length, x => s1[i] + s2[x] <= t);
			r = Math.Max(r, s1[i] + s2[j]);
		}
		Console.WriteLine(r);
	}

	static int[] CreateSums(int[] a, int t)
	{
		var n = a.Length;
		var dp = NewArray1(1 << n, -1);
		dp[0] = 0;
		for (int x = 0; x < 1 << n; x++)
		{
			if (dp[x] == -1) continue;
			for (int i = 0; i < n; i++)
				if (dp[x | (1 << i)] == -1 && dp[x] + a[i] <= t)
					dp[x | (1 << i)] = dp[x] + a[i];
		}
		return Array.FindAll(dp, v => v != -1);
	}

	static T[] NewArray1<T>(int n, T v = default) => Array.ConvertAll(new bool[n], _ => v);

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
