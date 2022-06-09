using System;

namespace CoderLib8.Collections
{
	public class RingBuffer<T>
	{
		static int ToPowerOf2(int n) { for (var p = 1; ; p <<= 1) if (p >= n) return p; }

		int n;
		T[] a;

		public int Size => n;
		public T this[int index]
		{
			get => a[index & (n - 1)];
			set => a[index & (n - 1)] = value;
		}

		public RingBuffer(int size = 8)
		{
			a = new T[n = ToPowerOf2(size)];
		}

		public void Expand(int start)
		{
			start &= n - 1;
			var b = new T[n <<= 1];
			Array.Copy(a, start, b, start, a.Length - start);
			Array.Copy(a, 0, b, a.Length, start);
			a = b;
		}
	}
}
