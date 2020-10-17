using System;
using System.Linq;

class B
{
	static long[] Read() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var h = Read();
		int n = (int)h[0], k = (int)h[1];
		long l = h[2], r = h[3];
		var a = Read();

		var m1 = Math.Min(20, n);

		var dp1 = new long[1 << m1];
		for (int x = 0; x < 1 << m1; x++)
			for (int i = 0; i < m1; i++)
				if (dp1[x | (1 << i)] == 0) dp1[x | (1 << i)] = dp1[x] + a[i];

		if (n <= 20)
		{
			Console.WriteLine(Enumerable.Range(0, 1 << n).Count(x => FlagCount(x) == k && l <= dp1[x] && dp1[x] <= r));
			return;
		}

		var m2 = n - 20;
		var dp2 = new long[1 << m2];
		for (int x = 0; x < 1 << m2; x++)
			for (int i = 0; i < m2; i++)
				if (dp2[x | (1 << i)] == 0) dp2[x | (1 << i)] = dp2[x] + a[20 + i];

		var gs1 = Enumerable.Range(0, 1 << 20).ToLookup(FlagCount, x => dp1[x]);
		var gs2 = Enumerable.Range(0, 1 << m2).ToLookup(FlagCount, x => dp2[x]);

		var c = 0L;
		for (int i = 0; i <= 20; i++)
		{
			if (!gs2[k - i].Any()) continue;

			var s1 = gs1[i].ToArray();
			var s2 = gs2[k - i].ToArray();
			Array.Sort(s2);

			foreach (var s in s1)
			{
				var li = First(0, s2.Length, si => s + s2[si] >= l);
				var ri = First(0, s2.Length, si => s + s2[si] > r);
				c += ri - li;
			}
		}
		Console.WriteLine(c);
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
