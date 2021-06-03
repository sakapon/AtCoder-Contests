using System;
using System.Collections.Generic;
using System.Linq;

class Q054
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var rs = Array.ConvertAll(new bool[m], _ => { Console.ReadLine(); return Read(); });

		var map = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
		for (int i = 0; i < m; i++)
		{
			for (int j = 0; j < rs[i].Length; j++)
			{
				map[rs[i][j]].Add(i);
			}
		}

		var ru = new bool[m];

		var r = Bfs(n + 1, v =>
		{
			var nv = new List<int>();
			foreach (var ri in map[v])
			{
				if (ru[ri]) continue;
				ru[ri] = true;
				nv.AddRange(rs[ri]);
			}
			return nv.ToArray();
		}, 1);

		return string.Join("\n", r[1..].Select(x => x == long.MaxValue ? -1 : x));
	}

	public static long[] Bfs(int n, Func<int, int[]> nexts, int sv, int ev = -1)
	{
		var costs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
		var q = new Queue<int>();
		costs[sv] = 0;
		q.Enqueue(sv);

		while (q.Count > 0)
		{
			var v = q.Dequeue();
			var nc = costs[v] + 1;

			foreach (var nv in nexts(v))
			{
				if (costs[nv] <= nc) continue;
				costs[nv] = nc;
				if (nv == ev) return costs;
				q.Enqueue(nv);
			}
		}
		return costs;
	}
}
