using System;
using System.Collections.Generic;

// static item set
// 配列による実装です。
// 実用可能です。
namespace CoderLib8.Collections.Statics.Typed
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class ArrayItemSet<T> : IEnumerable<T>
	{
		#region Initialization
		readonly int n;
		readonly T[] a;
		readonly T minItem, maxItem;
		readonly IComparer<T> comparer;

		// ソート済みであることを前提とします。
		public ArrayItemSet(T[] items, T minItem = default, T maxItem = default, IComparer<T> comparer = null)
		{
			a = items ?? throw new ArgumentNullException(nameof(items));
			n = a.Length;
			this.minItem = minItem;
			this.maxItem = maxItem;
			this.comparer = comparer ?? Comparer<T>.Default;
		}

		public int Count => n;
		public T[] Items => a;
		public T MinItem => minItem;
		public T MaxItem => maxItem;
		public IComparer<T> Comparer => comparer;
		#endregion

		#region By Index
		// 定義域 Z
		public T GetAt(int index) => index < 0 ? minItem : index >= n ? maxItem : a[index];
		#endregion

		#region By Item Range

		// 値域 (-1, n]
		public int GetFirstIndexGeq(T item)
		{
			int m, l = 0, r = n;
			while (l < r) if (comparer.Compare(a[m = l + r - 1 >> 1], item) >= 0) r = m; else l = m + 1;
			return r;
		}
		// 値域 [-1, n)
		public int GetLastIndexLeq(T item)
		{
			int m, l = -1, r = n - 1;
			while (l < r) if (comparer.Compare(a[m = l + r + 1 >> 1], item) <= 0) l = m; else r = m - 1;
			return l;
		}

		// secondary methods
		public T GetFirstGeq(T item) => GetAt(GetFirstIndexGeq(item));
		public T GetLastLeq(T item) => GetAt(GetLastIndexLeq(item));
		public int GetCountGeq(T item) => n - GetFirstIndexGeq(item);
		public int GetCountLeq(T item) => GetLastIndexLeq(item) + 1;
		public int GetCount(T startItem, T endItem) => GetFirstIndexGeq(endItem) - GetFirstIndexGeq(startItem);
		#endregion

		#region By Item
		public int GetFirstIndex(T item)
		{
			var i = GetFirstIndexGeq(item);
			return i < n && comparer.Compare(a[i], item) == 0 ? i : -1;
		}
		public int GetLastIndex(T item)
		{
			var i = GetLastIndexLeq(item);
			return i >= 0 && comparer.Compare(a[i], item) == 0 ? i : -1;
		}

		// secondary methods
		public bool Contains(T item) => GetFirstIndex(item) != -1;
		public int GetCount(T item) => GetLastIndexLeq(item) - GetFirstIndexGeq(item) + 1;
		#endregion

		#region Get Items
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)a).GetEnumerator();

		public IEnumerable<T> GetItems(T startItem, T endItem)
		{
			for (var i = GetFirstIndexGeq(startItem); i < n && comparer.Compare(a[i], endItem) < 0; ++i) yield return a[i];
		}

		public IEnumerable<T> GetItemsByIndex(int startIndex, int endIndex)
		{
			for (var i = startIndex; i < endIndex; ++i) yield return a[i];
		}
		#endregion
	}
}
