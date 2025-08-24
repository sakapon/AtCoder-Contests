using System;
using System.Collections.Generic;

// Test: https://atcoder.jp/contests/abc332/tasks/abc332_d
// Test: https://atcoder.jp/contests/abc336/tasks/abc336_f
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

		public readonly int h, w;
		public readonly T[] a;
		public Grid2(int _h, int _w, T[] _a = null) => (h, w, a) = (_h, _w, _a ?? new T[_h * _w]);
		public Grid2(int _h, int _w, T iv) : this(_h, _w, default(T[])) => Array.Fill(a, iv);

		public T this[int i, int j]
		{
			get => a[w * i + j];
			set => a[w * i + j] = value;
		}
		public ArraySegment<T> this[int i] => new ArraySegment<T>(a, w * i, w);
		public T[] ToArray(int i) => a[(w * i)..(w * (i + 1))];

		public T[] GetColumn(int j)
		{
			var r = new T[h];
			for (int i = 0; i < h; ++i) r[i] = a[w * i + j];
			return r;
		}
		public IEnumerable<T[]> GetColumns() { for (int j = 0; j < w; ++j) yield return GetColumn(j); }

		public void Fill(int i, T v) => Array.Fill(a, v, w * i, w);
		public void Fill(T v) => Array.Fill(a, v);
		public void Clear() => Array.Clear(a, 0, a.Length);
		public Grid2<T> Clone() => new Grid2<T>(h, w, (T[])a.Clone());

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<ArraySegment<T>> GetEnumerator() { for (int i = 0; i < h; ++i) yield return this[i]; }

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

		public bool IsInRange(int i, int j) => 0 <= i && i < h && 0 <= j && j < w;

		public IEnumerable<(int i, int j)> GetNexts(int i, int j)
		{
			if (i > 0) yield return (i - 1, j);
			if (j > 0) yield return (i, j - 1);
			if (i + 1 < h) yield return (i + 1, j);
			if (j + 1 < w) yield return (i, j + 1);
		}

		public IEnumerable<int> GetNexts(int v)
		{
			var i = Math.DivRem(v, w, out var j);
			if (i > 0) yield return v - w;
			if (j > 0) yield return v - 1;
			if (i + 1 < h) yield return v + w;
			if (j + 1 < w) yield return v + 1;
		}

		public Grid2<T> SwapRows(int i1, int i2)
		{
			var r = new Grid2<T>(h, w, (T[])a.Clone());
			Array.Copy(a, w * i2, r.a, w * i1, w);
			Array.Copy(a, w * i1, r.a, w * i2, w);
			return r;
		}

		public Grid2<T> SwapColumns(int j1, int j2)
		{
			var r = new Grid2<T>(h, w, (T[])a.Clone());
			for (int i = 0; i < h; ++i)
			{
				r[i, j1] = this[i, j2];
				r[i, j2] = this[i, j1];
			}
			return r;
		}

		public Grid2<T> Rotate180()
		{
			var r = new Grid2<T>(h, w, (T[])a.Clone());
			Array.Reverse(r.a);
			return r;
		}

		public Grid2<T> RotateLeft()
		{
			var r = new Grid2<T>(w, h);
			for (int i = 0; i < w; ++i)
				for (int j = 0; j < h; ++j)
					r[i, j] = this[j, w - 1 - i];
			return r;
		}

		public Grid2<T> RotateRight()
		{
			var r = new Grid2<T>(w, h);
			for (int i = 0; i < w; ++i)
				for (int j = 0; j < h; ++j)
					r[i, j] = this[h - 1 - j, i];
			return r;
		}

		public Grid2<T> Transpose()
		{
			var r = new Grid2<T>(w, h);
			for (int i = 0; i < w; ++i)
				for (int j = 0; j < h; ++j)
					r[i, j] = this[j, i];
			return r;
		}
	}
}
