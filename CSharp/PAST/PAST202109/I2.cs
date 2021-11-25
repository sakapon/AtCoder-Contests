using System;
using System.Collections.Generic;
using System.Linq;

class I2
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = ReadL();

		var set = new WBMultiSet<long>();

		var c = 0;
		foreach (var v in a)
		{
			var x = v;
			while ((x & 1) == 0)
			{
				x >>= 1;
				c++;
			}
			set.Add(x);
		}

		while (c > 0 && 3 * set.GetFirst() <= set.GetLast())
		{
			c--;
			var x = set.GetFirst();
			set.RemoveAt(0);
			set.Add(x * 3);
		}

		a = set.ToArray();
		var r = a[c % n];
		c /= n;

		while (c-- > 0)
		{
			r *= 3;
		}
		return r;
	}
}
