using System;
using System.Collections.Generic;
using System.Linq;

class K
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var r = new long[n];
		var ds = Divisors(a.Sum());

		foreach (var d in ds)
		{
			int s = 0, c = 0;
			foreach (var x in a)
			{
				if ((s += x) % d == 0)
					c++;
			}

			for (int i = 0; i < c; i++)
			{
				r[i] = d;
			}
		}
		Console.WriteLine(string.Join("\n", r));
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
