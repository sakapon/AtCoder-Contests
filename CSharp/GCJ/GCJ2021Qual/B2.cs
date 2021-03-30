using System;
using System.Collections.Generic;
using System.Linq;

static class B2
{
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select((_, i) => $"Case #{i + 1}: {Solve()}")));
	static object Solve()
	{
		var h = Console.ReadLine().Split();
		var x = int.Parse(h[0]);
		var y = int.Parse(h[1]);
		var s = h[2];

		var gs = s.Where(c => c != '?').GroupCountsBySeq(c => c).ToArray();
		var count = gs.Length - 1;
		if (count <= 0) return 0;

		var c2 = count / 2;
		var c1 = count - c2;

		if (gs[0].Key == 'J') (x, y) = (y, x);
		return c1 * x + c2 * y;
	}

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
