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

class OrderedList<T, TKey> : List<KeyValuePair<TKey, T>>
{
	static readonly Comparer<TKey> _comparer = Comparer<TKey>.Default;
	Func<T, TKey> _keySelector;

	public OrderedList(Func<T, TKey> keySelector)
	{
		_keySelector = keySelector;
	}

	public OrderedList(Func<T, TKey> keySelector, IEnumerable<T> collection) : base(collection.Select(o => new KeyValuePair<TKey, T>(keySelector(o), o)).OrderBy(p => p.Key))
	{
		_keySelector = keySelector;
	}

	public void AddForOrder(T o)
	{
		var p = new KeyValuePair<TKey, T>(_keySelector(o), o);
		Insert(Search(p.Key), p);
	}

	// 挿入先の番号を二分探索で求めます。値が重複する場合は後ろの番号です。
	int Search(TKey k) => Count > 0 ? Search(k, 0, Count) : 0;
	int Search(TKey k, int start, int count)
	{
		if (count == 1) return _comparer.Compare(k, this[start].Key) < 0 ? start : start + 1;
		var c = count / 2;
		var s = start + c;
		return _comparer.Compare(k, this[s].Key) < 0 ? Search(k, start, c) : Search(k, s, count - c);
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

	[TestMethod]
	public void AddForOrder_Key()
	{
		var values = new[] { "ddd", "a", "cc", "bb", "", "c" };
		var actual = new OrderedList<string, int>(s => s.Length);
		foreach (var v in values)
			actual.AddForOrder(v);
		CollectionAssert.AreEqual(values.OrderBy(s => s.Length).ToArray(), actual.Select(p => p.Value).ToArray());
	}
}
