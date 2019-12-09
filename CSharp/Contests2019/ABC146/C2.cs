using System;

class C2
{
	static void Main()
	{
		var h = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
		Console.WriteLine(SearchLast(n => h[0] * n + h[1] * n.ToString().Length <= h[2], 0, 1000000000));
	}

	static int SearchLast(Func<int, bool> f, int l, int r)
	{
		int m;
		while (l < r) if (f(m = (l + r + 1) / 2)) l = m; else r = m - 1;
		return r;
	}
}
