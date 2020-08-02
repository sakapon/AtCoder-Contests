using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

class B11
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var ok = false;
		Task.Run(() =>
		{
			Thread.Sleep(100);
			if (ok) return;
			Console.WriteLine("NEGATIVE CYCLE");
			Environment.Exit(0);
		});

		var h = Read();
		var es = new int[h[1]].Select(_ => Read()).ToArray();

		var d = Dijklmna(h[0] - 1, h[2], -1, es);
		Console.WriteLine(string.Join("\n", d.Select(x => x == long.MaxValue ? "INF" : $"{x}")));
		ok = true;
	}

	static long[] Dijklmna(int n, int sv, int ev, int[][] es)
	{
		var map = Array.ConvertAll(new int[n + 1], _ => new List<int[]>());
		foreach (var e in es)
		{
			map[e[0]].Add(new[] { e[1], e[2] });
		}

		var d = Enumerable.Repeat(long.MaxValue, n + 1).ToArray();
		var q = new Queue<int>();
		d[sv] = 0;
		q.Enqueue(sv);

		while (q.Count > 0)
		{
			var v = q.Dequeue();
			foreach (var e in map[v])
			{
				if (d[e[0]] <= d[v] + e[1]) continue;
				d[e[0]] = d[v] + e[1];
				q.Enqueue(e[0]);
			}
		}
		return d;
	}
}
