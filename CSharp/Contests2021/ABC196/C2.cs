using System;

class C2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = long.Parse(Console.ReadLine());
		return Last(0, 1 << 20, i => long.Parse($"{i}{i}") <= n);
	}

	static int Last(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
	}
}
