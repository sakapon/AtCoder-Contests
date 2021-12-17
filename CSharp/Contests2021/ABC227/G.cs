using System;
using System.Collections.Generic;
using System.Linq;

class G
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2L();

		var pmax = (int)Math.Sqrt(n) + 1;
		var ps = GetPrimes(pmax);

		var indexes = new int[pmax + 1];

		// nCk = n...(n-k+1) / k...1
		var vu = Enumerable.Range(0, (int)k + 1).Select(x => x + n - k).ToArray();
		var vd = Enumerable.Range(0, (int)k + 1).ToArray();

		foreach (var p in ps)
		{
			var x0 = (n - k + 1) / p * p;
			if (x0 < n - k + 1) x0 += p;

			for (long x = x0; x <= n; x += p)
			{
				while (vu[x - n + k] % p == 0)
				{
					indexes[p]++;
					vu[x - n + k] /= p;
				}
			}
			for (int x = p; x <= k; x += p)
			{
				while (vd[x] % p == 0)
				{
					indexes[p]--;
					vd[x] /= p;
				}
			}
		}

		var larger = 0;

		foreach (var x in vu[1..])
		{
			if (x != 1) larger++;
		}
		foreach (var x in vd[1..])
		{
			if (x != 1) larger--;
		}

		var r = indexes.Where(x => x != 0).Aggregate(1L, (x, y) => x * (y + 1) % M);
		for (int i = 0; i < larger; i++)
		{
			r = r * 2 % M;
		}
		return r;
	}

	const long M = 998244353;

	static int[] GetPrimes(int n)
	{
		var b = new bool[n + 1];
		for (int p = 2; p * p <= n; ++p) if (!b[p]) for (int x = p * p; x <= n; x += p) b[x] = true;
		var r = new List<int>();
		for (int x = 2; x <= n; ++x) if (!b[x]) r.Add(x);
		return r.ToArray();
	}
}
