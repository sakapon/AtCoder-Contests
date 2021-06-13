using System;
using System.Collections.Generic;

class Q062
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n], _ => Read2());

		var map = Array.ConvertAll(new bool[n + 1], _ => new List<int>());

		for (int i = 1; i <= n; i++)
		{
			var (a, b) = es[i - 1];
			map[i == a ? 0 : a].Add(i);
			map[i == b ? 0 : b].Add(i);
		}

		var r = new List<int>();
		var u = new bool[n + 1];
		var q = new Queue<int>();
		u[0] = true;
		q.Enqueue(0);

		while (q.TryDequeue(out var v))
		{
			foreach (var nv in map[v])
			{
				if (u[nv]) continue;
				r.Add(nv);
				u[nv] = true;
				q.Enqueue(nv);
			}
		}

		if (r.Count < n) return -1;
		r.Reverse();
		return string.Join("\n", r);
	}
}
