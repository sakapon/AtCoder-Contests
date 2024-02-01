using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using CoderLib6.Graphs;

class E3
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w, t) = Read3();
		var s0 = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		var n = h * w;
		var s = s0.SelectMany(c => c).ToArray();

		var rn = Enumerable.Range(0, n).ToArray();
		var sv = rn.Single(v => s[v] == 'S');
		var gv = rn.Single(v => s[v] == 'G');
		var ovs = rn.Where(v => s[v] == 'o').Append(sv).Append(gv).ToArray();

		var d = ovs.Select(v =>
		{
			var cs = Bfs(v);
			return ovs.Select(v => cs[v]).ToArray();
		}).ToArray();

		// お菓子の数
		var m = ovs.Length - 2;
		sv = m;
		gv = m + 1;

		var dp = TSP.Execute(m + 2, sv, d);
		var f = 0U;

		for (uint x = 0; x < 1 << m + 2; x++)
		{
			if ((x & (1U << sv)) != 0) continue;
			if (dp[x][gv] <= t) f |= 1U << BitOperations.PopCount(x) - 1;
		}
		if (f == 0) return -1;
		return BitOperations.Log2(f);

		IEnumerable<int> GetNexts(int v)
		{
			var (i, j) = (v / w, v % w);
			if (j > 0) yield return v - 1;
			if (i > 0) yield return v - w;
			if (j + 1 < w) yield return v + 1;
			if (i + 1 < h) yield return v + w;
		}

		long[] Bfs(int sv)
		{
			var cs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
			var q = new Queue<int>();
			cs[sv] = 0;
			q.Enqueue(sv);

			while (q.Count > 0)
			{
				var v = q.Dequeue();
				var nc = cs[v] + 1;

				foreach (var nv in GetNexts(v))
				{
					if (s[nv] == '#') continue;
					if (cs[nv] <= nc) continue;
					cs[nv] = nc;
					q.Enqueue(nv);
				}
			}
			return cs;
		}
	}
}
