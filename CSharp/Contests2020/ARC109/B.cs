using System;

class B
{
	static void Main()
	{
		var n = long.Parse(Console.ReadLine());

		var k = Last(0, int.MaxValue, x => (long)x * (x + 1) - 2 * (n + 1) <= 0);
		Console.WriteLine(n + 1 - k);
	}

	static int Last(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
	}
}
