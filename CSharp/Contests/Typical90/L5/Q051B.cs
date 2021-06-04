using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class Q051B
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long, long) Read3L() { var a = ReadL(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k, p) = ((int, int, long))Read3L();
		var a = ReadL();

		var n2 = n / 2;
		var g1 = GetAll(n2, a[..n2]);
		var g2 = GetAll(n - n2, a[n2..]);

		foreach (var g in g2)
			Array.Reverse(g);

		var r = 0L;

		for (int i = 0; i <= k && i <= n2; i++)
		{
			var j = k - i;
			if (j < 0 || n - n2 < j) continue;

			var b1 = g1[i];
			var b2 = g2[j];

			r += TwoPointers(b1.Length, b2.Length, (x, y) => b1[x] + b2[y] <= p).Sum(_ => (long)b2.Length - _.j);
		}

		return r;
	}

	static long[][] GetAll(int n, long[] a)
	{
		var pn = 1 << n;
		var map = Array.ConvertAll(new bool[n + 1], _ => new List<long>());
		var dp = new long[pn];
		map[0].Add(0);

		for (int x = 1; x < pn; x++)
		{
			for (int i = 0; i < n; i++)
			{
				if ((x & (1 << i)) != 0)
				{
					var px = x ^ (1 << i);
					dp[x] = dp[px] + a[i];
					break;
				}
			}

			var c = BitOperations.PopCount((uint)x);
			map[c].Add(dp[x]);
		}

		var r = Array.ConvertAll(map, l => l.ToArray());
		foreach (var g in r)
			Array.Sort(g);
		return r;
	}

	static IEnumerable<(int i, int j)> TwoPointers(int n1, int n2, Func<int, int, bool> predicate)
	{
		for (int i = 0, j = 0; i < n1 && j < n2; ++i)
			for (; j < n2; ++j)
				if (predicate(i, j)) { yield return (i, j); break; }
	}
}
