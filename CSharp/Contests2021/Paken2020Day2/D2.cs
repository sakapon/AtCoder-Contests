using System;
using System.Collections.Generic;
using System.Linq;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		const int PeopleCount = 200000;
		const int AnimalsCount = 500000;

		var n = int.Parse(Console.ReadLine());
		var es = new List<int[]>();

		for (int i = 1; i <= n; i++)
		{
			Console.ReadLine();
			foreach (var x in Read()) es.Add(new[] { PeopleCount + x, i });
			Console.ReadLine();
			foreach (var x in Read()) es.Add(new[] { i, PeopleCount + x });
		}

		var r = TopologicalSort(PeopleCount + AnimalsCount + 1, es.ToArray());
		if (r == null) { Console.WriteLine(-1); return; }
		Console.WriteLine(string.Join(" ", r.Where(x => 1 <= x && x <= n)));
	}

	static int[] TopologicalSort(int n, int[][] des)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
		var indeg = new int[n];
		foreach (var e in des)
		{
			map[e[0]].Add(e);
			++indeg[e[1]];
		}

		var r = new List<int>();
		var q = new Queue<int>();
		var svs = Enumerable.Range(0, n).Where(v => indeg[v] == 0).ToArray();

		foreach (var sv in svs)
		{
			r.Add(sv);
			q.Enqueue(sv);

			while (q.Count > 0)
			{
				var v = q.Dequeue();
				foreach (var e in map[v])
				{
					if (--indeg[e[1]] > 0) continue;
					r.Add(e[1]);
					q.Enqueue(e[1]);
				}
			}
		}
		if (r.Count < n) return null;
		return r.ToArray();
	}
}
