using System;
using System.Collections.Generic;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (a, n) = Read2();

		const int max = 999999;
		var d = new int[max + 1];
		Array.Fill(d, -1);
		var q = new Queue<long>();

		d[1] = 0;
		q.Enqueue(1);

		while (q.TryDequeue(out var x))
		{
			foreach (var nx in Nexts(x))
			{
				if (d[nx] != -1) continue;
				d[nx] = d[x] + 1;
				q.Enqueue(nx);
			}
		}

		return d[n];

		long[] Nexts(long x)
		{
			var r = new List<long>();

			var nx = x * a;
			if (nx <= max) r.Add(nx);

			if (x >= 10 && x % 10 != 0)
			{
				var s = x.ToString();
				s = s[^1] + s[..^1];
				nx = long.Parse(s);
				if (nx <= max) r.Add(nx);
			}
			return r.ToArray();
		}
	}
}
