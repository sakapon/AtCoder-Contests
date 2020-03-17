using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoderLibTest.Collections
{
	class OrderedList : List<int>
	{
		public OrderedList() { }
		public OrderedList(IEnumerable<int> collection) : base(collection.OrderBy(x => x)) { }

		public void AddForOrder(int v)
		{
			// 値が重複する場合は順序が保証されませんが、int 型のため問題ありません。
			var i = BinarySearch(v);
			Insert(i < 0 ? ~i : i, v);
		}

		public int Dequeue()
		{
			var r = this[0];
			RemoveAt(0);
			return r;
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

		// 挿入先の番号を求めます。値が重複する場合は最後尾に挿入するときの番号です。すべて正の値です。
		int Search(TKey k)
		{
			int l = 0, r = Count, m;
			while (l < r) if (_comparer.Compare(this[m = l + (r - l - 1) / 2].Key, k) > 0) r = m; else l = m + 1;
			return r;
		}
	}

	[TestClass]
	public class OrderedListTest
	{
		[TestMethod]
		public void Sort0()
		{
			var random = new Random();
			var values = Enumerable.Range(0, 100000).Select(i => random.Next(100000)).ToArray();
			var actual = new List<int>();
			foreach (var v in values)
			{
				var i = actual.BinarySearch(v);
				actual.Insert(i < 0 ? ~i : i, v);
			}
			CollectionAssert.AreEqual(values.OrderBy(x => x).ToArray(), actual);
		}

		[TestMethod]
		public void Ctor()
		{
			var random = new Random();
			var values = Enumerable.Range(0, 100000).Select(i => random.Next(100000)).ToArray();
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
		public void Dequeue()
		{
			var random = new Random();
			var values = Enumerable.Range(0, 100000).Select(i => random.Next(100000)).ToArray();
			var actual = new OrderedList(values);
			for (int v1 = actual.Dequeue(), v2; actual.Count > 0; v1 = v2)
			{
				v2 = actual.Dequeue();
				Assert.IsTrue(v1 <= v2);
			}
		}

		[TestMethod]
		public void AddForOrder_Key()
		{
			var random = new Random();
			var values = Enumerable.Range(0, 10000).Select(i => random.Next(10000)).ToArray();
			var actual = new OrderedList<int, int>(x => x / 10);
			foreach (var v in values)
				actual.AddForOrder(v);
			CollectionAssert.AreEqual(values.OrderBy(x => x / 10).ToArray(), actual.Select(p => p.Value).ToArray());
		}
	}
}
