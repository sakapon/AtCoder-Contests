using System;
using System.Collections.Generic;

class Q089
{
	const long M = 1000000007;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = ((int, long))Read2L();
		var a = Read();

		var compMap = new CompressionHashMap(a);
		a = compMap.Compressed;
		a = Array.ConvertAll(a, x => x + 1);

		var left = GetLeft(n, k, a);

		var bit = new BIT(n + 1);
		bit[1] = 1;
		for (int i = 0; i < n; i++)
		{
			bit[i + 2] = bit.Sum(left[i] + 1, i + 2) % M;
		}
		return bit[n + 1];
	}

	// i 以下のインデックスのうち、交換回数が k 以下となる最小値
	static int[] GetLeft(int n, long k, int[] a)
	{
		var left = new int[n];
		var bit = new BIT(n);
		var inv = 0L;
		var ui = new bool[n];
		var uj = new bool[n];

		var q = TwoPointers(n, n, (i, j) =>
		{
			if (!uj[j] && j > 0)
			{
				inv -= bit.Sum(1, a[j - 1]);
				bit.Add(a[j - 1], -1);
			}

			if (!ui[i])
			{
				inv += bit.Sum(a[i] + 1, n + 1);
				bit.Add(a[i], 1);
			}

			ui[i] = uj[j] = true;
			return inv <= k;
		});
		foreach (var (i, j) in q)
			left[i] = j;

		return left;
	}

	static IEnumerable<(int i, int j)> TwoPointers(int n1, int n2, Func<int, int, bool> predicate)
	{
		for (int i = 0, j = 0; i < n1 && j < n2; ++i)
			for (; j < n2; ++j)
				if (predicate(i, j)) { yield return (i, j); break; }
	}
}

class CompressionHashMap
{
	public int[] Raw { get; }
	public int[] ReverseMap { get; }
	public Dictionary<int, int> Map { get; }
	public int this[int v] => Map[v];
	public int Count => ReverseMap.Length;

	int[] c;
	public int[] Compressed => c ??= Array.ConvertAll(Raw, v => Map[v]);

	public CompressionHashMap(int[] a)
	{
		// r = a.Distinct().OrderBy(v => v).ToArray();
		var hs = new HashSet<int>();
		foreach (var v in a) hs.Add(v);
		var r = new int[hs.Count];
		hs.CopyTo(r);
		Array.Sort(r);
		var map = new Dictionary<int, int>();
		for (int i = 0; i < r.Length; ++i) map[r[i]] = i;

		(Raw, ReverseMap, Map) = (a, r, map);
	}
}

class BIT
{
	// Power of 2
	int n2 = 1;
	long[] a;

	public BIT(int n)
	{
		while (n2 < n) n2 <<= 1;
		a = new long[n2 + 1];
	}

	public long this[int i]
	{
		get { return Sum(i) - Sum(i - 1); }
		set { Add(i, value - this[i]); }
	}

	public void Add(int i, long v)
	{
		for (; i <= n2; i += i & -i) a[i] += v;
	}

	public long Sum(int l_in, int r_ex) => Sum(r_ex - 1) - Sum(l_in - 1);
	public long Sum(int r_in)
	{
		var r = 0L;
		for (var i = r_in; i > 0; i -= i & -i) r += a[i];
		return r;
	}
}
