using System;

class Q053C
{
	static int Query(int i)
	{
		Console.WriteLine($"? {i + 1}");
		return int.Parse(Console.ReadLine());
	}
	static void Answer(int x) => Console.WriteLine($"! {x}");

	static void Main() => Array.ConvertAll(new bool[int.Parse(Console.ReadLine())], _ => Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());

		var a = Array.ConvertAll(new bool[n], _ => -1);

		int GetValue(int i)
		{
			if (a[i] == -1) a[i] = Query(i);
			return a[i];
		}

		var mi = Min(0, n - 1, x => GetValue(x) > GetValue(x + 1));
		Answer(GetValue(mi));
		return null;
	}

	static int Min(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
