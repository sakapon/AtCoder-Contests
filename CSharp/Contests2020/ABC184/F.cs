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
		var dp = new int[1 << n];
		for (int x = 1; x < 1 << n; x++)
		{
			for (int i = 0; i < n; i++)
			{
				if ((x & (1 << i)) != 0)
				{
					var px = x ^ (1 << i);
					var v = dp[px] + a[i];
					dp[x] = dp[px] == -1 || v > t ? -1 : v;
					break;
				}
			}
		}
		return Array.FindAll(dp, v => v != -1);
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
