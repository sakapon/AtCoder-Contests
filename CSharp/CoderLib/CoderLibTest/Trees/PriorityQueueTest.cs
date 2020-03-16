using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

class PQ<T> : List<T>
{
	public static PQ<T> CreateDesc(T[] vs)
	{
		var c = Comparer<T>.Default;
		return new PQ<T>(vs, (x, y) => c.Compare(y, x));
	}

	public static PQ<T> Create<TKey>(T[] vs, Func<T, TKey> getKey, bool desc = false)
	{
		var c = Comparer<TKey>.Default;
		return desc ?
			new PQ<T>(vs, (x, y) => c.Compare(getKey(y), getKey(x))) :
			new PQ<T>(vs, (x, y) => c.Compare(getKey(x), getKey(y)));
	}

	Comparison<T> _c;
	public T First => this[0];

	public PQ(T[] vs = null, Comparison<T> c = null)
	{
		_c = c ?? Comparer<T>.Default.Compare;
		if (vs != null) foreach (var v in vs) Push(v);
	}

	void Swap(int i, int j) { var o = this[i]; this[i] = this[j]; this[j] = o; }
	void UpHeap(int i) { for (int j; i > 0 && _c(this[j = (i - 1) / 2], this[i]) > 0; Swap(i, j), i = j) ; }
	void DownHeap(int i)
	{
		for (int j; (j = 2 * i + 1) < Count; i = j)
		{
			if (j + 1 < Count && _c(this[j], this[j + 1]) > 0) j++;
			if (_c(this[i], this[j]) > 0) Swap(i, j); else break;
		}
	}

	public void Push(T v) { Add(v); UpHeap(Count - 1); }
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
	public void Pop()
	{
		var values = Enumerable.Range(0, 100000).Select(i => random.Next(100000)).ToArray();
		var actual = new PQ<int>(values);
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
		var actual = TestHelper.MeasureTime(() => new PQ<int>(values));
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
		var actual = TestHelper.MeasureTime(() => PQ<int>.CreateDesc(values));
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
			var pq = new PQ<int>(values);
			var a = new int[100];
			for (var i = 0; i < a.Length; i++) a[i] = pq.Pop();
			return a;
		});
		var expected = TestHelper.MeasureTime(() => values.OrderBy(x => x).Take(100).ToArray());
		CollectionAssert.AreEqual(expected, actual);
	}
}
