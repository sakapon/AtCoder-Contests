using System;
using System.Linq;

class Q030
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();
		return GetPrimeTypes(n).Count(x => x >= k);
	}

	static int[] GetPrimeTypes(int n)
	{
		var c = new int[n + 1];
		for (int p = 2; p <= n; ++p)
			if (c[p] == 0)
				for (int x = p; x <= n; x += p)
					++c[x];
		return c;
	}
}
