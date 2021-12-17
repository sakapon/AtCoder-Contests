using System;
using System.Linq;

class D2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		var hi = First(0, n, x => s.Take(x).Count(c => c == 'W') >= s.Skip(x).Count(c => c == 'R'));
		return s.Take(hi).Count(c => c == 'W');
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
