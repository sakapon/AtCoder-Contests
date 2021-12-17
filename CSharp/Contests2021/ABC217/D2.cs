using System;
using System.Collections.Generic;
using System.Linq;

class D2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int c, int x) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (l, qc) = Read2();
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());
		Array.Reverse(qs);

		var seps = qs.Where(q => q.c == 1).Select(q => q.x).Prepend(0).Append(l).ToArray();
		Array.Sort(seps);
		var map = ToInverseMap(seps);

		var r = new List<int>();
		var uf = new UF<int>(seps.Length, (x, y) => x + y,
			Enumerable.Range(0, seps.Length - 1).Select(i => seps[i + 1] - seps[i]).ToArray());

		foreach (var (c, x) in qs)
		{
			if (c == 1)
			{
				uf.Unite(map[x] - 1, map[x]);
			}
			else
			{
				var i = Last(-1, seps.Length - 1, xi => seps[xi] <= x);
				r.Add(uf.GetValue(i));
			}
		}

		r.Reverse();
		Console.WriteLine(string.Join("\n", r));
	}

	public static Dictionary<T, int> ToInverseMap<T>(T[] a)
	{
		var d = new Dictionary<T, int>();
		for (int i = 0; i < a.Length; ++i) d[a[i]] = i;
		return d;
	}

	static int Last(int l, int r, Func<int, bool> f)
	{
		int m;
		while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
		return l;
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
		if ((x = GetRoot(x)) == (y = GetRoot(y))) return false;

		// 要素数が大きいほうのグループにマージします。
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
	public int[][] ToGroups() => Enumerable.Range(0, p.Length).GroupBy(GetRoot).Select(g => g.ToArray()).ToArray();
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
