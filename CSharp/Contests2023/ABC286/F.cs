using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using CoderLib8.Numerics;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var ps = GetPrimes(23);
		ps[0] = 4;
		ps[1] = 9;

		var m = ps.Sum();
		Console.WriteLine(m);

		var a = new int[m];

		var si = 0;
		foreach (var p in ps)
		{
			for (int i = 0; i < p - 1; i++)
			{
				a[si + i] = si + i + 1;
			}
			a[si + p - 1] = si;
			si += p;
		}
		Console.WriteLine(string.Join(" ", a.Select(x => x + 1)));

		var b = Read().Select(x => x - 1).ToArray();

		var n = 0L;
		var prod = 1L;
		si = 0;
		foreach (var p in ps)
		{
			var rem = b[si] - si;

			if (prod == 1)
			{
				n = rem;
			}
			else
			{
				var crt = new CRT(prod, p);
				n = crt.Solve(n, rem);
			}

			prod *= p;
			si += p;
		}

		return n;
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
