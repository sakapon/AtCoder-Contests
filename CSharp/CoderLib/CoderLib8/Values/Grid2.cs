using System;
using System.Collections.Generic;

namespace CoderLib8.Values
{
	public class Grid2<T> : IEnumerable<ArraySegment<T>>, IEquatable<Grid2<T>>
	{
		// 下位ビットが分散するようにハッシュを生成します。
		public static int GetHashCode(T[] a)
		{
			var h = 0;
			for (int i = 0; i < a.Length; ++i) h = h * 10000019 + a[i].GetHashCode();
			return h;
		}

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

		#region Equality Operators
		public bool Equals(Grid2<T> other) => !(other is null) && Equals(a, other.a);
		public static bool Equals(Grid2<T> v1, Grid2<T> v2) => v1?.Equals(v2) ?? (v2 is null);
		public static bool operator ==(Grid2<T> v1, Grid2<T> v2) => Equals(v1, v2);
		public static bool operator !=(Grid2<T> v1, Grid2<T> v2) => !Equals(v1, v2);
		public override bool Equals(object obj) => Equals(obj as Grid2<T>);
		public override int GetHashCode() => GetHashCode(a);

		public static bool Equals(T[] a1, T[] a2)
		{
			if (a1.Length != a2.Length) return false;
			var c = EqualityComparer<T>.Default;
			for (int i = 0; i < a1.Length; ++i)
				if (!c.Equals(a1[i], a2[i])) return false;
			return true;
		}
		#endregion

		public bool IsInRange(int i, int j) => 0 <= i && i < n1 && 0 <= j && j < n2;

		public Grid2<T> SwapRows(int i1, int i2)
		{
			var r = new Grid2<T>(n1, n2, (T[])a.Clone());
			Array.Copy(a, n2 * i2, r.a, n2 * i1, n2);
			Array.Copy(a, n2 * i1, r.a, n2 * i2, n2);
			return r;
		}

		public Grid2<T> SwapColumns(int j1, int j2)
		{
			var r = new Grid2<T>(n1, n2, (T[])a.Clone());
			for (int i = 0; i < n1; ++i)
			{
				r[i, j1] = this[i, j2];
				r[i, j2] = this[i, j1];
			}
			return r;
		}

		public Grid2<T> Rotate180()
		{
			var r = new Grid2<T>(n1, n2, (T[])a.Clone());
			Array.Reverse(r.a);
			return r;
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
