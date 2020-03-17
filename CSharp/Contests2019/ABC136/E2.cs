using System;
using System.Collections.Generic;
using System.Linq;

class E2
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var a = read();

		var s = a.Sum();
		foreach (int d in Divisors(s).Reverse())
		{
			var m = a.Select(x => x % d).OrderBy(x => -x).ToArray();
			if (m.Skip(m.Sum() / d).Sum() <= h[1]) { Console.WriteLine(d); return; }
		}
	}

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
