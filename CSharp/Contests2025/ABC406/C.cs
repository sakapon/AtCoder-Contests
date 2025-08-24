class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var p = Read();

		var r = 0L;
		var cs = Enumerable.Range(0, n - 1)
			.GroupCountsBySeq(i => p[i] < p[i + 1])
			.Where(p => p.Key)
			.Select(p => p.Value)
			.ToArray();

		for (int i = 1; i < cs.Length; i++)
			r += (long)cs[i - 1] * cs[i];
		return r;
	}
}

static class GE
{
	public static IEnumerable<KeyValuePair<TK, int>> GroupCountsBySeq<TS, TK>(this IEnumerable<TS> source, Func<TS, TK> toKey)
	{
		var c = EqualityComparer<TK>.Default;
		TK k = default(TK), kt;
		var count = 0;

		foreach (var o in source)
		{
			if (!c.Equals(k, kt = toKey(o)))
			{
				if (count > 0) yield return new KeyValuePair<TK, int>(k, count);
				k = kt;
				count = 0;
			}
			++count;
		}
		if (count > 0) yield return new KeyValuePair<TK, int>(k, count);
	}
}
