using System;
using System.Linq;

class F
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).OrderBy(p => p[0]).ToArray();

		var rs = ps.Select(p => p[1]).OrderBy(x => x).ToList();
		var ls = rs.Take(0).ToList();

		var c = MInt(n * MPow(2, n) - n);
		for (int i = 0; i < n; i++)
		{
			var v = ps[i][1];
			var ri = rs.BinarySearch(v);
			rs.RemoveAt(ri);
			var li = ~ls.BinarySearch(v);
			ls.Insert(li, v);

			c -= MPow(2, i) + MPow(2, n - 1 - i) + MPow(2, li + ri) + MPow(2, n - 1 - li - ri);
			c += MPow(2, li) + MPow(2, i - li) + MPow(2, ri) + MPow(2, n - 1 - i - ri);
			c = MInt(c);
		}
		Console.WriteLine(c);
	}

	const int M = 998244353;
	static long MPow(long b, long i)
	{
		for (var r = 1L; ; b = b * b % M)
		{
			if (i % 2 > 0) r = r * b % M;
			if ((i /= 2) < 1) return r;
		}
	}
	static long MInt(long x) => (x %= M) < 0 ? x + M : x;
}
