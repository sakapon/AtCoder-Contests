using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int x, int y) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		ps = ps.OrderBy(p => p.x).ToArray();

		return First(0, 1 << 30, d =>
		{
			var q = new Queue<(int x, int y)>(ps);
			var min = int.MaxValue;
			var max = int.MinValue;

			foreach (var (x, y) in ps)
			{
				while (q.Count > 0 && q.Peek().x < x - d)
				{
					var qy = q.Dequeue().y;
					min = Math.Min(min, qy);
					max = Math.Max(max, qy);
				}

				if (min < y - d) return false;
				if (max > y + d) return false;
			}
			return true;
		});
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
