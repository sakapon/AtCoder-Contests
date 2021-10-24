using System;
using System.Collections.Generic;
using System.Linq;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var m = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[m], _ => Read());
		var p = Read();

		var svc = new int[9];
		for (int i = 0; i < 8; i++)
		{
			svc[p[i] - 1] = i + 1;
		}
		var sv = string.Join("", svc);

		var ev = "123456780";
		if (sv == ev) return 0;

		var r9 = Enumerable.Range(0, 9).ToArray();
		const int max = 1 << 30;

		var map = ToMap(9 + 1, es, false);
		var costs = new Dictionary<string, int>();

		Permutation(r9, 9, p =>
		{
			costs[string.Join("", p)] = max;
		});

		string[] Nexts(string ps)
		{
			var p = ps.Select(c => c - '0').ToArray();
			var nv = Array.IndexOf(p, 0) + 1;

			return Array.ConvertAll(map[nv], v2 =>
			{
				(p[v2 - 1], p[nv - 1]) = (p[nv - 1], p[v2 - 1]);
				var r = string.Join("", p);
				(p[v2 - 1], p[nv - 1]) = (p[nv - 1], p[v2 - 1]);
				return r;
			});
		}

		var q = new Queue<string>();
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
}
