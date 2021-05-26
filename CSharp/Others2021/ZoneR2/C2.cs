using System;
using System.Linq;
using System.Numerics;

class C2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var map = EdgesToMap1(n, es, false);

		var max = 0;
		int[] r = null;
		Combination(Enumerable.Range(0, n).ToArray(), 3, p =>
		{
			var f = map[p[0]] | map[p[1]] | map[p[2]];
			var c = BitOperations.PopCount(f);

			if (max < c)
			{
				max = c;
				r = (int[])p.Clone();
			}
		});
		return string.Join(" ", r);
	}

	public static ulong[] EdgesToMap1(int n, int[][] es, bool directed)
	{
		var map = new ulong[n];
		foreach (var e in es)
		{
			map[e[0]] |= 1UL << e[1];
			if (!directed) map[e[1]] |= 1UL << e[0];
		}
		return map;
	}

	public static void Combination<T>(T[] values, int r, Action<T[]> action)
	{
		var n = values.Length;
		var p = new T[r];

		if (r > 0) Dfs(0, 0);
		else action(p);

		void Dfs(int i, int j0)
		{
			var i2 = i + 1;
			for (int j = j0; j < n; ++j)
			{
				p[i] = values[j];

				if (i2 < r) Dfs(i2, j + 1);
				else action(p);
			}
		}
	}
}
