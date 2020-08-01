using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		var n = h[0];
		var es = new int[h[1]].Select(_ => Read()).ToArray();

		var map = Array.ConvertAll(new int[n], _ => new List<int[]>());
		var indeg = new int[n];
		foreach (var e in es)
		{
			map[e[0]].Add(e);
			indeg[e[1]]++;
		}

		var svs = Enumerable.Range(0, n).Where(v => indeg[v] == 0).ToArray();
		var seq = new List<int>();
		var q = new Queue<int>();

		foreach (var sv in svs)
		{
			seq.Add(sv);
			q.Enqueue(sv);

			while (q.Count > 0)
			{
				var v = q.Dequeue();
				foreach (var e in map[v])
				{
					if (--indeg[e[1]] > 0) continue;
					seq.Add(e[1]);
					q.Enqueue(e[1]);
				}
			}
		}
		Console.WriteLine(string.Join("\n", seq));
	}
}
