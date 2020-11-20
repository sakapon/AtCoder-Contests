using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		var h = Read();
		int n = h[0], qc = h[1];
		var c = Read();
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		var iv = c.Prepend(0).Select(ci => new Dictionary<int, int> { { ci, 1 } }).ToArray();
		var uf = new UF<Dictionary<int, int>>(n + 1, CollectionHelper.Merge, iv);

		foreach (var q in qs)
		{
			if (q[0] == 1)
			{
				uf.Unite(q[1], q[2]);
			}
			else
			{
				Console.WriteLine(uf.GetValue(q[1]).GetValue(q[2]));
			}
		}
		Console.Out.Flush();
	}
}

static class CollectionHelper
{
	public static TV GetValue<TK, TV>(this Dictionary<TK, TV> d, TK k, TV v0 = default) => d.ContainsKey(k) ? d[k] : v0;

	// 項目数が大きいほうにマージします。
	public static Dictionary<TK, int> Merge<TK>(Dictionary<TK, int> d1, Dictionary<TK, int> d2)
	{
		if (d1.Count < d2.Count) (d1, d2) = (d2, d1);
		foreach (var (k, v) in d2)
			if (d1.ContainsKey(k)) d1[k] += v;
			else d1[k] = v;
		return d1;
	}
}

class UF
{
	int[] p, sizes;
	public int GroupsCount;
	public UF(int n)
	{
		p = Enumerable.Range(0, n).ToArray();
		sizes = Array.ConvertAll(p, _ => 1);
		GroupsCount = n;
	}

	public int GetRoot(int x) => p[x] == x ? x : p[x] = GetRoot(p[x]);
	public int GetSize(int x) => sizes[GetRoot(x)];

	public bool AreUnited(int x, int y) => GetRoot(x) == GetRoot(y);
	public bool Unite(int x, int y)
	{
		x = GetRoot(x);
		y = GetRoot(y);
		if (x == y) return false;

		// 要素数が大きいほうのグループに合流します。
		if (sizes[x] < sizes[y]) Merge(y, x);
		else Merge(x, y);
		return true;
	}
	protected virtual void Merge(int x, int y)
	{
		p[y] = x;
		sizes[x] += sizes[y];
		--GroupsCount;
	}
}

class UF<T> : UF
{
	T[] a;
	// (parent, child) => result
	Func<T, T, T> MergeData;
	public UF(int n, Func<T, T, T> merge, T[] a0) : base(n)
	{
		a = a0;
		MergeData = merge;
	}

	public T GetValue(int x) => a[GetRoot(x)];
	protected override void Merge(int x, int y)
	{
		base.Merge(x, y);
		a[x] = MergeData(a[x], a[y]);
	}
}
