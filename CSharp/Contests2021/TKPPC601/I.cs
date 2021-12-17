using System;
using System.Collections.Generic;
using System.Linq;

class I
{
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select(_ => Solve())));
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());

		return Factorize(n)
			.GroupBy(p => p)
			.Select(g => g.Key == 2 ? CountFor2(g.Count()) : 2)
			.Aggregate((x, y) => x * y);

		static int CountFor2(int pow)
		{
			if (pow <= 1) return 1;
			if (pow == 2) return 2;
			return 4;
		}
	}

	static long[] Factorize(long n)
	{
		var r = new List<long>();
		for (long x = 2; x * x <= n && n > 1; ++x) while (n % x == 0) { r.Add(x); n /= x; }
		if (n > 1) r.Add(n);
		return r.ToArray();
	}
}
