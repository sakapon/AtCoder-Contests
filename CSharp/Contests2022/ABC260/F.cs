using System;
using System.Collections.Generic;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (s, t, m) = Read3();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var map = ToMap(s + t + 1, es, false);
		var u = Array.ConvertAll(new bool[s + t + 1], _ => new Dictionary<int, int>());

		for (int v = 1; v <= s; v++)
		{
			var to = map[v];

			for (int i = 0; i < to.Length; i++)
			{
				for (int j = i + 1; j < to.Length; j++)
				{
					var (a, b) = (to[i], to[j]);
					if (a > b) (a, b) = (b, a);

					if (u[a].ContainsKey(b))
					{
						return $"{v} {a} {b} {u[a][b]}";
					}
					else
					{
						u[a][b] = v;
					}
				}
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
}
