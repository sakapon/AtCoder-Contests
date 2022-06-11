using System;
using System.Linq;

class D
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		return GetFactors(n)[1..].GroupBy(x => x).Select(g => g.LongCount()).Sum(c => c * c);
	}

	// 代表元
	static int[] GetFactors(int n)
	{
		var a = new int[n + 1];
		for (int i = 1; i <= n; ++i) a[i] = i;
		var map = Array.ConvertAll(a, _ => 1);

		for (int q = 2; q <= n; ++q)
			if (a[q] != 1)
			{
				var p = a[q];
				for (int x = q; x <= n; x += q)
				{
					a[x] /= p;
					if (map[x] % p == 0) map[x] /= p;
					else map[x] *= p;
				}
			}
		return map;
	}
}
