using System;
using System.Collections.Generic;

namespace CoderLib8.Collections
{
	// 1 次元配列に 2 次元インデックスでアクセスします。
	public class SeqArray2<T> : IEnumerable<ArraySegment<T>>
	{
		public readonly int n1, n2;
		public readonly T[] a;
		public SeqArray2(int _n1, int _n2, T[] _a = null) => (n1, n2, a) = (_n1, _n2, _a ?? new T[_n1 * _n2]);
		public SeqArray2(int _n1, int _n2, T iv) : this(_n1, _n2, default(T[])) => Array.Fill(a, iv);

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

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<ArraySegment<T>> GetEnumerator() { for (int i = 0; i < n1; ++i) yield return this[i]; }

		public bool IsInRange(int i, int j) => 0 <= i && i < n1 && 0 <= j && j < n2;

		public SeqArray2<T> Rotate180()
		{
			var b = (T[])a.Clone();
			Array.Reverse(b);
			return new SeqArray2<T>(n1, n2, b);
		}

		public SeqArray2<T> RotateLeft()
		{
			var r = new SeqArray2<T>(n2, n1);
			for (int i = 0; i < n2; ++i)
				for (int j = 0; j < n1; ++j)
					r[i, j] = this[j, n2 - 1 - i];
			return r;
		}

		public SeqArray2<T> RotateRight()
		{
			var r = new SeqArray2<T>(n2, n1);
			for (int i = 0; i < n2; ++i)
				for (int j = 0; j < n1; ++j)
					r[i, j] = this[n1 - 1 - j, i];
			return r;
		}

		public SeqArray2<T> Transpose()
		{
			var r = new SeqArray2<T>(n2, n1);
			for (int i = 0; i < n2; ++i)
				for (int j = 0; j < n1; ++j)
					r[i, j] = this[j, i];
			return r;
		}
	}

	// 1 次元配列に 3 次元インデックスでアクセスします。
	public class SeqArray3<T>
	{
		public readonly int n1, n2, n3;
		public readonly T[] a;
		public SeqArray3(int _n1, int _n2, int _n3, T[] _a = null) => (n1, n2, n3, a) = (_n1, _n2, _n3, _a ?? new T[_n1 * _n2 * _n3]);
		public SeqArray3(int _n1, int _n2, int _n3, T iv) : this(_n1, _n2, _n3, default(T[])) => Array.Fill(a, iv);

		public T this[int i, int j, int k]
		{
			get => a[n3 * (n2 * i + j) + k];
			set => a[n3 * (n2 * i + j) + k] = value;
		}
		public T[] this[int i, int j] => a[(n3 * (n2 * i + j))..(n3 * (n2 * i + j + 1))];
		public ArraySegment<T> GetSegment(int i, int j) => new ArraySegment<T>(a, n3 * (n2 * i + j), n3);
		public SeqArray2<T> this[int i] => new SeqArray2<T>(n2, n3, a[(n3 * n2 * i)..(n3 * n2 * (i + 1))]);

		public void Fill(int i, int j, T v) => Array.Fill(a, v, n3 * (n2 * i + j), n3);
		public void Fill(int i, T v) => Array.Fill(a, v, n3 * n2 * i, n3 * n2);
		public void Fill(T v) => Array.Fill(a, v);
		public void Clear() => Array.Clear(a, 0, a.Length);
	}
}
