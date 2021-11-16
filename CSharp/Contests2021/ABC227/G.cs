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

		var pmax = (int)Math.Sqrt(n) + 10;
		var ps = GetPrimes(pmax);

		var indexes = new int[pmax + 1];

		// nCk = n...(n-k+1) / k...1
		var vu = Enumerable.Range(1, (int)k).Select(x => x + n - k).ToArray();
		var vd = Enumerable.Range(1, (int)k).ToArray();

		foreach (var p in ps)
		{
			var x0 = (n - k + 1) / p * p;
			if (x0 < n - k + 1) x0 += p;

			for (long x = x0; x <= n; x += p)
			{
				while (vu[x - n + k - 1] % p == 0)
				{
					indexes[p]++;
					vu[x - n + k - 1] /= p;
				}
			}
			for (int x = p; x <= k; x += p)
			{
				while (vd[x - 1] % p == 0)
				{
					indexes[p]--;
					vd[x - 1] /= p;
				}
			}
		}

		var larger = 0;

		foreach (var x in vu)
		{
			if (x == 1) continue;
			if (x <= pmax)
			{
				indexes[x]++;
			}
			else
			{
				larger++;
			}
		}
		foreach (var x in vd)
		{
			if (x == 1) continue;
			if (x <= pmax)
			{
				indexes[x]--;
			}
			else
			{
				larger--;
			}
		}

		var r = indexes.Select(x => x + 1L).Aggregate((x, y) => x * y % M);
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
