using System;
using System.Linq;

class E
{
	static void Main()
	{
		Func<long[]> read = () => Console.ReadLine().Split().Select(long.Parse).ToArray();
		var h = read();
		long n = h[0], m = h[1];
		var a = read().OrderBy(x => -x).ToArray();

		var s = new long[n + 1];
		for (int i = 0; i < n; i++) s[i + 1] = s[i] + a[i];

		var a_a = Last(0, 2 * a[0], x =>
		{
			var c = 0L;
			for (int i = 0; i < n; i++)
			{
				var sub = x - a[i];
				c += 1 + Last(-1, n - 1, j => a[j] >= sub);
				if (c >= m) return true;
			}
			return false;
		});

		{
			long r = 0, c = 0;
			for (int i = 0; i < n; i++)
			{
				var sub = a_a - a[i];
				var count = 1 + Last(-1, n - 1, j => a[j] > sub);
				r += s[count] + a[i] * count;
				c += count;
			}
			r += (m - c) * a_a;
			Console.WriteLine(r);
		}
	}

	static long Last(long l, long r, Func<long, bool> f)
	{
		long m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
	}
}
