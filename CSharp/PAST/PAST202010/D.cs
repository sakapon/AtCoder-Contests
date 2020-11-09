using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();

		var gs = s.GroupBySeq(c => c).ToList();

		var l0 = 0;
		if (gs[0].Key == '.')
		{
			l0 = gs[0].Count();
			gs.RemoveAt(0);
		}
		var r0 = 0;
		if (gs.Last().Key == '.')
		{
			r0 = gs.Last().Count();
			gs.RemoveAt(gs.Count - 1);
		}

		var lrgs = gs.Where(g => g.Key == '.').ToArray();
		var lr = lrgs.Any() ? Math.Max(0, lrgs.Max(g => g.Count()) - l0 - r0) : 0;

		Console.WriteLine($"{l0 + lr} {r0}");
	}
}

static class GE
{
	public static IEnumerable<IGrouping<TK, TS>> GroupBySeq<TS, TK>(this IEnumerable<TS> source, Func<TS, TK> toKey)
	{
		var c = EqualityComparer<TK>.Default;
		var k = default(TK);
		var l = new List<TS>();

		foreach (var o in source)
		{
			var kt = toKey(o);
			if (!c.Equals(kt, k))
			{
				if (l.Count > 0) yield return new G<TK, TS>(k, l.ToArray());
				k = kt;
				l.Clear();
			}
			l.Add(o);
		}
		if (l.Count > 0) yield return new G<TK, TS>(k, l.ToArray());
	}

	class G<TK, TE> : IGrouping<TK, TE>
	{
		public TK Key { get; }
		IEnumerable<TE> Values;
		public G(TK key, TE[] values) { Key = key; Values = values; }

		public IEnumerator<TE> GetEnumerator() => Values.GetEnumerator();
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
	}
}
