using System;
using System.Collections.Generic;
using System.Linq;

class CQ
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		var n = h[0];

		var map = Array.ConvertAll(new int[n + 1], _ => new List<int>());
		foreach (var e in new int[h[1]].Select(_ => Read()))
		{
			map[e[0]].Add(e[1]);
			map[e[1]].Add(e[0]);
		}

		var u = new bool[n + 1];
		var g = new int[n + 1];
		var gi = 0;
		var q = new Queue<int>();

		for (int i = 1; i <= n; i++)
		{
			if (u[i]) continue;
			u[i] = true;
			g[i] = ++gi;
			q.Enqueue(i);

			while (q.TryDequeue(out var v))
			{
				foreach (var nv in map[v])
				{
					if (u[nv]) continue;
					u[nv] = true;
					g[nv] = gi;
					q.Enqueue(nv);
				}
			}
		}
		Console.WriteLine(g.Distinct().Count() - 2);
	}
}
