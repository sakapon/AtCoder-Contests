using System;
using System.Collections.Generic;

namespace CoderLib6.DataTrees.Bsts
{
	// クラスに型引数を指定することで、Create メソッドを呼び出すときに型引数 <T, Tkey> の指定を省略できます。
	public static class Comparer2<T>
	{
		static IComparer<TKey> GetDefault<TKey>()
		{
			if (typeof(TKey) == typeof(string)) return (IComparer<TKey>)StringComparer.Ordinal;
			return Comparer<TKey>.Default;
		}

		public static IComparer<T> Create(bool descending = false)
		{
			var c = GetDefault<T>();
			if (descending) return Comparer<T>.Create((x, y) => c.Compare(y, x));
			else return c;
		}

		public static IComparer<T> Create<TKey>(Func<T, TKey> keySelector, bool descending = false)
		{
			if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
			var c = GetDefault<TKey>();
			if (descending) return Comparer<T>.Create((x, y) => c.Compare(keySelector(y), keySelector(x)));
			else return Comparer<T>.Create((x, y) => c.Compare(keySelector(x), keySelector(y)));
		}

		public static IComparer<T> Create<TKey1, TKey2>(Func<T, TKey1> keySelector1, Func<T, TKey2> keySelector2)
		{
			if (keySelector1 == null) throw new ArgumentNullException(nameof(keySelector1));
			if (keySelector2 == null) throw new ArgumentNullException(nameof(keySelector2));
			var c1 = GetDefault<TKey1>();
			var c2 = GetDefault<TKey2>();
			return Comparer<T>.Create((x, y) =>
			{
				var d = c1.Compare(keySelector1(x), keySelector1(y));
				if (d != 0) return d;
				return c2.Compare(keySelector2(x), keySelector2(y));
			});
		}
	}
}
