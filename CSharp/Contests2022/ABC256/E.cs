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
		var x = Read();
		var c = Read();

		var indeg = new int[n + 1];
		foreach (var v in x)
		{
			++indeg[v];
		}

		var u = new bool[n + 1];
		var q = new Queue<int>();
		for (int v = 1; v <= n; ++v)
			if (indeg[v] == 0)
				q.Enqueue(v);

		while (q.Count > 0)
		{
			var v = q.Dequeue();
			u[v] = true;

			var nv = x[v - 1];
			if (--indeg[nv] > 0) continue;
			q.Enqueue(nv);
		}

		var r = 0L;

		for (int v = 1; v <= n; v++)
		{
			if (u[v]) continue;

			var path = new List<int>();
			for (int t = v; !u[t]; t = x[t - 1])
			{
				u[t] = true;
				path.Add(t);
			}
			r += path.Min(t => c[t - 1]);
		}
		return r;
	}
}
