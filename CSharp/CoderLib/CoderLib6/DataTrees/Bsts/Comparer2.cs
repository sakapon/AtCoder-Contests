using System;
using System.Collections.Generic;

namespace CoderLib6.DataTrees.Bsts
{
	// クラスに型引数を指定することで、Create メソッドを呼び出すときに型引数 <T, Tkey> の指定を省略できます。
	public static class Comparer2<T>
	{
		public static IComparer<T> GetDefault()
		{
			if (typeof(T) == typeof(string)) return (IComparer<T>)StringComparer.Ordinal;
			return Comparer<T>.Default;
		}

		public static IComparer<T> Create(bool descending = false)
		{
			var c = GetDefault();
			if (descending)
				return Comparer<T>.Create((x, y) => c.Compare(y, x));
			else
				return c;
		}

		public static IComparer<T> Create<TKey>(Func<T, TKey> keySelector, bool descending = false)
		{
			if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

			var c = Comparer2<TKey>.GetDefault();
			if (descending)
				return Comparer<T>.Create((x, y) => c.Compare(keySelector(y), keySelector(x)));
			else
				return Comparer<T>.Create((x, y) => c.Compare(keySelector(x), keySelector(y)));
		}

		public static IComparer<T> Create<TKey1, TKey2>(Func<T, TKey1> keySelector1, Func<T, TKey2> keySelector2)
		{
			if (keySelector1 == null) throw new ArgumentNullException(nameof(keySelector1));
			if (keySelector2 == null) throw new ArgumentNullException(nameof(keySelector2));

			var c1 = Comparer2<TKey1>.GetDefault();
			var c2 = Comparer2<TKey2>.GetDefault();
			return Comparer<T>.Create((x, y) =>
			{
				var d = c1.Compare(keySelector1(x), keySelector1(y));
				if (d != 0) return d;
				return c2.Compare(keySelector2(x), keySelector2(y));
			});
		}

		public static IComparer<T> Create<TKey1, TKey2, TKey3>(Func<T, TKey1> keySelector1, Func<T, TKey2> keySelector2, Func<T, TKey3> keySelector3)
		{
			if (keySelector1 == null) throw new ArgumentNullException(nameof(keySelector1));
			if (keySelector2 == null) throw new ArgumentNullException(nameof(keySelector2));
			if (keySelector3 == null) throw new ArgumentNullException(nameof(keySelector3));

			var c1 = Comparer2<TKey1>.GetDefault();
			var c2 = Comparer2<TKey2>.GetDefault();
			var c3 = Comparer2<TKey3>.GetDefault();
			return Comparer<T>.Create((x, y) =>
			{
				var d = c1.Compare(keySelector1(x), keySelector1(y));
				if (d != 0) return d;
				d = c2.Compare(keySelector2(x), keySelector2(y));
				if (d != 0) return d;
				return c3.Compare(keySelector3(x), keySelector3(y));
			});
		}
	}
}
