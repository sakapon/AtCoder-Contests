using System;
using System.Collections.Generic;

class E
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var K = int.Parse(Console.ReadLine());
		var s = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

		var sets = Array.ConvertAll(new bool[K + 1], _ => new HashSet<ulong>());
		sets[0].Add(0);

		for (int k = 1; k <= K; k++)
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

						if (i != 0 && (x & (1UL << (id - n))) != 0 ||
							i != n - 1 && (x & (1UL << (id + n))) != 0 ||
							j != 0 && (x & (1UL << (id - 1))) != 0 ||
							j != n - 1 && (x & (1UL << (id + 1))) != 0 ||
							x == 0)
							sets[k].Add(x | f);
					}
				}
			}
		}

		return sets[K].Count;
	}
}
