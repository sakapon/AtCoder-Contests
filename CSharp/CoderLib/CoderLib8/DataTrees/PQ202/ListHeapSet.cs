using System;
using System.Collections.Generic;

// 0-based, bit operations
// 非多重集合です。
namespace AlgorithmLib10.DataTrees.PQ202
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class ListHeapSet<T>
	{
		readonly IComparer<T> c;
		readonly int s;
		readonly List<T> l = new List<T>();
		readonly HashSet<T> u = new HashSet<T>();

		public ListHeapSet(IEnumerable<T> items = null, IComparer<T> comparer = null, bool descending = false)
		{
			c = comparer ?? Comparer<T>.Default;
			s = descending ? -1 : 1;
			if (items != null) foreach (var x in items) Add(x);
		}

		public IComparer<T> Comparer => c;
		public bool Descending => s == -1;
		public int Count => u.Count;
		public T First => u.Count != 0 ? l[0] : throw new InvalidOperationException("No items.");

		public void Clear()
		{
			l.Clear();
			u.Clear();
		}

		public bool Contains(T item) => u.Contains(item);

		bool TrySwap(int i)
		{
			var p = (i - 1) >> 1;
			if (s * c.Compare(l[p], l[i]) <= 0) return false;
			(l[i], l[p]) = (l[p], l[i]);
			return true;
		}
		void UpHeap() { for (var i = l.Count - 1; i != 0 && TrySwap(i); i = (i - 1) >> 1) ; }
		void DownHeap() { for (var i = 1; i < l.Count && TrySwap(i + 1 < l.Count && s * c.Compare(l[i + 1], l[i]) < 0 ? ++i : i); i = (i << 1) | 1) ; }

		public bool Add(T item)
		{
			if (!u.Add(item)) return false;
			l.Add(item);
			UpHeap();
			return true;
		}

		public bool RemoveFirst()
		{
			if (u.Count == 0) return false;
			return Remove(l[0]);
		}

		public bool Remove(T item)
		{
			if (!u.Remove(item)) return false;
			EnsureFirst();
			return true;
		}

		void EnsureFirst()
		{
			while (l.Count != 0 && !u.Contains(l[0]))
			{
				while (l.Count != 0 && !u.Contains(l[l.Count - 1])) l.RemoveAt(l.Count - 1);
				if (l.Count == 0) break;
				l[0] = l[l.Count - 1];
				l.RemoveAt(l.Count - 1);
				DownHeap();
			}
		}
	}
}
