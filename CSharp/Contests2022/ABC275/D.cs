using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = long.Parse(Console.ReadLine());

		var d = new Dictionary<long, long>();
		d[0] = 1;

		return f(n);

		long f(long k)
		{
			if (d.ContainsKey(k)) return d[k];

			var r = f(k / 2) + f(k / 3);
			d[k] = r;
			return r;
		}
	}
}
