class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = Read();

		var sum = 0L;
		var raq = new StaticRAQ1(2 * n);

		for (int i = 1; i < m; i++)
		{
			var d1 = Math.Abs(a[i] - a[i - 1]);
			var d2 = n - d1;
			sum += Math.Min(d1, d2);

			if (d1 == d2) continue;

			if (d1 < d2)
			{
				raq.Add(Math.Min(a[i], a[i - 1]), Math.Max(a[i], a[i - 1]), d2 - d1);
			}
			else
			{
				raq.Add(Math.Max(a[i], a[i - 1]), Math.Min(a[i], a[i - 1]) + n, d1 - d2);
			}
		}

		var r = raq.GetSum0();
		return sum + Enumerable.Range(0, n).Min(i => r[i] + r[i + n]);
	}
}

public class StaticRAQ1
{
	int n;
	long[] d;
	public StaticRAQ1(int _n) { n = _n; d = new long[n]; }

	// O(1)
	// [l, r)
	// 範囲外のインデックスも可。
	public void Add(int l, int r, long v)
	{
		if (r < 0 || n <= l) return;
		d[Math.Max(0, l)] += v;
		if (r < n) d[r] -= v;
	}

	// O(n)
	public long[] GetSum()
	{
		var a = new long[n];
		a[0] = d[0];
		for (int i = 1; i < n; ++i) a[i] = a[i - 1] + d[i];
		return a;
	}

	// O(n)
	// d をそのまま使います。
	public long[] GetSum0()
	{
		for (int i = 1; i < n; ++i) d[i] += d[i - 1];
		return d;
	}
}
