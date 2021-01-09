using System;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0], t = h[1];
		var a = Read();

		var c = 20;
		var m1 = Math.Min(c, n);

		var dp1 = new long[1 << m1];
		for (int x = 0; x < 1 << m1; x++)
			for (int i = 0; i < m1; i++)
				if (dp1[x | (1 << i)] == 0 && dp1[x] + a[i] <= t)
					dp1[x | (1 << i)] = dp1[x] + a[i];

		if (n <= c)
		{
			Console.WriteLine(dp1.Max());
			return;
		}

		var m2 = n - c;
		var dp2 = new long[1 << m2];
		for (int x = 0; x < 1 << m2; x++)
			for (int i = 0; i < m2; i++)
				if (dp2[x | (1 << i)] == 0 && dp2[x] + a[c + i] <= t)
					dp2[x | (1 << i)] = dp2[x] + a[c + i];

		Array.Sort(dp2);
		Array.Reverse(dp2);
		var M = 0L;

		for (int i = 0; i < dp1.Length; i++)
		{
			var j = First(0, dp2.Length, x => dp1[i] + dp2[x] <= t);
			M = Math.Max(M, dp1[i] + dp2[j]);
		}
		Console.WriteLine(M);
	}

	static int FlagCount(int x)
	{
		var r = 0;
		for (; x != 0; x >>= 1) if ((x & 1) != 0) r++;
		return r;
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
