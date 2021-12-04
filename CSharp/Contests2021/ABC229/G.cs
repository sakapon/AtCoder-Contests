using System;
using System.Linq;

class G
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine();
		var n = s.Length;
		var k = long.Parse(Console.ReadLine());

		var indexes = Enumerable.Range(0, n).Where(i => s[i] == 'Y').ToArray();
		var m = indexes.Length;
		var cs = CumSumL(indexes);

		return Last(0, m, x =>
		{
			var xl = x / 2;
			var xr = x - xl;

			for (int i = 0; i < m - x + 1; i++)
			{
				var c = 0L;
				c += cs[i + x] - cs[i + xl];
				c -= (indexes[i + xl] * 2 + xr - 1) * (long)xr / 2;
				c += (indexes[i + xl] * 2 - xl - 1) * (long)xl / 2;
				c -= cs[i + xl] - cs[i];

				if (c <= k) return true;
			}
			return false;
		});
	}

	public static long[] CumSumL(int[] a)
	{
		var s = new long[a.Length + 1];
		for (int i = 0; i < a.Length; ++i) s[i + 1] = s[i] + a[i];
		return s;
	}

	static int Last(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
	}
}
