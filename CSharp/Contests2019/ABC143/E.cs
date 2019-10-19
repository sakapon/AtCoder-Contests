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
		var map2 = new int[n + 1].Select(_ => new List<int>()).ToArray();

		var q = new Queue<int>();
		for (int i = 1; i <= n; i++)
		{
			var d = Enumerable.Repeat(int.MaxValue, n + 1).ToArray();
			d[i] = 0;
			q.Enqueue(i);

			while (q.Any())
			{
				var p = q.Dequeue();
				foreach (var np in map[p])
				{
					int j = np[0], nd = d[p] + np[1];
					if (nd <= h[2] && nd < d[j])
					{
						if (d[j] > h[2]) map2[i].Add(j);
						d[j] = nd;
						q.Enqueue(j);
					}
				}
			}
		}

		var g = new int[n + 1, n + 1];
		for (int i = 1; i <= n; i++)
			for (int j = 1; j <= n; j++)
				g[i, j] = -1;
		for (int i = 1; i <= n; i++)
		{
			g[i, i] = 0;
			q.Enqueue(i);

			while (q.Any())
			{
				var p = q.Dequeue();
				foreach (var np in map2[p])
				{
					if (g[i, np] >= 0) continue;
					g[i, np] = g[i, p] + 1;
					q.Enqueue(np);
				}
			}
		}
		Console.WriteLine(string.Join("\n", qs.Select(x => g[x[0], x[1]]).Select(x => x > 0 ? x - 1 : x)));
	}
}
