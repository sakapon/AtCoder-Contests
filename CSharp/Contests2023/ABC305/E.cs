using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.DataTrees;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m, k) = Read3();
		var es = Array.ConvertAll(new bool[m], _ => Read());
		var ps = Array.ConvertAll(new bool[k], _ => Read2());

		var map = ToListMap(n + 1, es, true);
		var hs = new int[n + 1];
		Array.Fill(hs, -1);
		var q = PriorityQueue<int>.CreateWithKey(v => hs[v], true);

		foreach (var (p, h) in ps)
		{
			hs[p] = h;
			q.Push(p);
		}

		while (q.Count > 0)
		{
			var (v, h) = q.Pop();
			if (hs[v] < h) continue;

			var nh = hs[v] - 1;
			foreach (var nv in map[v])
			{
				if (hs[nv] >= nh) continue;
				hs[nv] = nh;
				q.Push(nv);
			}
		}

		var r = Enumerable.Range(0, n + 1).Where(v => hs[v] >= 0).ToArray();
		return $"{r.Length}\n" + string.Join(" ", r);
	}

	public static List<int>[] ToListMap(int n, int[][] es, bool twoWay)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int>());
		foreach (var e in es)
		{
			map[e[0]].Add(e[1]);
			if (twoWay) map[e[1]].Add(e[0]);
		}
		return map;
	}
}
