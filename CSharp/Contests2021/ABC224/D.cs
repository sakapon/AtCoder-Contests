using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var m = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[m], _ => Read());
		var sv = Console.ReadLine().Replace(" ", "");

		var ev = "12345678";
		if (sv == ev) return 0;

		var r9 = Enumerable.Range(1, 9).ToArray();
		const int max = 1 << 30;

		var map = ToMap(9 + 1, es, false);
		var costs = new Dictionary<string, int>();

		Permutation(r9, 8, p =>
		{
			costs[string.Join("", p)] = max;
		});

		string[] Nexts(string ps)
		{
			var p = ps.Select(c => c - '0').ToArray();
			var nv = r9.Except(p).First();

			return Array.ConvertAll(map[nv], v2 =>
			{
				for (int i = 0; i < 8; i++)
				{
					if (p[i] == v2)
					{
						p[i] = nv;
						var r = string.Join("", p);
						p[i] = v2;
						return r;
					}
				}
				throw new InvalidOperationException();
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
