using System;
using System.Collections.Generic;

class E
{
	const long M = 1000000007;
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n - 1], _ => ReadL());

		var map = ToMap(n + 1, es, false);
		var xor = new long[n + 1];

		Dfs(1, -1);

		void Dfs(long v, long pv)
		{
			foreach (var e in map[v])
			{
				var nv = e[1];
				if (nv == pv) continue;

				xor[nv] = xor[v] ^ e[2];
				Dfs(nv, v);
			}
		}

		var r = 0L;

		for (int i = 0; i < 60; i++)
		{
			var c = 0L;
			for (int v = 1; v <= n; v++)
			{
				if ((xor[v] & (1L << i)) != 0)
				{
					c++;
				}
			}

			r += (1L << i) % M * (c * (n - c) % M);
			r %= M;
		}
		return r;
	}

	static List<long[]>[] ToMap(int n, long[][] es, bool directed)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<long[]>());
		foreach (var e in es)
		{
			map[e[0]].Add(e);
			if (!directed) map[e[1]].Add(new[] { e[1], e[0], e[2] });
		}
		return map;
	}
}
