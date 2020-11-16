using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		var h = Read();
		int n = h[0], qc = h[1];
		var c = Read();
		var qs = Array.ConvertAll(new bool[qc], _ => Read());

		var iv = c.Prepend(0).Select(id => new Dictionary<int, int> { { id, 1 } }).ToArray();
		var uf = new UF<Dictionary<int, int>>(n + 1, Merge, iv);

		foreach (var q in qs)
		{
			if (q[0] == 1)
			{
				uf.Unite(q[1], q[2]);
			}
			else
			{
				var d = uf.GetValue(q[1]);
				Console.WriteLine(d.ContainsKey(q[2]) ? d[q[2]] : 0);
			}
		}
		Console.Out.Flush();
	}

	// 項目数が多いほうにマージします。
	static Dictionary<TK, int> Merge<TK>(Dictionary<TK, int> d1, Dictionary<TK, int> d2)
	{
		if (d1.Count < d2.Count) (d1, d2) = (d2, d1);
		foreach (var (k, v) in d2)
			if (d1.ContainsKey(k)) d1[k] += v;
			else d1[k] = v;
		return d1;
	}
}

class UF<T>
{
	int[] p, sizes;
	T[] a;
	public Func<T, T, T> Merge;

	// (parent, child) -> result
	public UF(int n, Func<T, T, T> merge, T[] a0)
	{
		p = Enumerable.Range(0, n).ToArray();
		sizes = Array.ConvertAll(new bool[n], _ => 1);
		a = a0;
		Merge = merge;
	}

	public void Unite(int x, int y)
	{
		var px = GetRoot(x);
		var py = GetRoot(y);
		if (px == py) return;

		// 要素数が多いほうのグループに合流します。
		if (sizes[px] < sizes[py]) (px, py) = (py, px);
		p[py] = px;
		sizes[px] += sizes[py];
		a[px] = Merge(a[px], a[py]);
	}
	public bool AreUnited(int x, int y) => GetRoot(x) == GetRoot(y);
	public int GetRoot(int x) => p[x] == x ? x : p[x] = GetRoot(p[x]);
	public int GetSize(int x) => sizes[GetRoot(x)];
	public T GetValue(int x) => a[GetRoot(x)];
}
