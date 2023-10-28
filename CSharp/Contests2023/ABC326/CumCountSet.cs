using System;
using System.Collections.Generic;
using CoderLib8.Collections.Statics.Int;

// static count set
// 累積和による実装です。
// GetFirstGeq, GetLastLeq では、2 段階の探索が必要です。
namespace CoderLib8.Collections.Statics.Typed
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class CumCountSet<T> : IEnumerable<T>
	{
		#region Initialization
		readonly int n;
		readonly T[] a;
		readonly T minItem, maxItem;
		readonly IComparer<T> comparer;
		readonly IntCumCountSet set;

		// ソート済みであることを前提とします。
		public CumCountSet(T[] items, long[] counts, T minItem = default, T maxItem = default, IComparer<T> comparer = null)
		{
			if (items.Length != counts.Length) throw new ArgumentException("The lengths of items and counts must be equal.");
			n = items.Length;
			a = items;
			this.minItem = minItem;
			this.maxItem = maxItem;
			this.comparer = comparer ?? Comparer<T>.Default;
			set = new IntCumCountSet(counts);
		}

		public int ItemsCount => n;
		public long Count => set.Count;
		public T[] Items => a;
		public T MinItem => minItem;
		public T MaxItem => maxItem;
		public IComparer<T> Comparer => comparer;
		public IntCumCountSet IntSet => set;
		#endregion

		#region Item Id
		T ToItem(int i) => i < 0 ? minItem : i >= n ? maxItem : a[i];

		// count > 0 を満たす item とは限りません。
		// 値域 (-1, n]
		public int GetFirstItemIdGeq(T item)
		{
			int m, l = 0, r = n;
			while (l < r) if (comparer.Compare(a[m = l + r - 1 >> 1], item) >= 0) r = m; else l = m + 1;
			return r;
		}
		// 値域 [-1, n)
		public int GetLastItemIdLeq(T item)
		{
			int m, l = -1, r = n - 1;
			while (l < r) if (comparer.Compare(a[m = l + r + 1 >> 1], item) <= 0) l = m; else r = m - 1;
			return l;
		}

		public int GetItemId(T item)
		{
			var i = GetFirstItemIdGeq(item);
			return i < n && comparer.Compare(a[i], item) == 0 ? i : -1;
		}
		#endregion

		#region By Item Range
		public long GetFirstIndexGeq(T item) => set.GetFirstIndexGeq(GetFirstItemIdGeq(item));
		public long GetLastIndexLeq(T item) => set.GetLastIndexLeq(GetLastItemIdLeq(item));

		// secondary methods
		public T GetFirstGeq(T item) => GetAt(GetFirstIndexGeq(item));
		public T GetLastLeq(T item) => GetAt(GetLastIndexLeq(item));
		public long GetCountGeq(T item) => set.Count - GetFirstIndexGeq(item);
		public long GetCountLeq(T item) => GetLastIndexLeq(item) + 1;
		public long GetCount(T startItem, T endItem) => GetFirstIndexGeq(endItem) - GetFirstIndexGeq(startItem);
		#endregion

		#region By Item
		public long GetFirstIndex(T item)
		{
			var i = GetItemId(item);
			return i != -1 ? set.GetFirstIndex(i) : -1;
		}
		public long GetLastIndex(T item)
		{
			var i = GetItemId(item);
			return i != -1 ? set.GetLastIndex(i) : -1;
		}

		// secondary methods
		public bool Contains(T item) => GetFirstIndex(item) != -1;
		public long GetCount(T item) => GetLastIndexLeq(item) - GetFirstIndexGeq(item) + 1;
		#endregion

		#region By Index
		public T GetAt(long index) => ToItem(set.GetAt(index));
		#endregion

		#region Get Items
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<T> GetEnumerator()
		{
			foreach (var i in set) yield return a[i];
		}

		public IEnumerable<T> GetItems(T startItem, T endItem)
		{
			foreach (var i in set.GetItems(GetFirstItemIdGeq(startItem), GetFirstItemIdGeq(endItem))) yield return a[i];
		}

		public IEnumerable<T> GetItemsByIndex(long startIndex, long endIndex)
		{
			foreach (var i in set.GetItemsByIndex(startIndex, endIndex)) yield return a[i];
		}
		#endregion
	}
}
