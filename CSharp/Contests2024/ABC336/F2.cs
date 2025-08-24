using CoderLib8.Values;

class F2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var s = Array.ConvertAll(new bool[h], _ => Read());

		var sv = new Grid2<byte>(h, w, s.SelectMany(x => x).Select(x => (byte)x).ToArray());
		var ev = new Grid2<byte>(h, w, Enumerable.Range(1, h * w).Select(x => (byte)x).ToArray());

		var d1 = ShortestByBFS(GetNexts, sv, null, 10);
		var d2 = ShortestByBFS(GetNexts, ev, null, 10);

		var vs = d1.Keys.ToHashSet();
		vs.IntersectWith(d2.Keys);
		if (vs.Count == 0) return -1;
		return vs.Min(v => d1[v] + d2[v]);

		Grid2<byte>[] GetNexts(Grid2<byte> v)
		{
			var l = new List<Grid2<byte>>();

			var nv = v.Clone();
			for (int i = 0; i < h - 1; ++i)
				for (int j = 0; j < w - 1; ++j)
					nv[i, j] = v[h - 2 - i, w - 2 - j];
			l.Add(nv);

			nv = v.Clone();
			for (int i = 0; i < h - 1; ++i)
				for (int j = 1; j < w; ++j)
					nv[i, j] = v[h - 2 - i, w - j];
			l.Add(nv);

			nv = v.Clone();
			for (int i = 1; i < h; ++i)
				for (int j = 0; j < w - 1; ++j)
					nv[i, j] = v[h - i, w - 2 - j];
			l.Add(nv);

			nv = v.Clone();
			for (int i = 1; i < h; ++i)
				for (int j = 1; j < w; ++j)
					nv[i, j] = v[h - i, w - j];
			l.Add(nv);

			return l.ToArray();
		}
	}

	public static Dictionary<T, long> ShortestByBFS<T>(Func<T, T[]> nexts, T sv, T ev, long maxCost = long.MaxValue)
	{
		var costs = new Dictionary<T, long>();
		var q = new Queue<T>();
		costs[sv] = 0;
		q.Enqueue(sv);

		while (q.Count > 0)
		{
			var v = q.Dequeue();
			if (costs.Comparer.Equals(v, ev)) return costs;
			var nc = costs[v] + 1;
			if (nc > maxCost) return costs;

			foreach (var nv in nexts(v))
			{
				if (costs.ContainsKey(nv)) continue;
				costs[nv] = nc;
				q.Enqueue(nv);
			}
		}
		return costs;
	}
}
