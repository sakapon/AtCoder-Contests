using System;
using System.Collections.Generic;

namespace CoderLib8.Collections.Dynamics.Int
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class IntSegmentMultiSet : IEnumerable<int>
	{
		readonly int n = 1;
		readonly long[] c;

		public IntSegmentMultiSet(int itemsCount)
		{
			while (n < itemsCount) n <<= 1;
			c = new long[n << 1];
		}

		public int ItemsCount => n;
		public long Count => c[1];

		public long GetCount(int i) => c[n | i];
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
		public long GetCountBefore(int i)
		{
			if (i == n) return c[1];
			var s = 0L;
			for (i |= n; i > 1; i >>= 1) if ((i & 1) != 0) s += c[--i];
			return s;
		}

		public long GetIndex(int i) => GetCountBefore(i);
		public int GetFirstGeq(int i) => GetAt(GetCountBefore(i));
		public int GetLastLeq(int i) => GetAt(GetCountBefore(i + 1) - 1);

		public int GetAt(long index)
		{
			if (index < 0) return -1;
			if (c[1] <= index) return n;
			var i = 1;
			while ((i & n) == 0) if (c[i <<= 1] <= index) index -= c[i++];
			return i & ~n;
		}

		public void Add(int i, long delta = 1) { for (i |= n; i > 0; i >>= 1) c[i] += delta; }
		int RemoveOne(int i) { if (i != -1 && i != n) Add(i, -1); return i; }
		public int RemoveFirstGeq(int i) => RemoveOne(GetFirstGeq(i));
		public int RemoveLastLeq(int i) => RemoveOne(GetLastLeq(i));
		public int RemoveAt(long index) => RemoveOne(GetAt(index));

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<int> GetEnumerator() { for (var i = 0; i < n; ++i) for (var j = c[n | i] - 1; j >= 0; --j) yield return i; }
	}
}
