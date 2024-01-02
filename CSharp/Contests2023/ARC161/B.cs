using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var t = int.Parse(Console.ReadLine());
		var ns = Array.ConvertAll(new bool[t], _ => long.Parse(Console.ReadLine()));

		var l = new List<long>();

		for (int i = 59; i >= 0; i--)
		{
			for (int j = i - 1; j >= 0; j--)
			{
				for (int k = j - 1; k >= 0; k--)
				{
					l.Add((1L << i) | (1L << j) | (1L << k));
				}
			}
		}
		l.Add(-1);
		l.Reverse();

		return string.Join("\n", ns.Select(n => l[Last(-1, l.Count - 1, x => l[x] <= n)]));
	}

	static int Last(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
	}
}
