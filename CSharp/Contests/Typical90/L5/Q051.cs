using System;
using System.Collections.Generic;
using System.Linq;

class Q051
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
		var map = Array.ConvertAll(new bool[n + 1], _ => new List<long>());

		AllBoolCombination(n, b =>
		{
			var c = 0;
			var sum = 0L;
			for (int i = 0; i < n; ++i)
			{
				if (b[i])
				{
					c++;
					sum += a[i];
				}
			}
			map[c].Add(sum);
			return false;
		});

		return Array.ConvertAll(map, l => l.OrderBy(x => x).ToArray());
	}

	static void AllBoolCombination(int n, Func<bool[], bool> action)
	{
		if (n > 30) throw new InvalidOperationException();
		var pn = 1 << n;
		var b = new bool[n];

		for (int x = 0; x < pn; ++x)
		{
			for (int i = 0; i < n; ++i) b[i] = (x & (1 << i)) != 0;
			if (action(b)) break;
		}
	}

	static IEnumerable<(int i, int j)> TwoPointers(int n1, int n2, Func<int, int, bool> predicate)
	{
		for (int i = 0, j = 0; i < n1 && j < n2; ++i)
			for (; j < n2; ++j)
				if (predicate(i, j)) { yield return (i, j); break; }
	}
}
