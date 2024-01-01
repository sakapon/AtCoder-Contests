using System;
using System.Collections.Generic;

// 0-based, bit operations
namespace AlgorithmLib10.DataTrees.PQ202
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class ListHeapQueueMap<TKey, TValue>
	{
		readonly IComparer<TKey> c;
		readonly int s;
		readonly List<TKey> l = new List<TKey>();
		readonly List<TValue> v = new List<TValue>();

		public ListHeapQueueMap(IEnumerable<(TKey key, TValue value)> items = null, IComparer<TKey> comparer = null, bool descending = false)
		{
			c = comparer ?? Comparer<TKey>.Default;
			s = descending ? -1 : 1;
			if (items != null) foreach (var (k, v) in items) Push(k, v);
		}

		public IComparer<TKey> Comparer => c;
		public bool Descending => s == -1;
		public int Count => l.Count;
		public (TKey key, TValue value) First => l.Count != 0 ? (l[0], v[0]) : throw new InvalidOperationException("No items.");

		public void Clear() { l.Clear(); v.Clear(); }

		bool TrySwap(int i)
		{
			var p = (i - 1) >> 1;
			if (s * c.Compare(l[p], l[i]) <= 0) return false;
			(l[i], l[p]) = (l[p], l[i]);
			(v[i], v[p]) = (v[p], v[i]);
			return true;
		}
		void UpHeap() { for (var i = l.Count - 1; i != 0 && TrySwap(i); i = (i - 1) >> 1) ; }
		void DownHeap() { for (var i = 1; i < l.Count && TrySwap(i + 1 < l.Count && s * c.Compare(l[i + 1], l[i]) < 0 ? ++i : i); i = (i << 1) | 1) ; }

		public void Push(TKey key, TValue value)
		{
			l.Add(key);
			v.Add(value);
			UpHeap();
		}

		public (TKey key, TValue value) Pop()
		{
			if (l.Count == 0) throw new InvalidOperationException("No items.");
			var item = (l[0], v[0]);
			l[0] = l[l.Count - 1];
			v[0] = v[v.Count - 1];
			l.RemoveAt(l.Count - 1);
			v.RemoveAt(v.Count - 1);
			DownHeap();
			return item;
		}
	}
}
