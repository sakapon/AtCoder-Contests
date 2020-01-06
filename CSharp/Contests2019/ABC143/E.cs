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

		var d = new long[n + 1, n + 1];
		for (int i = 1; i <= n; i++)
			for (int j = 1; j <= n; j++)
				d[i, j] = long.MaxValue;
		for (int i = 1; i <= n; i++)
			d[i, i] = 0;
		foreach (var r in rs)
		{
			d[r[0], r[1]] = r[2];
			d[r[1], r[0]] = r[2];
		}

		for (int k = 1; k <= n; k++)
			for (int i = 1; i <= n; i++)
				for (int j = 1; j <= n; j++)
					if (d[i, k] < long.MaxValue && d[k, j] < long.MaxValue)
						d[i, j] = Math.Min(d[i, j], d[i, k] + d[k, j]);

		var map = new int[n + 1].Select(_ => new List<int>()).ToArray();
		for (int i = 1; i <= n; i++)
			for (int j = 1; j <= n; j++)
				if (d[i, j] != 0 && d[i, j] <= h[2]) map[i].Add(j);

		var g = new int[n + 1, n + 1];
		for (int i = 1; i <= n; i++)
			for (int j = 1; j <= n; j++)
				g[i, j] = -1;
		var q = new Queue<int>();
		for (int i = 1; i <= n; i++)
		{
			g[i, i] = 0;
			q.Enqueue(i);

			while (q.Any())
			{
				var p = q.Dequeue();
				foreach (var np in map[p])
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
