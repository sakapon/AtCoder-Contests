using System;

class C
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();
		var l = s.Length;
		var n = long.Parse(s);
		return Comma(n, l);
	}

	static long Comma(long n, int l)
	{
		if (l < 4) return 0;
		if (l < 7) return n - 999;
		if (l < 10) return 2 * n - 999999 - 999;
		if (l < 13) return 3 * n - 999999999 - 999999 - 999;
		if (l < 16) return 4 * n - 999999999999 - 999999999 - 999999 - 999;
		return 5 * n - 999999999999999 - 999999999999 - 999999999 - 999999 - 999;
	}
}
