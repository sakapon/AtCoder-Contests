using System;
using System.Collections.Generic;

class D
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = long.Parse(Console.ReadLine());
		var d = new Dictionary<long, long> { [0] = 1 };
		return f(n);

		long f(long k) => d.ContainsKey(k) ? d[k] : d[k] = f(k / 2) + f(k / 3);
	}
}
