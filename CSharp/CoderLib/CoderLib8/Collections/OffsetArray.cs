using System;
using System.Collections.Generic;

namespace CoderLib8.Collections
{
	[System.Diagnostics.DebuggerDisplay(@"Start = {Offset}, Count = {Count}")]
	public class OffsetArray<T> : IEnumerable<T>
	{
		readonly T[] a;
		readonly int offset;

		public T[] Raw => a;
		public int Offset => offset;
		public int Count => a.Length;

		public T this[int i]
		{
			get => a[i - offset];
			set => a[i - offset] = value;
		}

		public OffsetArray(T[] a, int offset)
		{
			this.a = a;
			this.offset = offset;
		}
		// [start, start + count)
		public OffsetArray(int start, int count) : this(new T[count], start) { }
		// [l, r)
		public static OffsetArray<T> Create(int l, int r) => new OffsetArray<T>(l, r - l);

		public void Clear() => Array.Clear(a, 0, a.Length);
		public OffsetArray<T> Clone() => new OffsetArray<T>((T[])a.Clone(), offset);

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)a).GetEnumerator();
	}
}
