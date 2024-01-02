using System;

namespace CoderLib8.Collections
{
	// 1 次元配列に 2 次元インデックスでアクセスします。
	public class SeqArray2<T>
	{
		public readonly int n1, n2;
		public readonly T[] a;
		public SeqArray2(int _n1, int _n2, T[] _a) => (n1, n2, a) = (_n1, _n2, _a);
		public SeqArray2(int _n1, int _n2) : this(_n1, _n2, new T[_n1 * _n2]) { }

		public T this[int i, int j]
		{
			get => a[n2 * i + j];
			set => a[n2 * i + j] = value;
		}
		public T[] this[int i] => a[(n2 * i)..(n2 * (i + 1))];
		public ArraySegment<T> GetSegment(int i) => new ArraySegment<T>(a, n2 * i, n2);

		public void Fill(T v) => Array.Fill(a, v);
	}

	// 1 次元配列に 3 次元インデックスでアクセスします。
	public class SeqArray3<T>
	{
		public readonly int n1, n2, n3;
		public readonly T[] a;
		public SeqArray3(int _n1, int _n2, int _n3, T[] _a) => (n1, n2, n3, a) = (_n1, _n2, _n3, _a);
		public SeqArray3(int _n1, int _n2, int _n3) : this(_n1, _n2, _n3, new T[_n1 * _n2 * _n3]) { }

		public T this[int i, int j, int k]
		{
			get => a[n3 * n2 * i + n3 * j + k];
			set => a[n3 * n2 * i + n3 * j + k] = value;
		}
		public T[] this[int i, int j] => a[(n3 * (n2 * i + j))..(n3 * (n2 * i + j + 1))];
		public ArraySegment<T> GetSegment(int i, int j) => new ArraySegment<T>(a, n3 * (n2 * i + j), n3);
		public SeqArray2<T> this[int i] => new SeqArray2<T>(n2, n3, a[(n3 * n2 * i)..(n3 * n2 * (i + 1))]);

		public void Fill(T v) => Array.Fill(a, v);
	}
}
