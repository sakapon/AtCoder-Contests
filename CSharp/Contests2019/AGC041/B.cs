using System;
using System.Linq;

class B
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		int n = h[0], m = h[1], v = h[2], p = h[3];
		var a = read().OrderBy(x => -x).ToArray();

		var s = new long[n + 1];
		for (int i = 0; i < n; i++) s[i + 1] = s[i] + a[i];

		Console.WriteLine(1 + Last(0, n - 1, i =>
		{
			long x = a[i] + m;
			long v2 = v - p - n + 1 + i;

			if (x < a[p - 1]) return false;
			if (v2 <= 0) return true;
			return s[i] - s[p - 1] + m * v2 <= x * (i - p + 1);
		}));
	}

	static int Last(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
	}
}
