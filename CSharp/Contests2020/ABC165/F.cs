using System;
using System.Collections.Generic;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();
		var es = Array.ConvertAll(new bool[n - 1], _ => Read());

		const int max = 1 << 30;
		var root = 1;
		var map = ToMap(n + 1, es, true);

		var r = new int[n + 1];
		var lis = new List<int>();
		DFS(root);
		return string.Join("\n", r[1..]);

		void DFS(int v)
		{
			var x = a[v - 1];
			var i = First(0, lis.Count, i => lis[i] >= x);
			var x0 = 0;
			if (i < lis.Count)
			{
				x0 = lis[i];
				lis[i] = x;
			}
			else
			{
				x0 = max;
				lis.Add(x);
			}
			r[v] = lis.Count;

			foreach (var nv in map[v])
			{
				if (r[nv] != 0) continue;
				DFS(nv);
			}

			if (x0 != max)
			{
				lis[i] = x0;
			}
			else
			{
				lis.RemoveAt(i);
			}
		}
	}

	public static int[][] ToMap(int n, int[][] es, bool twoWay)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int>());
		foreach (var e in es)
		{
			map[e[0]].Add(e[1]);
			if (twoWay) map[e[1]].Add(e[0]);
		}
		return Array.ConvertAll(map, l => l.ToArray());
	}

	static int First(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
		return r;
	}
}
