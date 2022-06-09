using System;

namespace CoderLib8.Collections
{
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/1/ITP2_1_B
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_ar
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_bi
	public class Deque<T>
	{
		static int ToPowerOf2(int n) { for (var p = 1; ; p <<= 1) if (p >= n) return p; }

		T[] a;
		int size, fi, li;
		public int Count => li - fi;
		int Mod(int i) => i & (size - 1);

		public Deque(int size = 8)
		{
			a = new T[this.size = ToPowerOf2(size)];
		}

		public T First => a[Mod(fi)];
		public T Last => a[Mod(li - 1)];
		public T this[int index]
		{
			get => a[Mod(fi + index)];
			set => a[Mod(fi + index)] = value;
		}

		public void PushFirst(T item)
		{
			if (Count == size) Expand();
			if (fi == 0) li += fi = size;
			a[Mod(--fi)] = item;
		}
		public void PushLast(T item)
		{
			if (Count == size) Expand();
			a[Mod(li++)] = item;
		}

		public T PopFirst() => a[Mod(fi++)];
		public T PopLast() => a[Mod(--li)];

		void Expand()
		{
			var c = Mod(fi);
			Array.Resize(ref a, size <<= 1);
			Array.Copy(a, 0, a, size >> 1, c);
		}
	}
}
