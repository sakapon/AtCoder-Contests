using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static void Main()
	{
		var n = long.Parse(Console.ReadLine());
		Console.WriteLine(Divisors(n).Select(x => x + n / x - 2).Min());
	}

	static long[] Divisors(long v)
	{
		var d = new List<long>();
		for (long i = 1, rv = (long)Math.Sqrt(v); i <= rv; i++)
			if (v % i == 0) d.Add(i);
		return d.ToArray();
	}
}
