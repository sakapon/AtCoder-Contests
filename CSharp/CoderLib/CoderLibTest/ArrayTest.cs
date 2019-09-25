using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class ArrayTest
{
	static void Swap<T>(T[] a, int i, int j) { var o = a[i]; a[i] = a[j]; a[j] = o; }

	static int[] Range(int start, int count)
	{
		var a = new int[count];
		for (var i = 0; i < count; i++) a[i] = start++;
		return a;
	}

	static int[] FindIndexes<T>(T[] a, Func<T, bool> match)
	{
		var l = new List<int>();
		for (var i = 0; i < a.Length; i++) if (match(a[i])) l.Add(i);
		return l.ToArray();
	}

	static KeyValuePair<int, T>[] FindWithIndex<T>(T[] a, Func<T, bool> match)
	{
		var l = new List<KeyValuePair<int, T>>();
		for (var i = 0; i < a.Length; i++) if (match(a[i])) l.Add(new KeyValuePair<int, T>(i, a[i]));
		return l.ToArray();
	}

	static KeyValuePair<int, int> MaxWithIndex(IList<int> a)
	{
		if (a.Count == 0) return new KeyValuePair<int, int>(-1, int.MinValue);
		int k = 0, m = a[0];
		for (var i = 1; i < a.Count; i++) if (a[i] > m) { k = i; m = a[i]; }
		return new KeyValuePair<int, int>(k, m);
	}

	static KeyValuePair<int, int> MinWithIndex(IList<int> a)
	{
		if (a.Count == 0) return new KeyValuePair<int, int>(-1, int.MaxValue);
		int k = 0, m = a[0];
		for (var i = 1; i < a.Count; i++) if (a[i] < m) { k = i; m = a[i]; }
		return new KeyValuePair<int, int>(k, m);
	}

	static KeyValuePair<int, int>[] MaxWithIndexes(IList<int> a)
	{
		if (a.Count == 0) return new KeyValuePair<int, int>[0];
		var l = new List<KeyValuePair<int, int>>();
		var m = a[0];
		l.Add(new KeyValuePair<int, int>(0, m));
		for (var i = 1; i < a.Count; i++)
			if (a[i] > m)
			{
				m = a[i];
				l.Clear();
				l.Add(new KeyValuePair<int, int>(i, m));
			}
			else if (a[i] == m)
				l.Add(new KeyValuePair<int, int>(i, m));
		return l.ToArray();
	}

	static KeyValuePair<int, int>[] MinWithIndexes(IList<int> a)
	{
		if (a.Count == 0) return new KeyValuePair<int, int>[0];
		var l = new List<KeyValuePair<int, int>>();
		var m = a[0];
		l.Add(new KeyValuePair<int, int>(0, m));
		for (var i = 1; i < a.Count; i++)
			if (a[i] < m)
			{
				m = a[i];
				l.Clear();
				l.Add(new KeyValuePair<int, int>(i, m));
			}
			else if (a[i] == m)
				l.Add(new KeyValuePair<int, int>(i, m));
		return l.ToArray();
	}

	static int DotProduct(int[] a, int[] b)
	{
		var r = 0;
		for (var i = 0; i < a.Length; i++) r += a[i] * b[i];
		return r;
	}

	static int DotProduct2(int[] a, int[] b) { for (int r = 0, i = -1; ; r += a[i] * b[i]) if (++i >= a.Length) return r; }

	static int[] SlideMins(int[] a, int k)
	{
		var r = new int[a.Length - k + 1];
		var q = new List<int>();

		for (int i = 0, j = i - k + 1; i < a.Length; i++, j++)
		{
			while (q.Count > 0 && a[q[q.Count - 1]] >= a[i]) q.RemoveAt(q.Count - 1);
			q.Add(i);

			if (j < 0) continue;
			r[j] = a[q[0]];
			if (q[0] == j) q.RemoveAt(0);
		}
		return r;
	}

	#region Test Methods

	[TestMethod]
	public void Range()
	{
		CollectionAssert.AreEqual(new[] { 0, 1, 2, 3, 4 }, Range(0, 5));
		CollectionAssert.AreEqual(new[] { 2, 3, 4, 5, 6 }, Range(2, 5));
	}

	[TestMethod]
	public void FindIndexes()
	{
		CollectionAssert.AreEqual(new[] { 1, 4 }, FindIndexes(new[] { 1, 2, 3, 4, 5 }, x => x % 3 == 2));
		CollectionAssert.AreEqual(new[] { 0, 2 }, FindIndexes(new[] { "ab", "abc", "cd", "a" }, x => x.Length == 2));
	}

	[TestMethod]
	public void FindWithIndex()
	{
		CollectionAssert.AreEqual(new[] { 1, 4 }, FindWithIndex(new[] { 1, 2, 3, 4, 5 }, x => x % 3 == 2).Select(p => p.Key).ToArray());
		CollectionAssert.AreEqual(new[] { 2, 5 }, FindWithIndex(new[] { 1, 2, 3, 4, 5 }, x => x % 3 == 2).Select(p => p.Value).ToArray());
		CollectionAssert.AreEqual(new[] { 0, 2 }, FindWithIndex(new[] { "ab", "abc", "cd", "a" }, x => x.Length == 2).Select(p => p.Key).ToArray());
		CollectionAssert.AreEqual(new[] { "ab", "cd" }, FindWithIndex(new[] { "ab", "abc", "cd", "a" }, x => x.Length == 2).Select(p => p.Value).ToArray());
	}

	[TestMethod]
	public void MaxWithIndex()
	{
		var mi = MaxWithIndex(new[] { 2, 4, 1, 5, 3 });
		Assert.AreEqual(3, mi.Key); Assert.AreEqual(5, mi.Value);
		var mi2 = MaxWithIndex(new[] { 1, 1, 3, 5, 5 });
		Assert.AreEqual(3, mi2.Key); Assert.AreEqual(5, mi2.Value);
	}

	[TestMethod]
	public void MinWithIndex()
	{
		var mi = MinWithIndex(new[] { 2, 4, 1, 5, 3 });
		Assert.AreEqual(2, mi.Key); Assert.AreEqual(1, mi.Value);
		var mi2 = MinWithIndex(new[] { 1, 1, 3, 5, 5 });
		Assert.AreEqual(0, mi2.Key); Assert.AreEqual(1, mi2.Value);
	}

	[TestMethod]
	public void MaxWithIndexes()
	{
		var mi = MaxWithIndexes(new[] { 2, 4, 1, 5, 3 });
		Assert.AreEqual(3, mi[0].Key); Assert.AreEqual(5, mi[0].Value);
		var mi2 = MaxWithIndexes(new[] { 1, 1, 3, 5, 5 });
		Assert.AreEqual(3, mi2[0].Key); Assert.AreEqual(5, mi2[0].Value);
		Assert.AreEqual(4, mi2[1].Key); Assert.AreEqual(5, mi2[1].Value);
	}

	[TestMethod]
	public void MinWithIndexes()
	{
		var mi = MinWithIndexes(new[] { 2, 4, 1, 5, 3 });
		Assert.AreEqual(2, mi[0].Key); Assert.AreEqual(1, mi[0].Value);
		var mi2 = MinWithIndexes(new[] { 1, 1, 3, 5, 5 });
		Assert.AreEqual(0, mi2[0].Key); Assert.AreEqual(1, mi2[0].Value);
		Assert.AreEqual(1, mi2[1].Key); Assert.AreEqual(1, mi2[1].Value);
	}

	[TestMethod]
	public void DotProduct()
	{
		Assert.AreEqual(32, DotProduct(new[] { 1, 2, 3 }, new[] { 4, 5, 6 }));
		Assert.AreEqual(89, DotProduct(new[] { 2, 3, 5, 7 }, new[] { 11, 7, 5, 3 }));
	}

	[TestMethod]
	public void SlideMins()
	{
		CollectionAssert.AreEqual(new[] { 5, 6, 7 }, SlideMins(new[] { 5, 6, 7, 8, 9 }, 3));
		CollectionAssert.AreEqual(new[] { 7, 6, 5 }, SlideMins(new[] { 9, 8, 7, 6, 5 }, 3));
		CollectionAssert.AreEqual(new[] { 6, 5, 5 }, SlideMins(new[] { 7, 6, 8, 5, 9 }, 3));
		CollectionAssert.AreEqual(new[] { 6, 6, 5 }, SlideMins(new[] { 7, 8, 6, 9, 5 }, 3));
		CollectionAssert.AreEqual(new[] { 5, 5, 5 }, SlideMins(new[] { 9, 7, 5, 6, 8 }, 3));
	}
	#endregion
}
