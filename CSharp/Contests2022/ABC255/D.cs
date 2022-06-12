using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, qc) = Read2();
		var a = ReadL();
		var qs = Array.ConvertAll(new bool[qc], _ => long.Parse(Console.ReadLine()));

		Array.Sort(a);
		var rsq = new StaticRSQ1(a);
		var bsa = new BSArray<long>(a);

		return string.Join("\n", qs.Select(x =>
		{
			var i = bsa.GetFirstIndex(v => v >= x);
			var r = 0L;
			r += rsq.GetSum(i, n) - x * (n - i);
			r += x * i - rsq.GetSum(0, i);
			return r;
		}));
	}
}

public class StaticRSQ1
{
	int n;
	long[] s;
	public long[] Raw => s;
	public StaticRSQ1(long[] a)
	{
		n = a.Length;
		s = new long[n + 1];
		for (int i = 0; i < n; ++i) s[i + 1] = s[i] + a[i];
	}

	// [l, r)
	// 範囲外のインデックスも可。
	public long GetSum(int l, int r)
	{
		if (r < 0 || n < l) return 0;
		if (l < 0) l = 0;
		if (n < r) r = n;
		return s[r] - s[l];
	}
}

[System.Diagnostics.DebuggerDisplay(@"Count = {Count}")]
public class BSArray<T>
{
	T[] a;
	public T[] Raw => a;
	public int Count { get; }
	public T this[int i] => a[i];
	public BSArray(T[] array) { a = array; Count = a.Length; }

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
