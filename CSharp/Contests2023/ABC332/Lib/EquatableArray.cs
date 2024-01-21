using System;
using System.Collections.Generic;

// Test: https://atcoder.jp/contests/abc322/tasks/abc322_e
// Test: https://atcoder.jp/contests/abc332/tasks/abc332_d
// Test: https://atcoder.jp/contests/abc336/tasks/abc336_f
// ハッシュをキャッシュする実装にしてもよいですが、いずれにしても生成に O(n) かかります。
namespace CoderLib8.Values
{
	public class EquatableArray<T> : IEnumerable<T>, IEquatable<EquatableArray<T>>
	{
		// 下位ビットが分散するようにハッシュを生成します。
		public static int GetHashCode(T[] a)
		{
			var h = 0;
			for (int i = 0; i < a.Length; ++i) h = h * 987654323 + a[i].GetHashCode();
			return h;
		}

		public readonly T[] a;

		public EquatableArray(T[] _a) => a = _a;
		public EquatableArray(int n) : this(new T[n]) { }
		public EquatableArray(int n, T iv) : this(new T[n]) => Array.Fill(a, iv);

		public T this[int i]
		{
			get => a[i];
			set => a[i] = value;
		}

		public void Fill(T v) => Array.Fill(a, v);
		public void Clear() => Array.Clear(a, 0, a.Length);
		public EquatableArray<T> Clone() => new EquatableArray<T>((T[])a.Clone());

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<T> GetEnumerator() { for (int i = 0; i < a.Length; ++i) yield return a[i]; }

		#region Equality Operators
		public bool Equals(EquatableArray<T> other) => !(other is null) && Equals(a, other.a);
		public static bool Equals(EquatableArray<T> v1, EquatableArray<T> v2) => v1?.Equals(v2) ?? (v2 is null);
		public static bool operator ==(EquatableArray<T> v1, EquatableArray<T> v2) => Equals(v1, v2);
		public static bool operator !=(EquatableArray<T> v1, EquatableArray<T> v2) => !Equals(v1, v2);
		public override bool Equals(object obj) => Equals(obj as EquatableArray<T>);
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
	}
}
