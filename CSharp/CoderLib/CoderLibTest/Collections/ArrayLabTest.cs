﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Collections
{
	[TestClass]
	public class ArrayLabTest
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
			Assert.AreEqual(32, DotProduct2(new[] { 1, 2, 3 }, new[] { 4, 5, 6 }));
			Assert.AreEqual(89, DotProduct2(new[] { 2, 3, 5, 7 }, new[] { 11, 7, 5, 3 }));
		}
		#endregion
	}
}
