using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int, int) Read4() { var a = Read(); return (a[0], a[1], a[2], a[3]); }
	static void Main() => Console.WriteLine(Solve() ? "Takahashi" : "Aoki");
	static bool Solve()
	{
		var (a, b, c, d) = Read4();

		var ps = GetPrimes(b + d);

		for (int i = a; i <= b; i++)
		{
			var isPrime = false;

			for (int j = c; j <= d; j++)
			{
				if (!ps[i + j])
				{
					isPrime = true;
					break;
				}
			}
			if (!isPrime) return true;
		}

		return false;
	}

	static bool[] GetPrimes(int n)
	{
		var b = new bool[n + 1];
		for (int p = 2; p * p <= n; ++p) if (!b[p]) for (int x = p * p; x <= n; x += p) b[x] = true;
		return b;
	}
}
