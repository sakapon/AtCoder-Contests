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

		var map = ToMapList(s + t + 1, es, false);
		var u = NewArray2<int>(t + 1, t + 1);

		for (int v = 1; v <= s; v++)
		{
			var to = map[v];
			for (int i = 0; i < to.Count; i++)
			{
				var a = to[i] - s;
				for (int j = i + 1; j < to.Count; j++)
				{
					var b = to[j] - s;

					if (u[a][b] == 0)
					{
						u[a][b] = u[b][a] = v;
					}
					else
					{
						return $"{v} {to[i]} {to[j]} {u[a][b]}";
					}
				}
			}
		}
		return -1;
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));

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
