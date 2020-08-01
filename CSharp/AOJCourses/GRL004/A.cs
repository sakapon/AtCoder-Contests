using System;
using System.Collections.Generic;
using System.Linq;

class A
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		var n = h[0];
		var es = new int[h[1]].Select(_ => Read()).ToArray();

		var map = Array.ConvertAll(new int[n + 1], _ => new List<int[]>());
		var indeg = new int[n + 1];
		foreach (var e in es)
		{
			map[e[0]].Add(e);
			indeg[e[1]]++;
		}

		var svs = Enumerable.Range(0, n + 1).Where(v => indeg[v] == 0).ToArray();
		var u = new bool[n + 1];
		var q = new Queue<int>();

		foreach (var sv in svs)
		{
			u[sv] = true;
			q.Enqueue(sv);

			while (q.Count > 0)
			{
				var v = q.Dequeue();
				foreach (var e in map[v])
				{
					if (--indeg[e[1]] > 0) continue;
					u[e[1]] = true;
					q.Enqueue(e[1]);
				}
			}
		}
		Console.WriteLine(u.Any(b => !b) ? 1 : 0);
	}
}
