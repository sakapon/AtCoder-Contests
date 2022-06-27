using System;
using System.Collections.Generic;
using System.Linq;

namespace CoderLib8.Collections
{
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/8/ITP2/6
	// Test: https://onlinejudge.u-aizu.ac.jp/courses/lesson/1/ALDS1/4/ALDS1_4_B
	// Test: https://atcoder.jp/contests/typical90/tasks/typical90_g
	// Test: https://atcoder.jp/contests/abc143/tasks/abc143_d
	// Test: https://atcoder.jp/contests/abc241/tasks/abc241_f
	// Test: https://atcoder.jp/contests/abc255/tasks/abc255_d
	// sorted multiset として扱われます。
	[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
	public class BSArray<T>
	{
		T[] a;
		public T[] Items => a;
		public int Count { get; }
		public IComparer<T> Comparer { get; }
		public T this[int i] => a[i];

		public BSArray(IEnumerable<T> items, bool useRawItems = false, IComparer<T> comparer = null)
		{
			Comparer = comparer ?? Comparer<T>.Default;
			if (items == null) throw new ArgumentNullException(nameof(items));
			if (useRawItems) a = items as T[] ?? items.ToArray();
			else a = items.OrderBy(x => x, Comparer).ToArray(); // stable sort
			Count = a.Length;
		}

		public int GetFirstIndex(Func<T, bool> predicate) => First(0, Count, i => predicate(a[i]));
		public int GetLastIndex(Func<T, bool> predicate) => Last(-1, Count - 1, i => predicate(a[i]));

		public int GetCount(Func<T, bool> startPredicate, Func<T, bool> endPredicate)
		{
			var c = GetLastIndex(endPredicate) - GetFirstIndex(startPredicate) + 1;
			return c >= 0 ? c : 0;
		}

		public Maybe<T> GetFirst(Func<T, bool> predicate)
		{
			var i = GetFirstIndex(predicate);
			return i < Count ? a[i] : Maybe<T>.None;
		}
		public Maybe<T> GetLast(Func<T, bool> predicate)
		{
			var i = GetLastIndex(predicate);
			return i >= 0 ? a[i] : Maybe<T>.None;
		}

		public int GetFirstIndex(T item)
		{
			var i = GetFirstIndex(x => Comparer.Compare(x, item) >= 0);
			return i < Count && Comparer.Compare(a[i], item) == 0 ? i : -1;
		}
		public int GetLastIndex(T item)
		{
			var i = GetLastIndex(x => Comparer.Compare(x, item) <= 0);
			return i >= 0 && Comparer.Compare(a[i], item) == 0 ? i : -1;
		}

		public bool Contains(T item) => GetFirstIndex(item) != -1;
		public int GetCount(T item) => GetCount(x => Comparer.Compare(x, item) >= 0, x => Comparer.Compare(x, item) <= 0);

		public IEnumerable<T> GetItems(Func<T, bool> startPredicate, Func<T, bool> endPredicate)
		{
			for (var i = GetFirstIndex(startPredicate); i < Count && (endPredicate?.Invoke(a[i]) ?? true); ++i)
				yield return a[i];
		}
		public IEnumerable<T> GetItemsDescending(Func<T, bool> startPredicate, Func<T, bool> endPredicate)
		{
			for (var i = GetLastIndex(endPredicate); i >= 0 && (startPredicate?.Invoke(a[i]) ?? true); --i)
				yield return a[i];
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

	[System.Diagnostics.DebuggerDisplay(@"\{{Value}\}")]
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

		public static explicit operator T(Maybe<T> o) => o.Value;
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
