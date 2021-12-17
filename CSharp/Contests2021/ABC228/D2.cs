using System;
using System.Linq;

class D2
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => ReadL());

		const int n = 1 << 20;
		var a = new long[n];
		Array.Fill(a, -1);

		var uf = new UF<int>(n, Math.Max, Enumerable.Range(1, n).ToArray());

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		foreach (var q in qs)
		{
			var x = q[1];
			var h = (int)(x % n);

			if (q[0] == 1)
			{
			det_h:
				if (a[h] != -1)
				{
					h = uf.GetValue(h);
					if (h == n)
					{
						h = 0;
						goto det_h;
					}
				}

				if (h > 0 && a[h - 1] != -1) uf.Unite(h, h - 1);
				if (h + 1 < n && a[h + 1] != -1) uf.Unite(h, h + 1);

				a[h] = x;
			}
			else
			{
				Console.WriteLine(a[h]);
			}
		}
		Console.Out.Flush();
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
