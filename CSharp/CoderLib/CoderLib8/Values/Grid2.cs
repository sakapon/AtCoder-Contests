using System;
using System.Collections.Generic;

namespace CoderLib8.Values
{
	public class Grid2<T> : IEnumerable<ArraySegment<T>>
	{
		public readonly int n1, n2;
		public readonly T[] a;
		public Grid2(int _n1, int _n2, T[] _a = null) => (n1, n2, a) = (_n1, _n2, _a ?? new T[_n1 * _n2]);
		public Grid2(int _n1, int _n2, T iv) : this(_n1, _n2, default(T[])) => Array.Fill(a, iv);

		public T this[int i, int j]
		{
			get => a[n2 * i + j];
			set => a[n2 * i + j] = value;
		}
		public ArraySegment<T> this[int i] => new ArraySegment<T>(a, n2 * i, n2);
		public T[] ToArray(int i) => a[(n2 * i)..(n2 * (i + 1))];

		public T[] GetColumn(int j)
		{
			var r = new T[n1];
			for (int i = 0; i < n1; ++i) r[i] = a[n2 * i + j];
			return r;
		}
		public IEnumerable<T[]> GetColumns() { for (int j = 0; j < n2; ++j) yield return GetColumn(j); }

		public void Fill(int i, T v) => Array.Fill(a, v, n2 * i, n2);
		public void Fill(T v) => Array.Fill(a, v);
		public void Clear() => Array.Clear(a, 0, a.Length);
		public Grid2<T> Clone() => new Grid2<T>(n1, n2, (T[])a.Clone());

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<ArraySegment<T>> GetEnumerator() { for (int i = 0; i < n1; ++i) yield return this[i]; }

		public bool IsInRange(int i, int j) => 0 <= i && i < n1 && 0 <= j && j < n2;

		public Grid2<T> Rotate180()
		{
			var b = (T[])a.Clone();
			Array.Reverse(b);
			return new Grid2<T>(n1, n2, b);
		}

		public Grid2<T> RotateLeft()
		{
			var r = new Grid2<T>(n2, n1);
			for (int i = 0; i < n2; ++i)
				for (int j = 0; j < n1; ++j)
					r[i, j] = this[j, n2 - 1 - i];
			return r;
		}

		public Grid2<T> RotateRight()
		{
			var r = new Grid2<T>(n2, n1);
			for (int i = 0; i < n2; ++i)
				for (int j = 0; j < n1; ++j)
					r[i, j] = this[n1 - 1 - j, i];
			return r;
		}

		public Grid2<T> Transpose()
		{
			var r = new Grid2<T>(n2, n1);
			for (int i = 0; i < n2; ++i)
				for (int j = 0; j < n1; ++j)
					r[i, j] = this[j, i];
			return r;
		}
	}
}
