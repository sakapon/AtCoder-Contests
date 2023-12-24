using System;
using System.Collections.Generic;

// static count set
// 累積和による実装です。
// 実用可能です。

// static range sum の機能を含みますが、count >= 0 を前提としています。
// static multilist と考えることもできます。
// typed にすれば例えば RLE を表現できますが、構造上の追加はほぼありません。
// [0, n) の値を item (unique id) と考えます。
namespace CoderLib8.Collections.Statics.Int
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class IntCumCountSet : IEnumerable<int>
	{
		#region Initialization
		readonly int n;
		readonly long[] s;

		public IntCumCountSet(long[] counts)
		{
			n = counts.Length;
			s = new long[n + 1];
			for (int i = 0; i < n; ++i) s[i + 1] = s[i] + counts[i];
		}

		public int ItemsCount => n;
		public long Count => s[n];
		public long[] CumSum => s;
		#endregion

		#region By Item Range
		public long GetFirstIndexGeq(int item) => s[item];
		public long GetLastIndexLeq(int item) => s[item + 1] - 1;

		public int GetFirstGeq(int item) => GetAt(s[item]);
		public int GetLastLeq(int item) => GetAt(s[item + 1] - 1);

		public long GetCountGeq(int item) => s[n] - s[item];
		public long GetCountLeq(int item) => s[item + 1];

		public long GetCount(int startItem, int endItem) => s[endItem] - s[startItem];
		#endregion

		#region By Item
		public long GetFirstIndex(int item) => s[item] < s[item + 1] ? s[item] : -1;
		public long GetLastIndex(int item) => s[item] < s[item + 1] ? s[item + 1] - 1 : -1;

		public bool Contains(int item) => s[item] < s[item + 1];
		public long GetCount(int item) => s[item + 1] - s[item];
		#endregion

		#region By Index
		public int GetAt(long index)
		{
			int m, l = -1, r = n;
			while (l < r) if (s[m = l + r + 1 >> 1] <= index) l = m; else r = m - 1;
			return l;
		}
		#endregion

		#region Get Items
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<int> GetEnumerator()
		{
			var c = 0L;
			for (int i = 0; i < n; ++i)
				for (; c < s[i + 1]; ++c) yield return i;
		}

		public IEnumerable<int> GetItems(int startItem, int endItem)
		{
			var c = s[startItem];
			for (int i = startItem; i < endItem; ++i)
				for (; c < s[i + 1]; ++c) yield return i;
		}

		public IEnumerable<int> GetItemsByIndex(long startIndex, long endIndex)
		{
			var c = startIndex;
			for (int i = GetAt(c); c < endIndex; ++i)
				for (; c < s[i + 1] && c < endIndex; ++c) yield return i;
		}
		#endregion
	}
}
