using System;
using System.Collections.Generic;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = ReadL();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var mapl = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
		var cost = new long[n + 1];

		foreach (var e in es)
		{
			mapl[e[0]].Add(e[1]);
			mapl[e[1]].Add(e[0]);
			cost[e[0]] += a[e[1] - 1];
			cost[e[1]] += a[e[0] - 1];
		}

		var map = Array.ConvertAll(mapl, l => l.ToArray());
		var u = new bool[n + 1];
		var q = new Queue<int>();

		return First(0, 1L << 50, x =>
		{
			var c = (long[])cost.Clone();
			Array.Clear(u, 0, u.Length);
			u[0] = true;
			q.Clear();

			for (int v = 1; v <= n; v++)
			{
				if (c[v] <= x)
				{
					q.Enqueue(v);
					u[v] = true;
				}
			}

			while (q.Count > 0)
			{
				var v = q.Dequeue();
				foreach (var nv in map[v])
				{
					if (u[nv]) continue;
					if ((c[nv] -= a[v - 1]) <= x)
					{
						q.Enqueue(nv);
						u[nv] = true;
					}
				}
			}
			return Array.TrueForAll(u, v => v);
		});
	}

	static long First(long l, long r, Func<long, bool> f)
	{
		long m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
