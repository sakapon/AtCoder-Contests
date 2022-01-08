using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.DataTrees.Bsts;

class M
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, qc) = Read2();
		var s = Console.ReadLine().Split();
		var qs = Array.ConvertAll(new bool[qc], _ => Console.ReadLine().Split());

		var comp = ComparerHelper<(int id, string name)>.Create(p => p.name, false, p => p.id, false);
		var set = new WBSet<(int id, string name)>(comp);

		for (int id = 0; id < n; id++)
		{
			set.Add((id, s[id]));
		}

		foreach (var q in qs)
		{
			var o = int.Parse(q[0]) - 1;
			var (id, _) = set.RemoveAt(o);
			set.Add((id, q[1]));
		}

		return string.Join(" ", set.OrderBy(p => p.id).Select(p => p.name));
	}
}

public static class ComparerHelper
{
	public static IComparer<T> GetDefault<T>()
	{
		// カルチャに依存しない場合に高速化します。
		if (typeof(T) == typeof(string)) return (IComparer<T>)StringComparer.Ordinal;
		return Comparer<T>.Default;
	}

	public static IComparer<T> ToDescending<T>(this IComparer<T> c)
	{
		if (c == null) throw new ArgumentNullException(nameof(c));
		return Comparer<T>.Create((x, y) => c.Compare(y, x));
	}
}

// クラスに型引数を指定することで、Create メソッドを呼び出すときに型引数 <T, Tkey> の指定を省略できます。
public static class ComparerHelper<T>
{
	public static IComparer<T> Create(bool descending = false)
	{
		var c = ComparerHelper.GetDefault<T>();
		return descending ? c.ToDescending() : c;
	}

	public static IComparer<T> Create<TKey>(Func<T, TKey> keySelector, bool descending = false)
	{
		if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
		var c = ComparerHelper<TKey>.Create(descending);
		return Comparer<T>.Create((x, y) => c.Compare(keySelector(x), keySelector(y)));
	}

	public static IComparer<T> Create<TKey1, TKey2>(Func<T, TKey1> keySelector1, bool descending1, Func<T, TKey2> keySelector2, bool descending2)
	{
		if (keySelector1 == null) throw new ArgumentNullException(nameof(keySelector1));
		if (keySelector2 == null) throw new ArgumentNullException(nameof(keySelector2));
		var c1 = ComparerHelper<TKey1>.Create(descending1);
		var c2 = ComparerHelper<TKey2>.Create(descending2);
		return Comparer<T>.Create((x, y) =>
		{
			var d = c1.Compare(keySelector1(x), keySelector1(y));
			if (d != 0) return d;
			return c2.Compare(keySelector2(x), keySelector2(y));
		});
	}
}

namespace CoderLib6.DataTrees.Bsts
{
	public class WBSet<T> : IEnumerable<T>
	{
		public IComparer<T> Comparer { get; }

		public WBSet(IComparer<T> comparer = null)
		{
			Comparer = comparer ?? Comparer<T>.Default;
		}

		public IEnumerator<T> GetEnumerator() => throw new NotImplementedException();
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => throw new NotImplementedException();

		public bool Add(T item) => throw new NotImplementedException();
		public T RemoveAt(int index) => throw new NotImplementedException();
	}
}
