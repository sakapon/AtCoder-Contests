using System;
using System.Collections.Generic;

// https://github.com/sakapon/Samples-2020/blob/master/AlgorithmSample/AlgorithmLab/DataTrees/PQ/HeapQueue201.cs
namespace CoderLib8.DataTrees
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class HeapQueue<T>
	{
		T[] a;
		int n = 1;
		readonly IComparer<T> c;

		public HeapQueue(IComparer<T> comparer = null, IEnumerable<T> items = null)
		{
			a = new T[4];
			c = comparer ?? Comparer<T>.Default;
			if (items != null) foreach (var item in items) Add(item);
		}

		public HeapQueue(bool descending, IEnumerable<T> items = null) : this(GetComparer(descending), items) { }
		static IComparer<T> GetComparer(bool descending)
		{
			var asc = Comparer<T>.Default;
			return descending ? Comparer<T>.Create((x, y) => asc.Compare(y, x)) : asc;
		}

		public int Count => n - 1;
		public IComparer<T> Comparer => c;
		public T First => a[1];

		public void Clear() => n = 1;

		public void Add(T item)
		{
			if (n == a.Length) Array.Resize(ref a, a.Length << 1);
			a[n++] = item;

			for (int i = n - 1; i > 1 && c.Compare(a[i], a[i >> 1]) < 0; i >>= 1)
			{
				(a[i], a[i >> 1]) = (a[i >> 1], a[i]);
			}
		}

		public T Pop()
		{
			var item = a[1];
			a[1] = a[--n];

			for (int i = 2; i < n; i <<= 1)
			{
				if (i + 1 < n && c.Compare(a[i], a[i + 1]) > 0) ++i;
				if (c.Compare(a[i], a[i >> 1]) >= 0) break;
				(a[i], a[i >> 1]) = (a[i >> 1], a[i]);
			}
			return item;
		}

		public IEnumerable<T> GetRawItems() { for (var i = 1; i < n; ++i) yield return a[i]; }
	}

	public class HeapQueue<TKey, TValue> : HeapQueue<KeyValuePair<TKey, TValue>>
	{
		public HeapQueue(IComparer<TKey> comparer = null, IEnumerable<KeyValuePair<TKey, TValue>> items = null) : base(GetComparer(comparer), items) { }
		public HeapQueue(bool descending, IEnumerable<KeyValuePair<TKey, TValue>> items = null) : base(GetComparer(descending), items) { }

		static IComparer<KeyValuePair<TKey, TValue>> GetComparer(IComparer<TKey> c)
		{
			c = c ?? Comparer<TKey>.Default;
			return Comparer<KeyValuePair<TKey, TValue>>.Create((x, y) => c.Compare(x.Key, y.Key));
		}
		static IComparer<KeyValuePair<TKey, TValue>> GetComparer(bool descending)
		{
			var asc = Comparer<TKey>.Default;
			return descending ? Comparer<KeyValuePair<TKey, TValue>>.Create((x, y) => asc.Compare(y.Key, x.Key)) : Comparer<KeyValuePair<TKey, TValue>>.Create((x, y) => asc.Compare(x.Key, y.Key));
		}

		public void Add(TKey key, TValue value) => Add(new KeyValuePair<TKey, TValue>(key, value));
	}
}
