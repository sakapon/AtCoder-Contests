using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class ArrayTest
{
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

	static int DotProduct(int[] a, int[] b)
	{
		var r = 0;
		for (var i = 0; i < a.Length; i++) r += a[i] * b[i];
		return r;
	}

	static int DotProduct2(int[] a, int[] b) { for (int r = 0, i = -1; ; r += a[i] * b[i]) if (++i >= a.Length) return r; }

	#region Test Methods

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
	public void DotProduct()
	{
		Assert.AreEqual(32, DotProduct(new[] { 1, 2, 3 }, new[] { 4, 5, 6 }));
		Assert.AreEqual(89, DotProduct(new[] { 2, 3, 5, 7 }, new[] { 11, 7, 5, 3 }));
	}
	#endregion
}
