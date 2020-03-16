﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Trees
{
	// 1-indexed
	class PQ1<T>
	{
		public static PQ1<T> Create(T[] vs = null, bool desc = false)
		{
			var c = Comparer<T>.Default;
			return desc ?
				new PQ1<T>(vs, (x, y) => c.Compare(y, x)) :
				new PQ1<T>(vs, c.Compare);
		}

		public static PQ1<T> Create<TKey>(Func<T, TKey> getKey, T[] vs = null, bool desc = false)
		{
			var c = Comparer<TKey>.Default;
			return desc ?
				new PQ1<T>(vs, (x, y) => c.Compare(getKey(y), getKey(x))) :
				new PQ1<T>(vs, (x, y) => c.Compare(getKey(x), getKey(y)));
		}

		List<T> l = new List<T> { default(T) };
		Comparison<T> c;
		public T First => l[1];
		public int Count => l.Count - 1;

		PQ1(T[] vs, Comparison<T> _c)
		{
			c = _c;
			if (vs != null) foreach (var v in vs) Push(v);
		}

		void Swap(int i, int j) { var o = l[i]; l[i] = l[j]; l[j] = o; }
		void UpHeap(int i) { for (int j; (j = i / 2) > 0 && c(l[j], l[i]) > 0; Swap(i, i = j)) ; }
		void DownHeap(int i)
		{
			for (int j; (j = 2 * i) < l.Count;)
			{
				if (j + 1 < l.Count && c(l[j], l[j + 1]) > 0) j++;
				if (c(l[i], l[j]) > 0) Swap(i, i = j); else break;
			}
		}

		public void Push(T v)
		{
			l.Add(v);
			UpHeap(Count);
		}
		public T Pop()
		{
			var r = l[1];
			l[1] = l[Count];
			l.RemoveAt(Count);
			DownHeap(1);
			return r;
		}
	}

	[TestClass]
	public class PriorityQueue1Test
	{
		Random random = new Random();

		[TestMethod]
		public void Sort()
		{
			var values = Enumerable.Range(0, 100000).Select(i => random.Next(100000)).ToArray();
			var actual = TestHelper.MeasureTime(() => PQ1<int>.Create(values));
			var a = new int[values.Length];
			TestHelper.MeasureTime(() => { for (var i = 0; i < a.Length; i++) a[i] = actual.Pop(); });
			var e = (int[])values.Clone();
			TestHelper.MeasureTime(() => Array.Sort(e));
			CollectionAssert.AreEqual(e, a);
		}

		[TestMethod]
		public void SortDescending()
		{
			var values = Enumerable.Range(0, 100000).Select(i => random.Next(100000)).ToArray();
			var actual = TestHelper.MeasureTime(() => PQ1<int>.Create(values, true));
			var a = new int[values.Length];
			TestHelper.MeasureTime(() => { for (var i = 0; i < a.Length; i++) a[i] = actual.Pop(); });
			var e = TestHelper.MeasureTime(() => values.OrderByDescending(x => x).ToArray());
			CollectionAssert.AreEqual(e, a);
		}

		// 少しだけ取り出す場合、PQ を使うほうが速いです。
		[TestMethod]
		public void SortTake()
		{
			var values = Enumerable.Range(0, 100000).Select(i => random.Next(100000)).ToArray();
			var actual = TestHelper.MeasureTime(() =>
			{
				var pq = PQ1<int>.Create(values);
				var a = new int[100];
				for (var i = 0; i < a.Length; i++) a[i] = pq.Pop();
				return a;
			});
			var expected = TestHelper.MeasureTime(() => values.OrderBy(x => x).Take(100).ToArray());
			CollectionAssert.AreEqual(expected, actual);
		}
	}
}
