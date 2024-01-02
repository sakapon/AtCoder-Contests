using System;
using System.Linq;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		var s = new int[n];
		s[0] = 0;

		for (int i = 1; i < n; i++)
		{
			s[i] = s[i - 1];
			if (i % 2 == 0) s[i] += a[i] - a[i - 1];
		}

		var r = qs.Select(q =>
		{
			var (l, r) = q;
			var li = Last(-1, n - 1, x => a[x] <= l);
			var ri = Last(-1, n - 1, x => a[x] <= r);

			var ls = s[li];
			if (li % 2 == 1) ls += l - a[li];
			var rs = s[ri];
			if (ri % 2 == 1) rs += r - a[ri];

			return rs - ls;
		});

		return string.Join("\n", r);
	}

	static int Last(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
	}
}
