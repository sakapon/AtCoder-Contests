using System;

namespace CoderLib8.Collections
{
	// Test: https://atcoder.jp/contests/abc241/tasks/abc241_f
	public class BSArray<T>
	{
		T[] a;
		public T[] Raw => a;
		public int Count => a.Length;
		public T this[int i] => a[i];
		public BSArray(T[] array) { a = array; }

		public int GetFirstIndex(Func<T, bool> predicate) => First(0, Count, i => predicate(a[i]));
		public int GetLastIndex(Func<T, bool> predicate) => Last(-1, Count - 1, i => predicate(a[i]));

		public Maybe<T> GetFirst(Func<T, bool> predicate)
		{
			var i = GetFirstIndex(predicate);
			return i != Count ? a[i] : Maybe<T>.None;
		}
		public Maybe<T> GetLast(Func<T, bool> predicate)
		{
			var i = GetLastIndex(predicate);
			return i != -1 ? a[i] : Maybe<T>.None;
		}

		static int First(int l, int r, Func<int, bool> f)
		{
			int m;
			while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
			return r;
		}
		static int Last(int l, int r, Func<int, bool> f)
		{
			int m;
			while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
			return l;
		}
	}

	public struct Maybe<T>
	{
		public static readonly Maybe<T> None = new Maybe<T>();

		T v;
		public bool HasValue { get; }
		public Maybe(T value)
		{
			v = value;
			HasValue = true;
		}

		public static explicit operator T(Maybe<T> value) => value.Value;
		public static implicit operator Maybe<T>(T value) => new Maybe<T>(value);

		public T Value => HasValue ? v : throw new InvalidOperationException("This has no value.");
		public T GetValueOrDefault(T defaultValue = default(T)) => HasValue ? v : defaultValue;
		public bool TryGetValue(out T value)
		{
			value = HasValue ? v : default(T);
			return HasValue;
		}
	}
}
