using System;
using System.Collections.Generic;
using System.Linq;

class C
{
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var s = Console.ReadLine();
		var t = Console.ReadLine();

		var sg = s.GroupCountsBySeq(c => c).ToArray();
		var tg = t.GroupCountsBySeq(c => c).ToArray();

		if (!sg.Select(p => p.Key).SequenceEqual(tg.Select(p => p.Key))) return false;
		return sg.Select(p => p.Value).Zip(tg.Select(p => p.Value)).All(p => p.First == 1 ? p.Second == 1 : p.First <= p.Second);
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
