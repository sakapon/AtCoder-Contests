using System;
using System.Collections.Generic;

// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/1/ITP2_1_B
// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/2/ITP2_2_B
// Test: https://atcoder.jp/contests/typical90/tasks/typical90_ar
// Test: https://atcoder.jp/contests/typical90/tasks/typical90_bi
namespace AlgorithmLab.Collections.Arrays301
{
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class ArrayDeque<T> : IEnumerable<T>
	{
		const int MinCapacity = 1 << 1;
		T[] a;
		int fi, li;
		int f;

		public ArrayDeque(IEnumerable<T> items = null)
		{
			a = new T[MinCapacity];
			f = MinCapacity - 1;
			if (items != null) foreach (var x in items) AddLast(x);
		}

		public ArrayDeque(T[] items) => Initialize(items, items.Length);
		void Initialize(T[] a0, int c)
		{
			li = c;
			if (c < MinCapacity) c = MinCapacity;
			while (c != (c & -c)) c += c & -c;
			a = new T[c];
			Array.Copy(a0, a, li);
			f = c - 1;
		}

		public int Count => li - fi;
		public T this[int i]
		{
			get => a[fi + i & f];
			set => a[fi + i & f] = value;
		}
		public T First
		{
			get => a[fi & f];
			set => a[fi & f] = value;
		}
		public T Last
		{
			get => a[li - 1 & f];
			set => a[li - 1 & f] = value;
		}

		public void Clear()
		{
			a = new T[MinCapacity];
			f = MinCapacity - 1;
			fi = li = 0;
		}

		public void AddFirst(T item)
		{
			TryExpand();
			a[--fi & f] = item;
		}
		public void AddLast(T item)
		{
			TryExpand();
			a[li++ & f] = item;
		}
		public T PopFirst()
		{
			if (fi == li) throw new InvalidOperationException("No items.");
			TryReduce();
			return a[fi++ & f];
		}
		public T PopLast()
		{
			if (fi == li) throw new InvalidOperationException("No items.");
			TryReduce();
			return a[--li & f];
		}

		void TryExpand()
		{
			if (li - fi != a.Length) return;
			Array.Resize(ref a, a.Length << 1);
			Array.Copy(a, 0, a, a.Length >> 1, a.Length >> 1);
			f = a.Length - 1;
		}
		void TryReduce()
		{
			if (li - fi != a.Length >> 2 || li - fi < MinCapacity) return;
			var q = a.Length >> 2;
			var a0 = a;
			a = new T[a.Length >> 1];
			Array.Copy(a0, (fi & f) < q || (li & f) < q ? 0 : q << 1, a, 0, q);
			Array.Copy(a0, (fi & f) < ((q << 1) | q) && (li & f) < ((q << 1) | q) ? q : (q << 1) | q, a, q, q);
			f = a.Length - 1;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<T> GetEnumerator() { for (var i = fi; i < li; ++i) yield return a[i & f]; }

		public T[] ToArray()
		{
			var r = new T[Count];
			for (var i = fi; i < li; ++i) r[i - fi] = a[i & f];
			return r;
		}
	}
}
