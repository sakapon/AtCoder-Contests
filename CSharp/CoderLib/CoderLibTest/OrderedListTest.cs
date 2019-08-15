using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

class OrderedList : List<int>
{
	public OrderedList() { }
	public OrderedList(IEnumerable<int> collection) : base(collection.OrderBy(x => x)) { }

	public void AddForOrder(int v) => Insert(Search(v), v);

	// 挿入先の番号を二分探索で求めます。値が重複する場合は後ろの番号です。
	int Search(int v) => Count > 0 ? Search(v, 0, Count) : 0;
	int Search(int v, int start, int count)
	{
		if (count == 1) return start + (v < this[start] ? 0 : 1);
		var c = count / 2;
		var s = start + c;
		return v < this[s] ? Search(v, start, c) : Search(v, s, count - c);
	}
}

[TestClass]
public class OrderedListTest
{
	[TestMethod]
	public void Ctor()
	{
		var random = new Random();
		var values = Enumerable.Range(0, 1000000).Select(i => random.Next(1000000)).ToArray();
		var actual = new OrderedList(values);
		CollectionAssert.AreEqual(values.OrderBy(x => x).ToArray(), actual);
	}

	[TestMethod]
	public void AddForOrder()
	{
		var random = new Random();
		var values = Enumerable.Range(0, 100000).Select(i => random.Next(100000)).ToArray();
		var actual = new OrderedList();
		foreach (var v in values)
			actual.AddForOrder(v);
		CollectionAssert.AreEqual(values.OrderBy(x => x).ToArray(), actual);
	}
}
