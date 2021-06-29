using System;
using System.Collections.Generic;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (L, R) = Read2();

		var ps = GetPrimes(R);

		var u = new int[R];
		foreach (var p in ps)
		{
			for (int x = p; x < R; x += p)
			{
				u[x]++;
			}
		}
		foreach (var p in ps)
		{
			if (p * p >= R) break;
			for (int x = p * p; x < R; x += p * p)
			{
				u[x] = 0;
			}
		}

		var c = 0L;

		for (int g = 2; g < R; g++)
		{
			if (u[g] == 0) continue;

			var l = (L + g - 1) / g;
			var r = R / g;

			if (l >= r) continue;

			var t = (long)(r - l) * (r - l + 1) / 2;
			for (int d = l; d < r; d++)
			{
				t -= r / d - 1;
			}

			if (u[g] % 2 == 1) c += t;
			else c -= t;
		}

		return c * 2;
	}

	static int[] GetPrimes(int n)
	{
		var b = new bool[n + 1];
		for (int p = 2; p * p <= n; ++p) if (!b[p]) for (int x = p * p; x <= n; x += p) b[x] = true;
		var r = new List<int>();
		for (int x = 2; x <= n; ++x) if (!b[x]) r.Add(x);
		return r.ToArray();
	}
}
