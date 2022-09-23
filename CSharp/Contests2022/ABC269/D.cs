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
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		const int Size = 2020;
		const int Offset = 1010;

		var u = new int[Size, Size];
		for (int i = 0; i < Size; i++)
			for (int j = 0; j < Size; j++)
				u[i, j] = -1;

		var uf = new UF(n);

		for (int pi = 0; pi < n; pi++)
		{
			var (i, j) = ps[pi];
			i += Offset;
			j += Offset;

			u[i, j] = pi;

			if (u[i - 1, j - 1] != -1) uf.Unite(pi, u[i - 1, j - 1]);
			if (u[i - 1, j] != -1) uf.Unite(pi, u[i - 1, j]);
			if (u[i, j - 1] != -1) uf.Unite(pi, u[i, j - 1]);
			if (u[i, j + 1] != -1) uf.Unite(pi, u[i, j + 1]);
			if (u[i + 1, j] != -1) uf.Unite(pi, u[i + 1, j]);
			if (u[i + 1, j + 1] != -1) uf.Unite(pi, u[i + 1, j + 1]);
		}
		return uf.GroupsCount;
	}
}

public class UF
{
	int[] p, sizes;
	public int GroupsCount { get; private set; }

	public UF(int n)
	{
		p = Array.ConvertAll(new bool[n], _ => -1);
		sizes = Array.ConvertAll(p, _ => 1);
		GroupsCount = n;
	}

	public int GetRoot(int x) => p[x] == -1 ? x : p[x] = GetRoot(p[x]);
	public bool AreUnited(int x, int y) => GetRoot(x) == GetRoot(y);
	public int GetSize(int x) => sizes[GetRoot(x)];

	public bool Unite(int x, int y)
	{
		if ((x = GetRoot(x)) == (y = GetRoot(y))) return false;

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
	public ILookup<int, int> ToGroups() => Enumerable.Range(0, p.Length).ToLookup(GetRoot);
}
