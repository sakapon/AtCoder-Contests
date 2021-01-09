using System;
using System.Collections.Generic;
using System.Linq;

class C3
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0], m = h[1];
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var rv = 1;
		es = GetTree(n + 1, es, false, rv);
		var r = new int[n + 1];
		r[rv] = 1;

		TreeTour(n + 1, es, true, rv, e =>
		{
			if (e[2] == r[e[0]])
				r[e[1]] = e[2] % n + 1;
			else
				r[e[1]] = e[2];
		});
		Console.WriteLine(string.Join("\n", r.Skip(1)));
	}

	static int[][] GetTree(int n, int[][] es, bool directed, int rv)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
		foreach (var e in es)
		{
			map[e[0]].Add(new[] { e[0], e[1], e[2] });
			if (!directed) map[e[1]].Add(new[] { e[1], e[0], e[2] });
		}

		var res = new List<int[]>();
		var u = new bool[n];

		Action<int> Dfs = null;
		Dfs = v =>
		{
			u[v] = true;
			foreach (var e in map[v])
			{
				if (u[e[1]]) continue;
				res.Add(e);
				Dfs(e[1]);
			}
		};

		Dfs(rv);
		return res.ToArray();
	}

	static void TreeTour(int n, int[][] es, bool directed, int rv, Action<int[]> going = null, Action<int[]> backing = null)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
		foreach (var e in es)
		{
			map[e[0]].Add(new[] { e[0], e[1], e[2] });
			if (!directed) map[e[1]].Add(new[] { e[1], e[0], e[2] });
		}

		Action<int[]> Dfs = null;
		Dfs = pe =>
		{
			foreach (var e in map[pe[1]])
			{
				if (e[1] == pe[0]) continue;
				going?.Invoke(e);
				Dfs(e);
				backing?.Invoke(e);
			}
		};
		Dfs(new[] { -1, rv, -1 });
	}
}
