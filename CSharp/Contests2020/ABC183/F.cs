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
	int[] p;
	T[] a;
	public Func<T, T, T> Merge;

	// (parent, child) -> result
	public UF(int n, Func<T, T, T> merge, T[] a)
	{
		p = Enumerable.Range(0, n).ToArray();
		this.a = a;
		Merge = merge;
	}

	public void Unite(int x, int y)
	{
		if (AreUnited(x, y)) return;
		a[p[x]] = Merge(a[p[x]], a[p[y]]);
		p[p[y]] = p[x];
	}
	public bool AreUnited(int x, int y) => GetRoot(x) == GetRoot(y);
	public int GetRoot(int x) => p[x] == x ? x : p[x] = GetRoot(p[x]);
	public T GetValue(int x) => a[GetRoot(x)];
}
