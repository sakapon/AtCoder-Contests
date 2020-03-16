using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Trees
{
	class PQ<T> : List<T>
	{
		public static PQ<T> Create(T[] vs = null, bool desc = false)
		{
			var c = Comparer<T>.Default;
			return desc ?
				new PQ<T>(vs, (x, y) => c.Compare(y, x)) :
				new PQ<T>(vs, c.Compare);
		}

		public static PQ<T> Create<TKey>(Func<T, TKey> getKey, T[] vs = null, bool desc = false)
		{
			var c = Comparer<TKey>.Default;
			return desc ?
				new PQ<T>(vs, (x, y) => c.Compare(getKey(y), getKey(x))) :
				new PQ<T>(vs, (x, y) => c.Compare(getKey(x), getKey(y)));
		}

		Comparison<T> c;
		public T First => this[0];

		PQ(T[] vs, Comparison<T> _c)
		{
			c = _c;
			if (vs != null) foreach (var v in vs) Push(v);
		}

		void Swap(int i, int j) { var o = this[i]; this[i] = this[j]; this[j] = o; }
		void UpHeap(int i) { for (int j; i > 0 && c(this[j = (i - 1) / 2], this[i]) > 0; Swap(i, i = j)) ; }
		void DownHeap(int i)
		{
			for (int j; (j = 2 * i + 1) < Count;)
			{
				if (j + 1 < Count && c(this[j], this[j + 1]) > 0) j++;
				if (c(this[i], this[j]) > 0) Swap(i, i = j); else break;
			}
		}

		public void Push(T v)
		{
			Add(v);
			UpHeap(Count - 1);
		}
		public T Pop()
		{
			var r = this[0];
			this[0] = this[Count - 1];
			RemoveAt(Count - 1);
			DownHeap(0);
			return r;
		}
	}

	[TestClass]
	public class PriorityQueueTest
	{
		Random random = new Random();

		[TestMethod]
		public void Sort()
		{
			var values = Enumerable.Range(0, 100000).Select(i => random.Next(100000)).ToArray();
			var actual = TestHelper.MeasureTime(() => PQ<int>.Create(values));
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
			var actual = TestHelper.MeasureTime(() => PQ<int>.Create(values, true));
			var a = new int[values.Length];
			TestHelper.MeasureTime(() => { for (var i = 0; i < a.Length; i++, actual.Pop()) a[i] = actual.First; });
			var e = TestHelper.MeasureTime(() => values.OrderByDescending(x => x).ToArray());
			CollectionAssert.AreEqual(e, a);
		}

		[TestMethod]
		public void SortDescending_String()
		{
			var values = Enumerable.Range(0, 100000).Select(i => random.Next(100000)).ToArray();
			var actual = TestHelper.MeasureTime(() => PQ<int>.Create(x => x.ToString(), values, true));
			var a = new List<int>();
			TestHelper.MeasureTime(() => { while (actual.Any()) a.Add(actual.Pop()); });
			var e = TestHelper.MeasureTime(() => values.OrderByDescending(x => x.ToString()).ToArray());
			CollectionAssert.AreEqual(e, a);
		}

		// 少しだけ取り出す場合、PQ を使うほうが速いです。
		[TestMethod]
		public void SortTake()
		{
			var values = Enumerable.Range(0, 100000).Select(i => random.Next(100000)).ToArray();
			var actual = TestHelper.MeasureTime(() =>
			{
				var pq = PQ<int>.Create(values);
				var a = new int[100];
				for (var i = 0; i < a.Length; i++) a[i] = pq.Pop();
				return a;
			});
			var expected = TestHelper.MeasureTime(() => values.OrderBy(x => x).Take(100).ToArray());
			CollectionAssert.AreEqual(expected, actual);
		}
	}
}
