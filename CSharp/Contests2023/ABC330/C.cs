using System;

class C
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var d = long.Parse(Console.ReadLine());

		long f(long x, long y) => x * x + y * y - d;

		var r = 1L << 60;

		for (int x = 1; x < 1500000; x++)
		{
			var y1 = First(0, 1500000, y => f(x, y) >= 0);
			r = Math.Min(r, f(x, y1));
			if (y1 > 0) r = Math.Min(r, -f(x, y1 - 1));
		}
		return r;
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
