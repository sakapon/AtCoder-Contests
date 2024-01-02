using System;
using System.Collections.Generic;

// dynamic count set
// セグメント木による実装です。
namespace CoderLib8.Collections.Dynamics.Int
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class IntSegmentCountSet : IEnumerable<int>
	{
		#region Initialization
		readonly int n = 1;
		readonly long[] c;

		public IntSegmentCountSet(int itemsCount, long[] counts = null)
		{
			while (n < itemsCount) n <<= 1;
			c = new long[n << 1];
			if (counts != null)
			{
				Array.Copy(counts, 0, c, n, counts.Length);
				for (int i = n - 1; i > 0; --i) c[i] = c[i << 1] + c[(i << 1) | 1];
			}
		}

		public int ItemsCount => n;
		public long Count => c[1];
		#endregion

		#region By Item Range
		public long GetFirstIndexGeq(int i) => GetCountLt(i);
		public long GetLastIndexLeq(int i) => GetCountLt(i + 1) - 1;

		public int GetFirstGeq(int i) => GetAt(GetCountLt(i));
		public int GetLastLeq(int i) => GetAt(GetCountLt(i + 1) - 1);

		public long GetCountGeq(int i) => c[1] - GetCountLt(i);
		public long GetCountLeq(int i) => GetCountLt(i + 1);

		long GetCountLt(int i)
		{
			if (i == n) return c[1];
			var s = 0L;
			for (i |= n; i > 1; i >>= 1) if ((i & 1) != 0) s += c[--i];
			return s;
		}
		public long GetCount(int l, int r)
		{
			var s = 0L;
			for (l += n, r += n; l < r; l >>= 1, r >>= 1)
			{
				if ((l & 1) != 0) s += c[l++];
				if ((r & 1) != 0) s += c[--r];
			}
			return s;
		}
		#endregion

		#region By Item
		public long GetFirstIndex(int i) => c[n | i] > 0 ? GetCountLt(i) : -1;
		public long GetLastIndex(int i) => c[n | i] > 0 ? GetCountLt(i + 1) - 1 : -1;

		public bool Contains(int i) => c[n | i] > 0;
		public long GetCount(int i) => c[n | i];
		#endregion

		#region By Index
		public int GetAt(long index)
		{
			if (index < 0) return -1;
			if (c[1] <= index) return n;
			var i = 1;
			while ((i & n) == 0) if (c[i <<= 1] <= index) index -= c[i++];
			return i & ~n;
		}
		#endregion

		#region Add
		public bool Add(int i, long delta = 1)
		{
			if (c[n | i] + delta < 0) return false;
			for (i |= n; i > 0; i >>= 1) c[i] += delta;
			return true;
		}
		public bool Remove(int i, long delta = 1) => Add(i, -delta);
		public bool Set(int i, long count) => Add(i, count - c[n | i]);
		#endregion

		#region Remove
		int RemoveOne(int i) { if (i != -1 && i != n) Add(i, -1); return i; }
		public int RemoveFirstGeq(int i) => RemoveOne(GetFirstGeq(i));
		public int RemoveLastLeq(int i) => RemoveOne(GetLastLeq(i));
		public int RemoveAt(long index) => RemoveOne(GetAt(index));
		#endregion

		#region Get Items
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<int> GetEnumerator()
		{
			for (var i = 0; i < n; ++i)
				for (var j = c[n | i] - 1; j >= 0; --j) yield return i;
		}

		public IEnumerable<int> GetItems(int l, int r)
		{
			for (var i = l; i < r; ++i)
				for (var j = c[n | i] - 1; j >= 0; --j) yield return i;
		}

		public IEnumerable<int> GetItemsByIndex(long l, long r)
		{
			var (i, ci) = (1, l);
			while ((i & n) == 0) if (c[i <<= 1] <= ci) ci -= c[i++];
			for (i &= ~n; l < r; ++i, ci = 0)
				for (; ci < c[n | i] && l < r; ++ci, ++l) yield return i;
		}
		#endregion
	}
}
