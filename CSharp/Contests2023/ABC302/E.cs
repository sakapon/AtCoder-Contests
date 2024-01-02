using System;
using System.Collections.Generic;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, qc) = Read2();
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		var map = Array.ConvertAll(new bool[n + 1], _ => new HashSet<int>());

		var r = new List<int>();
		var c = n;

		foreach (var q in qs)
		{
			if (q[0] == 1)
			{
				var u = q[1];
				var v = q[2];

				if (map[u].Count == 0) c--;
				if (map[v].Count == 0) c--;

				map[u].Add(v);
				map[v].Add(u);
			}
			else
			{
				var v = q[1];

				foreach (var u in map[v])
				{
					if (map[u].Count == 1) c++;
					map[u].Remove(v);
				}

				if (map[v].Count > 0) c++;
				map[v].Clear();
			}

			r.Add(c);
		}

		return string.Join("\n", r);
	}
}
