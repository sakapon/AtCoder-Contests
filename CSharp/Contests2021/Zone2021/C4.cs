using System;
using System.Collections.Generic;
using System.Linq;

class C4
{
	const int all = 1 << 5;
	const int max = 1 << 30;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read());

		// マスクされた種類に対する最小値
		int GetMin(int i, int x)
		{
			var r = max;
			for (int j = 0; j < 5; j++)
			{
				if ((x & (1 << j)) != 0)
				{
					r = Math.Min(r, ps[i][j]);
				}
			}
			return r;
		}

		var rn = Enumerable.Range(0, n).ToArray();
		var maxes = Enumerable.Range(0, all).Select(x => rn.Max(i => GetMin(i, x))).ToArray();

		var r = 0;

		for (int x = 0; x < all; x++)
		{
			for (int y = 0; y < all; y++)
			{
				for (int z = 0; z < all; z++)
				{
					if ((x | y | z) == all - 1)
					{
						var m = Math.Min(Math.Min(maxes[x], maxes[y]), maxes[z]);
						r = Math.Max(r, m);
					}
				}
			}
		}
		return r;
	}
}
