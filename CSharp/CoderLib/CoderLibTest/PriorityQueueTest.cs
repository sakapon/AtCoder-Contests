using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

class PQ<T> : List<T>
{
	Comparison<T> c;
	public T Current => this[0];

	public PQ(IEnumerable<T> vs = null, Comparison<T> compare = null)
	{
		c = compare ?? Comparer<T>.Default.Compare;
		if (vs != null) foreach (var v in vs) Push(v);
	}

	void Swap(int i, int j)
	{
		var t = this[i];
		this[i] = this[j];
		this[j] = t;
	}

	void UpHeap(int i) { for (var j = (i - 1) / 2; i > 0 && c(this[j], this[i]) > 0; Swap(i, j), i = j, j = (i - 1) / 2) ; }
	void DownHeap(int i)
	{
		for (var j = 2 * i + 1; j < Count; i = j, j = 2 * i + 1)
		{
			if (j + 1 < Count && c(this[j], this[j + 1]) > 0) j++;
			if (c(this[i], this[j]) > 0) Swap(i, j); else break;
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
	[TestMethod]
	public void Pop()
	{
		var random = new Random();
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
		var random = new Random();
		var values = Enumerable.Range(0, 100000).Select(i => random.Next(100000)).ToArray();
		var actual = new PQ<int>(values);
		var l = new List<int>();
		while (actual.Count > 0) l.Add(actual.Pop());
		CollectionAssert.AreEqual(values.OrderBy(x => x).ToArray(), l);
	}
}
