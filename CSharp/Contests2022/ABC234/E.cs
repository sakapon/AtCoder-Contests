using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var x = long.Parse(Console.ReadLine());

		var nums = new List<long>();

		for (int k = 1; k <= 17; k++)
		{
			var ks = Enumerable.Range(0, k).ToArray();

			for (int d1 = 1; d1 <= 9; d1++)
			{
				for (int d = -9; d <= 9; d++)
				{
					var ds = ks.Select(v => d1 + v * d).ToArray();
					if (ds.Any(v => v < 0 || v > 9)) continue;
					var s = string.Join("", ds);
					nums.Add(long.Parse(s));
				}
			}
		}
		nums.Add(111111_111111_111111);

		return nums.First(v => v >= x);
	}
}
