using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, qc, x0) = Read3();
		var w = ReadL();
		var qs = Array.ConvertAll(new bool[qc], _ => ReadL()[0]);

		var wsum = w.Sum();
		var rsq = new StaticRSQ1(w.Concat(w).ToArray());
		var bsa = new BSArray<long>(rsq.Raw, true);

		// 開始位置の周期
		var period = new List<int>();
		var u = new bool[n];

		// potato i から開始したときの個数
		var counts = new long[n];
		var turns = Math.DivRem(x0, wsum, out var x);
		Array.Fill(counts, turns * n);

		var pi = 0;
		do
		{
			period.Add(pi);
			u[pi] = true;

			var ni = bsa.GetFirstIndex(v => v >= bsa[pi] + x);
			counts[pi] += ni - pi;
			pi = ni % n;
		}
		while (!u[pi]);

		// ループを開始する位置
		var li = period.IndexOf(pi);

		return string.Join("\n", qs.Select(k =>
		{
			if (k - 1 < li) return counts[period[(int)k - 1]];
			return counts[period[li + (int)((k - 1 - li) % (period.Count - li))]];
		}));
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
