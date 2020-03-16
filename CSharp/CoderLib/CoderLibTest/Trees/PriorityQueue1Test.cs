using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Trees
{
	// 1-indexed
	class PQ1<T> : List<T>
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

		Comparison<T> c;
		public T First => this[1];
		public new int Count => base.Count - 1;
		PQ1(T[] vs, Comparison<T> _c)
		{
			c = _c;
			Add(default(T));
			if (vs != null) foreach (var v in vs) Push(v);
		}

		void Swap(int i, int j) { var o = this[i]; this[i] = this[j]; this[j] = o; }
		void UpHeap(int i) { for (int j; i > 1 && c(this[j = i / 2], this[i]) > 0; Swap(i, i = j)) ; }
		void DownHeap(int i)
		{
			for (int j; (j = 2 * i) < base.Count; i = j)
			{
				if (j + 1 < base.Count && c(this[j], this[j + 1]) > 0) j++;
				if (c(this[i], this[j]) > 0) Swap(i, j); else break;
			}
		}

		public void Push(T v)
		{
			Add(v);
			UpHeap(Count);
		}
		public T Pop()
		{
			var r = this[1];
			this[1] = this[Count];
			RemoveAt(Count);
			DownHeap(1);
			return r;
		}
	}

	[TestClass]
	public class PriorityQueue1Test
	{
		Random random = new Random();

		[TestMethod]
		public void Pop()
		{
			var values = Enumerable.Range(0, 100000).Select(i => random.Next(100000)).ToArray();
			var actual = PQ1<int>.Create(values);
			for (int v1 = actual.Pop(), v2; actual.Count > 0; v1 = v2)
			{
				v2 = actual.Pop();
				Assert.IsTrue(v1 <= v2);
			}
		}

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
