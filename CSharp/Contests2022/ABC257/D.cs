using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long x, long y, long p) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read3L());

		var d = new long[n, n];
		for (int i = 0; i < n; i++)
		{
			var (xi, yi, _) = ps[i];
			for (int j = 0; j < n; j++)
			{
				var (xj, yj, _) = ps[j];
				d[i, j] = Math.Abs(xi - xj) + Math.Abs(yi - yj);
			}
		}

		return First(0, 1L << 32, s =>
		{
			var map = Array.ConvertAll(new bool[n], _ => new List<int>());
			for (int i = 0; i < n; i++)
			{
				var p = ps[i].p;
				for (int j = 0; j < n; j++)
				{
					if (p * s >= d[i, j]) map[i].Add(j);
				}
			}

			for (int sv = 0; sv < n; sv++)
			{
				var r = Dfs(n, v => map[v].ToArray(), sv);
				if (Array.TrueForAll(r, b => b)) return true;
			}
			return false;
		});
	}

	static long First(long l, long r, Func<long, bool> f)
	{
		long m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}

	public static bool[] Dfs(int n, Func<int, int[]> nexts, int sv, int ev = -1)
	{
		var u = new bool[n];
		var q = new Stack<int>();
		u[sv] = true;
		q.Push(sv);

		while (q.Count > 0)
		{
			var v = q.Pop();

			foreach (var nv in nexts(v))
			{
				if (u[nv]) continue;
				u[nv] = true;
				if (nv == ev) return u;
				q.Push(nv);
			}
		}
		return u;
	}
}
