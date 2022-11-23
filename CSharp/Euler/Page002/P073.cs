using System;
using System.Collections.Generic;
using System.Linq;

class P073
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		const int d_max = 12000;

		var r = 0;
		var reducibles = Array.ConvertAll(new bool[d_max + 1], _ => new HashSet<int>());

		for (int d = 2; d <= d_max; d++)
		{
			var n = d / 3 + 1;
			var n_max = (d - 1) / 2;

			for (; n <= n_max; n++)
			{
				if (reducibles[d].Contains(n)) continue;

				r++;
				for (var (d2, n2) = (d << 1, n << 1); d2 <= d_max; d2 += d, n2 += n)
				{
					reducibles[d2].Add(n2);
				}
			}
		}
		return r;
	}
}
