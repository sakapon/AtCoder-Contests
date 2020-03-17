using System;
using System.Linq;

class D2
{
	static void Main()
	{
		var h = Console.ReadLine().Split().Select(int.Parse).ToArray();
		int n = h[0], m = h[1];
		var a = Console.ReadLine().Split().Select(double.Parse).ToArray();

		var v = First(0, 1 << 30, x =>
		{
			var c = 0;
			foreach (var _ in a)
			{
				var ai = _;
				while (ai > x) { ai /= 2; c++; }
			}
			return c <= m;
		});

		for (int i = 0; i < n; i++)
			while (a[i] > v) { a[i] /= 2; m--; }

		Array.Sort(a);
		for (int i = Math.Max(0, n - m); i < n; i++)
			a[i] /= 2;
		Console.WriteLine(a.Sum(x => (long)x));
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
