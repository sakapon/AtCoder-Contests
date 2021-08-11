using System;
using System.Collections.Generic;
using System.Linq;

namespace CoderLib6.DataTrees
{
	// キーのみ、重複なしの場合に使えます。
	// First: Min
	// Push: Add
	class BstPQ1<T> : SortedSet<T>
	{
		public T Pop() { var r = Min; Remove(r); return r; }
	}

	// キーのみ、重複ありの場合にも使えます。
	class BstPQ<T>
	{
		SortedDictionary<T, int> d = new SortedDictionary<T, int>();
		public bool Any => d.Count > 0;
		public T First => d.First().Key;

		public void Push(T v)
		{
			int c;
			d.TryGetValue(v, out c);
			d[v] = c + 1;
		}

		public T Pop()
		{
			var v = First;
			if (d[v] == 1) d.Remove(v);
			else --d[v];
			return v;
		}
	}
}
