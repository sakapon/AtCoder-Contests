using System;
using System.Collections.Generic;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var a = Read();
		var h = a[..3];
		var w = a[3..];

		var rows = Array.ConvertAll(new bool[3], _ => new List<(int, int, int)>());

		for (int ri = 0; ri < 3; ri++)
		{
			var row = rows[ri];
			var sum = h[ri];

			for (int i = 1; i < sum; i++)
			{
				for (int j = 1; i + j < sum; j++)
				{
					row.Add((i, j, sum - i - j));
				}
			}
		}

		var r = 0;
		foreach (var r0 in rows[0])
		{
			foreach (var r1 in rows[1])
			{
				foreach (var r2 in rows[2])
				{
					if (r0.Item1 + r1.Item1 + r2.Item1 == w[0] &&
						r0.Item2 + r1.Item2 + r2.Item2 == w[1] &&
						r0.Item3 + r1.Item3 + r2.Item3 == w[2]) r++;
				}
			}
		}
		return r;
	}
}
