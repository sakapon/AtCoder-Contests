using System;
using System.Collections.Generic;
using System.Linq;

namespace CoderLib8.Linq
{
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
}
