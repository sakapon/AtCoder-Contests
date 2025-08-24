using System;
using System.Collections.Generic;

// 0-based, bit operations
// ListHeapMultiSet と同じです。
namespace AlgorithmLib10.DataTrees.PQ202
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class RemovableListHeapQueue<T>
	{
		readonly IComparer<T> c;
		readonly int s;
		readonly List<T> l = new List<T>();
		readonly Dictionary<T, int> counts = new Dictionary<T, int>();
		int n;

		public RemovableListHeapQueue(IEnumerable<T> items = null, IComparer<T> comparer = null, bool descending = false)
		{
			c = comparer ?? Comparer<T>.Default;
			s = descending ? -1 : 1;
			if (items != null) foreach (var x in items) Push(x);
		}

		public IComparer<T> Comparer => c;
		public bool Descending => s == -1;
		public int Count => n;
		public T First => n != 0 ? l[0] : throw new InvalidOperationException("No items.");

		public void Clear()
		{
			l.Clear();
			counts.Clear();
			n = 0;
		}

		public int GetCount(T item) => counts.GetValueOrDefault(item);

		bool TrySwap(int i)
		{
			var p = (i - 1) >> 1;
			if (s * c.Compare(l[p], l[i]) <= 0) return false;
			(l[i], l[p]) = (l[p], l[i]);
			return true;
		}
		void UpHeap() { for (var i = l.Count - 1; i != 0 && TrySwap(i); i = (i - 1) >> 1) ; }
		void DownHeap() { for (var i = 1; i < l.Count && TrySwap(i + 1 < l.Count && s * c.Compare(l[i + 1], l[i]) < 0 ? ++i : i); i = (i << 1) | 1) ; }

		public void Push(T item)
		{
			l.Add(item);
			UpHeap();
			counts[item] = counts.GetValueOrDefault(item) + 1;
			++n;
		}

		public T Pop()
		{
			if (n == 0) throw new InvalidOperationException("No items.");
			var item = l[0];
			Remove(item);
			return item;
		}

		public bool Remove(T item)
		{
			if (!counts.TryGetValue(item, out var count)) return false;
			if (--count == 0) counts.Remove(item); else counts[item] = count;
			--n;
			EnsureFirst();
			return true;
		}

		void EnsureFirst()
		{
			while (l.Count != 0 && !counts.ContainsKey(l[0]))
			{
				while (l.Count != 0 && !counts.ContainsKey(l[l.Count - 1])) l.RemoveAt(l.Count - 1);
				if (l.Count == 0) break;
				l[0] = l[l.Count - 1];
				l.RemoveAt(l.Count - 1);
				DownHeap();
			}
		}
	}
}
