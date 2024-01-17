using CoderLib8.Values;

class F3
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w) = Read2();
		var s = Array.ConvertAll(new bool[h], _ => Read());

		var sv = new EquatableArray<byte>(s.SelectMany(x => x).Select(x => (byte)x).ToArray());
		var ev = new EquatableArray<byte>(Enumerable.Range(1, h * w).Select(x => (byte)x).ToArray());

		var d1 = ShortestByBFS(GetNexts, sv, null, 10);
		var d2 = ShortestByBFS(GetNexts, ev, null, 10);

		var vs = d1.Keys.ToHashSet();
		vs.IntersectWith(d2.Keys);
		if (vs.Count == 0) return -1;
		return vs.Min(v => d1[v] + d2[v]);

		EquatableArray<byte>[] GetNexts(EquatableArray<byte> v)
		{
			return new[]
			{
				GetNext(v, 0, 0),
				GetNext(v, 0, 1),
				GetNext(v, 1, 0),
				GetNext(v, 1, 1),
			};
		}

		EquatableArray<byte> GetNext(EquatableArray<byte> v, int si, int sj)
		{
			var nv = v.Clone();
			for (int i = 0; i < h - 1; ++i)
				for (int j = 0; j < w - 1; ++j)
					nv[(si + i) * w + sj + j] = v[(si + h - 2 - i) * w + sj + w - 2 - j];
			return nv;
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
