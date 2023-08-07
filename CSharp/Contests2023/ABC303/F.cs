using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, h) = Read2L();
		(long t, long d)[] ps = Array.ConvertAll(new bool[n], _ => Read2L());

		// t_i < t_j かつ d_i > d_j となるもののみを残します。
		var q1 = new Stack<(long t, long d)>();
		q1.Push((1 << 30, 0));
		foreach (var (t, d) in ps.OrderBy(p => -p.t).ThenBy(p => -p.d))
		{
			var (t0, d0) = q1.Peek();
			if (d <= d0) continue;
			q1.Push((t, d));
		}

		var q2 = new Stack<(long t, long d)>();
		q2.Push((0, 1 << 30));
		var ts = new List<long>();

		// t * d が単調増加となるように残します。
		foreach (var (t, d) in q1)
		{
			var (t0, d0) = q2.Peek();
			if (t * d <= t0 * d0) continue;
			q2.Push((t, d));
			ts.Add(t0 * d0 / d);
		}
		ts.Add(1L << 60);

		ps = q2.ToArray();
		Array.Reverse(ps);

		return First(0, 1L << 60, t => GetDamage(t) >= h);

		long GetDamage(long t)
		{
			var r = 0L;
			for (int i = 1; i < ps.Length; i++)
			{
				var end = false;

				var t0 = ts[i - 1];
				var (t1, d) = ps[i];
				var t2 = ts[i];
				var d1 = (t0 + 1) * d;

				if (t <= t1)
				{
					t1 = t;
					end = true;
				}
				var d2 = t1 * d;
				try
				{
					checked
					{
						r += (d1 + d2) * (t1 - t0) / 2;
					}
					if (end) return r;
				}
				catch (OverflowException)
				{
					return 1L << 60;
				}

				if (t <= t2)
				{
					t2 = t;
					end = true;
				}
				try
				{
					checked
					{
						r += d2 * (t2 - t1);
					}
					if (end) return r;
				}
				catch (OverflowException)
				{
					return 1L << 60;
				}
			}
			throw new InvalidOperationException();
		}
	}

	static long First(long l, long r, Func<long, bool> f)
	{
		long m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
