using System;
using System.Collections.Generic;

class E2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, qc) = Read2();
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		var b = new bool[qc];
		Array.Fill(b, true);
		var cs = new int[n + 1];
		var map = Array.ConvertAll(cs, _ => new List<(int, int)>());

		var r = new List<int>();
		var rc = n;

		for (int qi = 0; qi < qc; qi++)
		{
			var q = qs[qi];

			if (q[0] == 1)
			{
				var u = q[1];
				var v = q[2];

				if (cs[u] == 0) rc--;
				if (cs[v] == 0) rc--;

				cs[u]++;
				cs[v]++;
				map[u].Add((qi, v));
				map[v].Add((qi, u));
			}
			else
			{
				var v = q[1];

				foreach (var (qj, u) in map[v])
				{
					if (b[qj])
					{
						b[qj] = false;
						if (cs[u] == 1) rc++;
						cs[u]--;
					}
				}

				if (cs[v] > 0) rc++;
				cs[v] = 0;
				map[v].Clear();
			}

			r.Add(rc);
		}

		return string.Join("\n", r);
	}
}
