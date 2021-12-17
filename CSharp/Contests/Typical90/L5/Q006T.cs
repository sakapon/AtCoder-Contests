using System;
using System.Collections.Generic;
using CoderLib6.DataTrees.Bsts;

class Q006T
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();
		var s = Console.ReadLine();

		var r = new List<char>();

		var comp = Comparer2<int>.Create(i => s[i], i => i);
		var set = new IndexedMultiSet<int>(comp);
		for (int i = 0; i < n - k; i++)
		{
			set.Add(i);
		}

		var t = -1;
		for (int i = n - k; i < n; i++)
		{
			set.Add(i);
			while (set.GetFirst() < t) set.RemoveAt(0);

			t = set.RemoveAt(0);
			r.Add(s[t]);
		}

		return string.Join("", r);
	}
}

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
	}
}

namespace CoderLib6.DataTrees.Bsts
{
	public class IndexedMultiSet<T>
	{
		public IndexedMultiSet(IComparer<T> comparer = null) { }
		public T GetFirst() => throw new NotImplementedException();
		public T GetLast() => throw new NotImplementedException();
		public bool Contains(T item) => throw new NotImplementedException();
		public bool Add(T item) => throw new NotImplementedException();
		public T RemoveAt(int index) => throw new NotImplementedException();
	}
}
