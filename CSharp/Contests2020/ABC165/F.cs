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
		var lis = new int[n];
		var history = new Stack<(int i, int x)>();
		Array.Fill(r, -1);
		Array.Fill(lis, max);
		DFS(root, -1);

		return string.Join("\n", r[1..]);

		void DFS(int v, int pv)
		{
			var x = a[v - 1];
			var i = First(0, n, i => lis[i] >= x);
			history.Push((i, lis[i]));
			lis[i] = x;
			r[v] = First(0, n, i => lis[i] == max);

			foreach (var nv in map[v])
			{
				if (nv == pv) continue;
				DFS(nv, v);
			}

			var (i0, x0) = history.Pop();
			lis[i0] = x0;
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
