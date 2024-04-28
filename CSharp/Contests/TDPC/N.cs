using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.Values;

class N
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n - 1], _ => Read2());

		var mc = new MCombination(n);

		var mapL = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
		for (int e = 0; e < n - 1; e++)
		{
			var (w, v) = es[e];
			mapL[w].Add(e);
			mapL[v].Add(e);
		}
		var map = Array.ConvertAll(mapL, l => l.ToArray());

		var u = new bool[n - 1];
		var q = new Queue<int>();
		var mapE = Array.ConvertAll(u, _ => new List<int>());

		return Enumerable.Range(0, n - 1).Sum(ForEdge) % M;

		long ForEdge(int se)
		{
			Array.Clear(u, 0, u.Length);
			Array.ForEach(mapE, l => l.Clear());
			u[se] = true;
			q.Enqueue(se);

			while (q.Count > 0)
			{
				var e = q.Dequeue();

				foreach (var ne in map[es[e].Item1])
				{
					if (u[ne]) continue;
					u[ne] = true;
					q.Enqueue(ne);
					mapE[e].Add(ne);
				}
				foreach (var ne in map[es[e].Item2])
				{
					if (u[ne]) continue;
					u[ne] = true;
					q.Enqueue(ne);
					mapE[e].Add(ne);
				}
			}

			return DFS(se).comb;
		}

		(int count, long comb) DFS(int v)
		{
			var count = 0;
			var comb = 1L;

			foreach (var nv in mapE[v])
			{
				var (c, r) = DFS(nv);
				count += c;
				comb *= mc.MInvFactorial(c);
				comb %= M;
				comb *= r;
				comb %= M;
			}

			comb *= mc.MFactorial(count);
			comb %= M;
			return (count + 1, comb);
		}
	}

	const long M = 1000000007;
}
