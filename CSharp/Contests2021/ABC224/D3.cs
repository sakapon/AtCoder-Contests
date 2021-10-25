using System;
using System.Collections.Generic;
using System.Linq;

class D3
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var m = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[m], _ => Read());
		var p = Read();

		var p10 = Pows(10, 9);

		var sv = Enumerable.Range(0, 8).Sum(id => (id + 1) * p10[p[id] - 1]);
		var ev = 087654321;
		if (sv == ev) return 0;

		var r9 = Enumerable.Range(0, 9).ToArray();
		const int max = 1 << 30;

		var map = ToMap(9 + 1, es, false);
		var costs = new Dictionary<int, int>();

		Permutation(r9, 9, p =>
		{
			costs[r9.Sum(pi => p[pi] * p10[pi])] = max;
		});

		int[] Nexts(int v)
		{
			var p0 = r9.First(pi => v / p10[pi] % 10 == 0) + 1;

			return Array.ConvertAll(map[p0], p2 =>
			{
				var id = v / p10[p2 - 1] % 10;
				return v + id * (p10[p0 - 1] - p10[p2 - 1]);
			});
		}

		var q = new Queue<int>();
		costs[sv] = 0;
		q.Enqueue(sv);

		while (q.Count > 0)
		{
			var v = q.Dequeue();
			var nc = costs[v] + 1;

			foreach (var nv in Nexts(v))
			{
				if (costs[nv] <= nc) continue;
				costs[nv] = nc;
				if (nv == ev) return costs[ev];
				q.Enqueue(nv);
			}
		}

		return -1;
	}

	public static int[][] ToMap(int n, int[][] es, bool directed) => Array.ConvertAll(ToMapList(n, es, directed), l => l.ToArray());
	public static List<int>[] ToMapList(int n, int[][] es, bool directed)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int>());
		foreach (var e in es)
		{
			map[e[0]].Add(e[1]);
			if (!directed) map[e[1]].Add(e[0]);
		}
		return map;
	}

	public static void Permutation<T>(T[] values, int r, Action<T[]> action)
	{
		var n = values.Length;
		var p = new T[r];
		var u = new bool[n];

		if (r > 0) Dfs(0);
		else action(p);

		void Dfs(int i)
		{
			var i2 = i + 1;
			for (int j = 0; j < n; ++j)
			{
				if (u[j]) continue;
				p[i] = values[j];
				u[j] = true;

				if (i2 < r) Dfs(i2);
				else action(p);

				u[j] = false;
			}
		}
	}

	public static int[] Pows(int b, int n)
	{
		var p = new int[n + 1];
		p[0] = 1;
		for (int i = 0; i < n; ++i) p[i + 1] = p[i] * b;
		return p;
	}
}
