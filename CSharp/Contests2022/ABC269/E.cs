using System;

class E
{
	static int Query(int a, int b, int c, int d)
	{
		Console.WriteLine($"? {a} {b} {c} {d}");
		return int.Parse(Console.ReadLine());
	}
	static void Main() => Console.WriteLine($"! {Solve()}");
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());

		var ri = First(1, n, x => Query(1, x, 1, n) != x);
		var rj = First(1, n, x => Query(1, n, 1, x) != x);
		return $"{ri} {rj}";
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
