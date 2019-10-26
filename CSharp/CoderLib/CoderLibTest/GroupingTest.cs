using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}

[TestClass]
public class GroupingTest
{
	[TestMethod]
	public void GroupBySeq_1()
	{
		var actual = Enumerable.Range(0, 9).GroupBySeq(i => i % 4 == 0).ToArray();

		CollectionAssert.AreEqual(new[] { true, false, true, false, true }, actual.Select(g => g.Key).ToArray());
		CollectionAssert.AreEqual(new[] { 1, 3, 1, 3, 1 }, actual.Select(g => g.Count()).ToArray());
	}

	[TestMethod]
	public void GroupBySeq_2()
	{
		var actual = Enumerable.Range(1, 11).GroupBySeq(i => i % 4 == 0).ToArray();

		CollectionAssert.AreEqual(new[] { false, true, false, true, false }, actual.Select(g => g.Key).ToArray());
		CollectionAssert.AreEqual(new[] { 3, 1, 3, 1, 3 }, actual.Select(g => g.Count()).ToArray());
	}

	[TestMethod]
	public void GroupBySeq_Empty()
	{
		var empty = new int[0];

		CollectionAssert.AreEqual(empty, empty.GroupBySeq(i => i).ToArray());
	}
}
