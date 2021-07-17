using System;
using System.Collections.Generic;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (n, p) = Read2();
		var a = Read();

		Array.Sort(a);
		Array.Reverse(a);

		return Dfs(0, p, 1 << 30);

		bool Dfs(int i, long q, long pq)
		{
			if (q == 1) return true;
			if (i == n) return false;

			var ds = Divisors(q);
			Array.Reverse(ds);

			foreach (var d in ds)
			{
				if (d > pq || d == 1) continue;
				if (d > a[i]) continue;

				if (Dfs(i + 1, q / d, d)) return true;
			}
			return false;
		}
	}

	static long[] Divisors(long n)
	{
		var r = new List<long>();
		for (long x = 1; x * x <= n; ++x) if (n % x == 0) r.Add(x);
		var i = r.Count - 1;
		if (r[i] * r[i] == n) --i;
		for (; i >= 0; --i) r.Add(n / r[i]);
		return r.ToArray();
	}
}
