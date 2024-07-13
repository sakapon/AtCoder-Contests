class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var a = Read();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var map = ToWeightedListMap(n + 1, es, true);

		var r = Dijkstra(n + 1, v => map[v].ToArray(), 1);
		return string.Join(" ", r[2..]);

		long[] Dijkstra(int n, Func<int, int[][]> nexts, int sv, int ev = -1)
		{
			var costs = Array.ConvertAll(new bool[n], _ => long.MaxValue);
			var q = CoderLib8.DataTrees.PriorityQueue<int>.CreateWithKey(v => costs[v]);
			costs[sv] = a[sv - 1];
			q.Push(sv);

			while (q.Any)
			{
				var (v, c) = q.Pop();
				if (v == ev) break;
				if (costs[v] < c) continue;

				foreach (var e in nexts(v))
				{
					var (nv, nc) = (e[1], c + e[2] + a[e[1] - 1]);
					if (costs[nv] <= nc) continue;
					costs[nv] = nc;
					q.Push(nv);
				}
			}
			return costs;
		}
	}

	public static List<int[]>[] ToWeightedListMap(int n, int[][] es, bool twoWay)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
		foreach (var e in es)
		{
			map[e[0]].Add(e);
			if (twoWay) map[e[1]].Add(new[] { e[1], e[0], e[2] });
		}
		return map;
	}
}
