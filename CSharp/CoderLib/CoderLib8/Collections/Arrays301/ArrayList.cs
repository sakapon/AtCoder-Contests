using System;
using System.Collections.Generic;

// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/1/ITP2_1_A
// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/1/ITP2_1_D
// コレクション初期化子を利用できます。
namespace AlgorithmLab.Collections.Arrays301
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class ArrayList<T> : IEnumerable<T>
	{
		const int MinCapacity = 1 << 1;
		T[] a;
		int n;

		public ArrayList(IEnumerable<T> items = null)
		{
			a = new T[MinCapacity];
			if (items != null) foreach (var x in items) Add(x);
		}

		public ArrayList(T[] items) => Initialize(items, items.Length);
		public ArrayList(ArrayList<T> list) => Initialize(list.a, list.n);
		void Initialize(T[] a0, int c)
		{
			n = c;
			if (c < MinCapacity) c = MinCapacity;
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
		public T Last
		{
			get => a[n - 1];
			set => a[n - 1] = value;
		}

		public void Clear()
		{
			a = new T[MinCapacity];
			n = 0;
		}

		public void Add(T item)
		{
			TryExpand();
			a[n++] = item;
		}
		public T Pop()
		{
			if (n == 0) throw new InvalidOperationException("No items.");
			TryReduce();
			return a[--n];
		}

		void TryExpand()
		{
			if (n != a.Length) return;
			Array.Resize(ref a, a.Length << 1);
		}
		void TryReduce()
		{
			if (n != a.Length >> 2 || n < MinCapacity) return;
			Array.Resize(ref a, a.Length >> 1);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<T> GetEnumerator() { for (var i = 0; i < n; ++i) yield return a[i]; }

		public T[] ToArray()
		{
			var r = new T[n];
			Array.Copy(a, r, n);
			return r;
		}

		public ArraySegment<T> Slice(int l, int r) => new ArraySegment<T>(a, l, r - l);
		public void Reverse() => Array.Reverse(a, 0, n);
	}
}
