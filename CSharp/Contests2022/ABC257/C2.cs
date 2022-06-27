using System;
using System.Collections.Generic;
using System.Linq;

class C2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine();
		var w = Read();

		var ch = w.Where((x, i) => s[i] == '0').ToArray();
		var ad = w.Where((x, i) => s[i] == '1').ToArray();
		Array.Sort(ch);
		Array.Sort(ad);

		var bs_ch = new BSArray<int>(ch, true);
		var bs_ad = new BSArray<int>(ad, true);

		var r = ch.Length;
		foreach (var x in w)
		{
			var f = bs_ch.GetFirstIndex(v => v >= x) + ad.Length - bs_ad.GetFirstIndex(v => v >= x);
			r = Math.Max(r, f);
		}
		return r;
	}
}

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
