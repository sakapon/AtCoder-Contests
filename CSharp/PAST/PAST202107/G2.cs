using System;
using System.Collections.Generic;

class G2
{
	static void Main()
	{
		var n = long.Parse(Console.ReadLine());

		var r = new List<long>();

		for (long p = 1; n != 0; p *= 3)
		{
			var m = n % (3 * p);
			if (m == 0) continue;

			if (m == p || m == -p)
			{
				r.Add(m);
				n -= p;
			}
			else
			{
				r.Add(-m / 2);
				n += p;
			}
		}

		Console.WriteLine(r.Count);
		Console.WriteLine(string.Join(" ", r));
	}
}
