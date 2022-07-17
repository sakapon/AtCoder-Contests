using System;
using System.Collections.Generic;

namespace CoderLib8.Graphs.Int
{
	// グラフ用の簡易的なリストです。
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class AList<T> : IEnumerable<T>
	{
		T[] a;
		int n;

		public AList(int capacity = 2) => a = new T[capacity];
		public AList(IEnumerable<T> items) : this() { foreach (var item in items) Add(item); }
		public AList(T[] items) => Initialize(items, items.Length);
		public AList(AList<T> list) => Initialize(list.a, list.n);
		void Initialize(T[] a0, int c)
		{
			n = c;
			while (c != (c & -c)) c += c & -c;
			a = new T[c];
			Array.Copy(a0, a, n);
		}

		public int Count => n;
		public T this[int i]
		{
			get => a[i];
			set => a[i] = value;
		}
		public T First => a[0];
		public T Last => a[n - 1];

		public void Clear() => n = 0;
		public void Add(T item)
		{
			if (n == a.Length) Expand();
			a[n++] = item;
		}
		void Expand() => Array.Resize(ref a, a.Length << 1);

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<T> GetEnumerator() { for (var i = 0; i < n; ++i) yield return a[i]; }
	}
}
