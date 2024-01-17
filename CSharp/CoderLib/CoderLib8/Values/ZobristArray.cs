using System;
using System.Collections.Generic;

namespace CoderLib8.Values
{
	public class ZobristArray<T> : IEnumerable<T>, IEquatable<ZobristArray<T>>
	{
		// 下位ビットが分散するようにハッシュを生成します。
		static int CreateHash(int id, T value) => id * 1000003 + value.GetHashCode() * 10000019;

		public readonly T[] a;
		int hash;

		public ZobristArray(T[] _a, int _hash) => (a, hash) = (_a, _hash);
		public ZobristArray(int n) : this(new T[n]) { }
		public ZobristArray(T[] _a) : this(_a, 0)
		{
			for (int i = 0; i < a.Length; ++i)
				hash ^= CreateHash(i, a[i]);
		}

		public T this[int i]
		{
			get => a[i];
			set
			{
				hash ^= CreateHash(i, a[i]);
				hash ^= CreateHash(i, value);
				a[i] = value;
			}
		}

		public void Fill(T v) { for (int i = 0; i < a.Length; ++i) this[i] = v; }
		public void Clear() => Fill(default(T));
		public ZobristArray<T> Clone() => new ZobristArray<T>((T[])a.Clone(), hash);

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<T> GetEnumerator() { for (int i = 0; i < a.Length; ++i) yield return a[i]; }

		#region Equality Operators
		public bool Equals(ZobristArray<T> other) => !(other is null) && Equals(a, other.a);
		public static bool Equals(ZobristArray<T> v1, ZobristArray<T> v2) => v1?.Equals(v2) ?? (v2 is null);
		public static bool operator ==(ZobristArray<T> v1, ZobristArray<T> v2) => Equals(v1, v2);
		public static bool operator !=(ZobristArray<T> v1, ZobristArray<T> v2) => !Equals(v1, v2);
		public override bool Equals(object obj) => Equals(obj as ZobristArray<T>);
		public override int GetHashCode() => hash;

		public static bool Equals(T[] a1, T[] a2)
		{
			if (a1.Length != a2.Length) return false;
			var c = EqualityComparer<T>.Default;
			for (int i = 0; i < a1.Length; ++i)
				if (!c.Equals(a1[i], a2[i])) return false;
			return true;
		}
		#endregion
	}
}
