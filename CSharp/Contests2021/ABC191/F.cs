using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var map = new Dictionary<int, int>();
		foreach (var x in a)
			foreach (int d in Divisors(x))
				if (map.ContainsKey(d)) map[d] = Gcd(map[d], x);
				else map[d] = x;

		var min = a.Min();
		Console.WriteLine(map.Count(p => p.Key == p.Value && p.Key <= min));
	}

	static int Gcd(int a, int b) { for (int r; (r = a % b) > 0; a = b, b = r) ; return b; }

	static long[] Divisors(long n)
	{
		var r = new List<long>();
		for (long x = 1; x * x <= n; ++x) if (n % x == 0) r.Add(x);
		var i = r.Count - 1;
		if (r[i] * r[i] == n) --i;
		for (; i >= 0; --i) r.Add(n / r[i]);
		return r.ToArray();
	}
}
