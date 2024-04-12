static class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n - 1], _ => Read());
		var c = ReadL().Prepend(0).ToArray();

		var map = ToMap(n + 1, es, true);
		var r = new long[n + 1];

		var rv = 1;
		DFS1(rv, -1);
		DFS2(rv, -1, 0, 0);
		return r[1..].Min();

		void DFS1(int v, int pv)
		{
			foreach (var nv in map[v])
			{
				if (nv == pv) continue;
				DFS1(nv, v);
				c[v] += c[nv];
				r[v] += r[nv] + c[nv];
			}
		}

		void DFS2(int v, int pv, long s1, long s2)
		{
			var t = r[v];
			r[v] += s1 + s2;

			foreach (var nv in map[v])
			{
				if (nv == pv) continue;
				DFS2(nv, v, s1 + c[v] - c[nv], s1 + s2 + t - r[nv] - c[nv]);
			}
		}
	}

	public static T[][] ToArrays<T>(this List<T>[] map) => Array.ConvertAll(map, l => l.ToArray());
	public static int[][] ToMap(int n, int[][] es, bool twoWay) => ToListMap(n, es, twoWay).ToArrays();
	public static List<int>[] ToListMap(int n, int[][] es, bool twoWay)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int>());
		foreach (var e in es)
		{
			map[e[0]].Add(e[1]);
			if (twoWay) map[e[1]].Add(e[0]);
		}
		return map;
	}
}
