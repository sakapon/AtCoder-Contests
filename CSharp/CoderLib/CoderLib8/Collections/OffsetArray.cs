using System;
using System.Collections.Generic;

namespace CoderLib8.Collections
{
	public class OffsetArray<T>
	{
		readonly T[] a;
		readonly int offset;

		public T[] Raw => a;
		public int Offset => offset;
		public T this[int i]
		{
			get => a[i - offset];
			set => a[i - offset] = value;
		}

		// [start, start + count)
		public OffsetArray(int start, int count)
		{
			a = new T[count];
			offset = start;
		}
		// [l, r)
		public static OffsetArray<T> Create(int l, int r) => new OffsetArray<T>(l, r - l);

		public void Clear() => Array.Clear(a, 0, a.Length);
	}
}
