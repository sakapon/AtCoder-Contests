using CoderLib8.DataTrees;

static class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		const long max = 1L << 60;
		var map = ToWeightedMap(n + 1, es, false);

		var r = Dijkstra(n + 1, (v, c) =>
		{
			var t = max - c;

			var l = new List<(int, long)>();
			foreach (var e in map[v])
			{
				var k0 = Last(-1, e[2] - 1, x => e[0] + x * e[1] + e[3] <= t);
				if (k0 == -1) continue;
				var nt = e[0] + k0 * e[1];
				l.Add((e[4], t - nt));
			}
			return l.ToArray();
		}, n);

		return string.Join("\n", r[1..^1].Select(x => x == long.MaxValue ? "Unreachable" : $"{max - x}"));
	}

	static long Last(long l, long r, Func<long, bool> f)
	{
		long m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
	}

	public static T[][] ToArrays<T>(this List<T>[] map) => Array.ConvertAll(map, l => l.ToArray());

	public static int[][][] ToWeightedMap(int n, int[][] es, bool twoWay) => ToWeightedListMap(n, es, twoWay).ToArrays();
	public static List<int[]>[] ToWeightedListMap(int n, int[][] es, bool twoWay)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
		foreach (var e in es)
		{
			// 逆方向
			map[e[5]].Add(e);
		}
		return map;
	}

	public static long[] Dijkstra(int n, Func<int, long, (int to, long c)[]> nexts, int sv, int ev = -1)
	{
		var costs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
		var q = PriorityQueue<int>.CreateWithKey(v => costs[v]);
		costs[sv] = 0;
		q.Push(sv);

		while (q.Any)
		{
			var (v, c) = q.Pop();
			if (v == ev) break;
			if (costs[v] < c) continue;

			foreach (var (to, ec) in nexts(v, c))
			{
				var (nv, nc) = (to, c + ec);
				if (costs[nv] <= nc) continue;
				costs[nv] = nc;
				q.Push(nv);
			}
		}
		return costs;
	}
}
