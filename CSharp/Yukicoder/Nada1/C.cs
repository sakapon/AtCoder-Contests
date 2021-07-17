using System;
using System.Linq;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => ReadL());

		var r = WarshallFloyd(n + 1, es, true).Item1;

		Console.WriteLine(string.Join("\n", r.Skip(1).Select(vs => vs.Where(v => v != long.MaxValue).Sum())));
	}

	public static Tuple<long[][], int[][]> WarshallFloyd(int n, long[][] es, bool directed)
	{
		var cs = Array.ConvertAll(new bool[n], i => Array.ConvertAll(new bool[n], _ => long.MaxValue));
		var inters = Array.ConvertAll(new bool[n], i => Array.ConvertAll(new bool[n], _ => -1));
		for (int i = 0; i < n; ++i) cs[i][i] = 0;

		foreach (var e in es)
		{
			cs[e[0]][e[1]] = Math.Min(cs[e[0]][e[1]], e[2]);
			if (!directed) cs[e[1]][e[0]] = Math.Min(cs[e[1]][e[0]], e[2]);
		}

		for (int k = 0; k < n; ++k)
			for (int i = 0; i < n; ++i)
				for (int j = 0; j < n; ++j)
				{
					if (cs[i][k] == long.MaxValue || cs[k][j] == long.MaxValue) continue;
					var nc = cs[i][k] + cs[k][j];
					if (cs[i][j] <= nc) continue;
					cs[i][j] = nc;
					inters[i][j] = k;
				}
		for (int i = 0; i < n; ++i) if (cs[i][i] < 0) return Tuple.Create<long[][], int[][]>(null, null);
		return Tuple.Create(cs, inters);
	}
}
