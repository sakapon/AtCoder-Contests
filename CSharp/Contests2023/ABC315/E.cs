using System;
using System.Collections.Generic;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var map = Array.ConvertAll(new bool[n], _ => Read());

		var indeg = new int[n + 1];
		var u = new bool[n + 1];
		var q = new Queue<int>();
		var r = new List<int>();

		q.Enqueue(1);
		while (q.Count > 0)
		{
			var v = q.Dequeue();
			foreach (var nv in map[v - 1][1..])
			{
				indeg[nv]++;
				if (u[nv]) continue;
				u[nv] = true;
				q.Enqueue(nv);
			}
		}

		q.Enqueue(1);
		while (q.Count > 0)
		{
			var v = q.Dequeue();
			foreach (var nv in map[v - 1][1..])
			{
				if (--indeg[nv] > 0) continue;
				q.Enqueue(nv);
				r.Add(nv);
			}
		}

		r.Reverse();
		return string.Join(" ", r);
	}
}
