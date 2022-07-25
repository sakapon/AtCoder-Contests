using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var c = Read();
		var x = Read();

		var cmap = Array.ConvertAll(new bool[n + 1], _ => new List<int>());
		for (int i = 0; i < n; i++)
		{
			cmap[c[i]].Add(x[i]);
		}

		var r = InversionNumber(x);
		foreach (var l in cmap)
		{
			if (l.Count == 0) continue;
			r -= InversionNumber(l.ToArray());
		}
		return r;
	}

	public static long InversionNumber(int[] a)
	{
		var r = 0L;
		var set = new SegmentSet<int>(true, a.OrderBy(x => x).ToArray(), 0, 1 << 30);
		foreach (var v in a)
		{
			r += set.Count - set.GetFirstIndex(x => x > v);
			set.Add(v);
		}
		return r;
	}
}

[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
public class SegmentSet<T>
{
	readonly bool multiple;
	readonly int n = 1;
	readonly long[] c;
	readonly T[] a;
	readonly T minItem, maxItem;
	readonly IComparer<T> comparer;

	public SegmentSet(bool multiple, T[] items, T minItem = default, T maxItem = default, IComparer<T> comparer = null)
	{
		this.multiple = multiple;
		this.minItem = minItem;
		this.maxItem = maxItem;
		this.comparer = comparer ?? Comparer<T>.Default;

		while (n < items.Length) n <<= 1;
		c = new long[n << 1];
		a = new T[n << 1];

		Array.Copy(items, 0, a, n, items.Length);
		Array.Fill(a, maxItem, n + items.Length, n - items.Length);
		for (int i = n - 1; i > 0; --i) a[i] = a[i << 1];
	}

	public long Count => c[1];

	// 満たさないものの個数
	public long GetFirstIndex(Func<T, bool> predicate)
	{
		if (predicate(a[1])) return 0;
		var r = 0L;
		var i = 1;
		while ((i & n) == 0) if (!predicate(a[(i <<= 1) | 1])) r += c[i++];
		return r + c[i];
	}
	public long GetLastIndex(Func<T, bool> predicate)
	{
		if (!predicate(a[1])) return -1;
		var r = 0L;
		var i = 1;
		while ((i & n) == 0) if (predicate(a[(i <<= 1) | 1])) r += c[i++];
		return r + c[i] - 1;
	}

	int GetLeafIndex(T item)
	{
		if (comparer.Compare(item, a[1]) < 0) return -1;
		var i = 1;
		while ((i & n) == 0) if (comparer.Compare(item, a[(i <<= 1) | 1]) >= 0) ++i;
		return comparer.Compare(item, a[i]) == 0 ? i : -1;
	}

	public bool Add(T item, long delta = 1)
	{
		var i = GetLeafIndex(item);
		if (i == -1) return false;
		var nc = c[i] + delta;
		if (nc < 0 || !multiple && 1 < nc) return false;
		for (; i > 0; i >>= 1) c[i] += delta;
		return true;
	}
	public bool Remove(T item, long delta = 1) => Add(item, -delta);
}
