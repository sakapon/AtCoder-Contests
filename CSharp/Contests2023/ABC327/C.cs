using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var n = 9;
		var n2 = 3;
		var a = Array.ConvertAll(new bool[n], _ => Read());

		var rn = Enumerable.Range(0, n).ToArray();

		if (!rn.All(i => a[i].Distinct().Count() == n)) return false;
		if (!rn.All(j => rn.Select(i => a[i][j]).Distinct().Count() == n)) return false;

		for (int i = 0; i < n2; i++)
		{
			for (int j = 0; j < n2; j++)
			{
				var oi = 3 * i;
				var oj = 3 * j;

				var l = new List<int>();

				for (int x = 0; x < n2; x++)
				{
					for (int y = 0; y < n2; y++)
					{
						l.Add(a[oi + x][oj + y]);
					}
				}

				if (l.Distinct().Count() != 9) return false;
			}
		}
		return true;
	}
}
