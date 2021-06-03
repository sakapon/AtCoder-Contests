using System;
using System.Collections.Generic;
using System.Linq;

class Q055B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, p, q) = Read3();
		var a = ReadL();

		var n1 = n / 2;
		var n2 = n - n1;

		var g1 = Tally(n1, a[..n1], p);
		var g2 = Tally(n2, a[n1..], p);

		var r = 0;

		for (int k = 0; k <= 5; k++)
		{
			var l1 = g1[k];
			var l2 = g2[5 - k];

			foreach (var x in l1)
				foreach (var y in l2)
					if (x * y % p == q) r++;
		}
		return r;
	}

	static List<long>[] Tally(int n, long[] a, long p)
	{
		var r = new List<long>[6];
		var rn = Enumerable.Range(0, n).ToArray();

		for (int k = 0; k <= 5; k++)
		{
			var c = new List<long>();

			Combination(rn, k, pi =>
			{
				var m = 1L;
				for (int i = 0; i < k; i++)
					m = m * a[pi[i]] % p;
				c.Add(m);
			});

			r[k] = c;
		}
		return r;
	}

	static void Combination<T>(T[] values, int r, Action<T[]> action)
	{
		var n = values.Length;
		var p = new T[r];

		if (r > 0) Dfs(0, 0);
		else action(p);

		void Dfs(int i, int j0)
		{
			var i2 = i + 1;
			for (int j = j0; j < n; ++j)
			{
				p[i] = values[j];

				if (i2 < r) Dfs(i2, j + 1);
				else action(p);
			}
		}
	}
}
