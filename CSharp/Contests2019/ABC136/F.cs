using System;
using System.Linq;

class F
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var t = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).OrderBy(p => p[0]).Select(p => p[1]).ToArray();
		var yx = Enumerable.Range(0, n).ToArray();
		var xy = (int[])yx.Clone();
		Array.Sort(t, yx);
		yx.CopyTo(t, 0);
		Array.Sort(t, xy);

		int ss = 64, sc = n / ss + 1;
		var s = new int[sc, sc];
		for (int i = 0; i < n; i++)
			s[i / ss, xy[i] / ss]++;
		for (int i = 0; i < sc; i++)
			for (int j = 1; j < sc; j++)
				s[i, j] += s[i, j - 1];
		for (int j = 0; j < sc; j++)
			for (int i = 1; i < sc; i++)
				s[i, j] += s[i - 1, j];

		var c = MInt(n * MPow(2, n) - n);
		for (int i = 0; i < n; i++)
		{
			var v = xy[i];

			var li = i >= ss && v >= ss ? s[i / ss - 1, v / ss - 1] : 0;
			var ix = i - i % ss;
			for (int i2 = ix; i2 < i; i2++)
				if (xy[i2] < v) li++;
			for (int j2 = v - v % ss; j2 < v; j2++)
				if (yx[j2] < ix) li++;

			c -= MPow(2, i) + MPow(2, n - 1 - i) + MPow(2, v) + MPow(2, n - 1 - v);
			c += MPow(2, li) + MPow(2, i - li) + MPow(2, v - li) + MPow(2, n - 1 + li - i - v);
			c = MInt(c);
		}
		Console.WriteLine(c);
	}

	const int M = 998244353;
	static long MPow(long b, long i)
	{
		for (var r = 1L; ; b = b * b % M)
		{
			if (i % 2 > 0) r = r * b % M;
			if ((i /= 2) < 1) return r;
		}
	}
	static long MInt(long x) => (x %= M) < 0 ? x + M : x;
}
