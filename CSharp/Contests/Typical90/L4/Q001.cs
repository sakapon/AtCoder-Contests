using System;
using System.Linq;

class Q001
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, l) = Read2();
		var k = int.Parse(Console.ReadLine());
		var a = Read().Prepend(0).Append(l).ToArray();

		var d = Enumerable.Range(0, a.Length - 1).Select(i => a[i + 1] - a[i]).ToArray();

		return Max(0, l, s =>
		{
			var (c, t) = (0, 0);
			foreach (var v in d)
			{
				if ((t += v) >= s)
				{
					c++;
					t = 0;
				}
			}
			return c >= k + 1;
		});
	}

	static int Max(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
	}
}
