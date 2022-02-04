using System;
using System.Collections.Generic;
using System.Linq;
using Lis = System.ValueTuple<int, int, int>;

class F2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();

		// 0 <= v < m
		var d = new Dictionary<Lis, long>();
		var nd = new Dictionary<Lis, long>();
		d[(max, max, max)] = 1;

		for (int i = 0; i < n; i++)
		{
			nd.Clear();

			foreach (var (j, v) in d)
			{
				for (int k = 0; k < m; k++)
				{
					var nj = NextLis(j, k);
					if (nj.Item1 == -1) continue;

					if (!nd.ContainsKey(nj)) nd[nj] = 0;
					nd[nj] += v;
					nd[nj] %= M;
				}
			}

			(d, nd) = (nd, d);
		}

		Lis NextLis(Lis l, int k)
		{
			var (a, b, c) = l;
			if (k <= a) return (k, b, c);
			if (k <= b) return (a, k, c);
			if (k <= c) return (a, b, k);
			return (-1, -1, -1);
		}

		return d.Where(p => p.Key.Item3 != max).Sum(p => p.Value) % M;
	}

	const long M = 998244353;
	const int max = 1 << 30;
}
