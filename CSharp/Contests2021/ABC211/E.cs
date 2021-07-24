using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var K = int.Parse(Console.ReadLine());
		var s = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

		var sets = Array.ConvertAll(new bool[K + 1], _ => new HashSet<ulong>());
		sets[0].Add(0);

		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < n; j++)
			{
				if (s[i][j] == '#') continue;

				var id = i * n + j;
				sets[1].Add(1UL << id);
			}
		}

		for (int k = 2; k <= K; k++)
		{
			foreach (var x in sets[k - 1])
			{
				for (int i = 0; i < n; i++)
				{
					for (int j = 0; j < n; j++)
					{
						if (s[i][j] == '#') continue;

						var id = i * n + j;
						var f = 1UL << id;
						if ((x & f) != 0) continue;

						if (i - 1 >= 0 && (x & (1UL << (id - n))) != 0 ||
							i + 1 < n && (x & (1UL << (id + n))) != 0 ||
							j - 1 >= 0 && (x & (1UL << (id - 1))) != 0 ||
							j + 1 < n && (x & (1UL << (id + 1))) != 0)
							sets[k].Add(x | f);
					}
				}
			}
		}

		return sets[K].Count;
	}
}
