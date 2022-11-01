using System;
using System.Collections.Generic;
using System.Linq;

class P050
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		const int n = 1000000;
		var (b, ps) = GetPrimes(n);

		for (int k = 600; k > 0; k--)
		{
			var r = Sum(k);
			if (r != -1) return r;
		}
		throw new InvalidOperationException();

		int Sum(int count)
		{
			if (count % 2 == 0)
			{
				var r = ps.Take(count).Sum();
				if (CheckPrime(r)) return r;
				return -1;
			}
			else
			{
				var r = ps.Take(count).Sum();
				if (CheckPrime(r)) return r;

				for (int i = count; i < ps.Length; i++)
				{
					r -= ps[i - count];
					r += ps[i];
					if (CheckPrime(r)) return r;
				}
				return -1;
			}
		}

		bool CheckPrime(int x)
		{
			return x <= n && !b[x];
		}
	}

	static (bool[], int[]) GetPrimes(int n)
	{
		var b = new bool[n + 1];
		for (int p = 2; p * p <= n; ++p) if (!b[p]) for (int x = p * p; x <= n; x += p) b[x] = true;
		var r = new List<int>();
		for (int x = 2; x <= n; ++x) if (!b[x]) r.Add(x);
		return (b, r.ToArray());
	}
}
