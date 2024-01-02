using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read());

		const int m = 100;

		var nexts = Array.ConvertAll(new bool[n + 1], _ => new HashSet<int>());
		var xmap = new int[m + 1, m, m];
		var ymap = new int[m, m + 1, m];
		var zmap = new int[m, m, m + 1];

		void Add(int[,,] map, int x, int y, int z, int i)
		{
			if (map[x, y, z] == 0)
			{
				map[x, y, z] = i;
			}
			else
			{
				var j = map[x, y, z];
				nexts[i].Add(j);
				nexts[j].Add(i);
			}
		}

		for (int i = 1; i <= n; i++)
		{
			var p = ps[i - 1];
			var (x1, x2) = (p[0], p[3]);
			var (y1, y2) = (p[1], p[4]);
			var (z1, z2) = (p[2], p[5]);

			for (int y = y1; y < y2; y++)
			{
				for (int z = z1; z < z2; z++)
				{
					Add(xmap, x1, y, z, i);
					Add(xmap, x2, y, z, i);
				}
			}

			for (int z = z1; z < z2; z++)
			{
				for (int x = x1; x < x2; x++)
				{
					Add(ymap, x, y1, z, i);
					Add(ymap, x, y2, z, i);
				}
			}

			for (int x = x1; x < x2; x++)
			{
				for (int y = y1; y < y2; y++)
				{
					Add(zmap, x, y, z1, i);
					Add(zmap, x, y, z2, i);
				}
			}
		}

		return string.Join("\n", nexts[1..].Select(set => set.Count));
	}
}
