using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		var x = s.IndexOf('#');
		var y = n - 1 - s.LastIndexOf('#');

		var q = s.GroupCountsBySeq(c => c).Where(p => p.Key == '.').ToArray();
		var all = Math.Max(x + y, q.Any() ? q.Max(p => p.Value) : 0);
		Console.WriteLine($"{x} {all - x}");
	}
}

static class GE
{
	public static Dictionary<TK, int> GroupCounts<TS, TK>(this IEnumerable<TS> source, Func<TS, TK> toKey)
	{
		var d = new Dictionary<TK, int>();
		TK k;
		foreach (var o in source)
			if (d.ContainsKey(k = toKey(o))) ++d[k];
			else d[k] = 1;
		return d;
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
