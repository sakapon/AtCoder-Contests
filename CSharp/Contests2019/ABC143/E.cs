using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		int n = h[0];
		var rs = new int[h[1]].Select(_ => read()).ToArray();
		var qs = new int[int.Parse(Console.ReadLine())].Select(_ => read()).ToArray();

		var map = new int[n + 1].Select(_ => new List<int[]>()).ToArray();
		foreach (var r in rs)
		{
			map[r[0]].Add(new[] { r[1], r[2] });
			map[r[1]].Add(new[] { r[0], r[2] });
		}

		var d = new long[n + 1, n + 1];
		for (int i = 1; i <= n; i++)
			for (int j = 1; j <= n; j++)
				d[i, j] = long.MaxValue;
		var q = new List<int>();
		for (int i = 1; i <= n; i++)
		{
			d[i, i] = 0;
			q.Add(i);

			while (q.Any())
			{
				var ps = q.ToArray();
				q.Clear();

				foreach (var p in ps)
					foreach (var np in map[p])
					{
						var nd = d[i, p] + np[1];
						if (nd < d[i, np[0]])
						{
							d[i, np[0]] = nd;
							q.Add(np[0]);
						}
					}
			}
		}

		var map2 = new int[n + 1].Select(_ => new List<int>()).ToArray();
		for (int i = 1; i <= n; i++)
			for (int j = 1; j <= n; j++)
				if (d[i, j] != 0 && d[i, j] <= h[2]) map2[i].Add(j);

		foreach (var query in qs)
		{
			Find(query, map2, q);
			q.Clear();
		}
	}

	static void Find(int[] query, List<int>[] map, List<int> q)
	{
		var u = new bool[map.Length];
		u[query[0]] = true;
		q.Add(query[0]);

		for (int i = 0; q.Any(); i++)
		{
			var ps = q.ToArray();
			q.Clear();

			foreach (var p in ps)
				foreach (var np in map[p])
				{
					if (u[np]) continue;
					if (np == query[1]) { Console.WriteLine(i); return; }
					u[np] = true;
					q.Add(np);
				}
		}
		Console.WriteLine(-1);
	}
}
