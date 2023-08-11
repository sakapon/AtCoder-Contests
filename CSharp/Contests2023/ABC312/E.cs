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

		var nexts = Array.ConvertAll(ps, _ => new HashSet<int>());
		var xmap = NewArray3(m + 1, m, m, -1);
		var ymap = NewArray3(m, m + 1, m, -1);
		var zmap = NewArray3(m, m, m + 1, -1);

		void Add(int[][][] map, int x, int y, int z, int i)
		{
			if (map[x][y][z] == -1)
			{
				map[x][y][z] = i;
			}
			else
			{
				var j = map[x][y][z];
				nexts[i].Add(j);
				nexts[j].Add(i);
			}
		}

		for (int i = 0; i < n; i++)
		{
			var p = ps[i];
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

		return string.Join("\n", nexts.Select(set => set.Count));
	}

	static T[][][] NewArray3<T>(int n1, int n2, int n3, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => Array.ConvertAll(new bool[n3], ___ => v)));
}
