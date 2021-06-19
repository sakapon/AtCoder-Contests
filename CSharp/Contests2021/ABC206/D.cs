using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	const int max = 200000;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var d = Tally(a, max);
		var uf = new UF(n);

		for (int x = 0; x <= max; x++)
		{
			if (d[x].Count == 0) continue;

			var root = d[x][0];
			foreach (var i in d[x])
			{
				uf.Unite(root, i);
			}
		}

		var r = 0;

		for (int i = 0; i < n; i++)
		{
			if (uf.Unite(i, n - 1 - i))
			{
				r++;
			}
		}

		return r;
	}

	static List<int>[] Tally(int[] a, int max)
	{
		var d = Array.ConvertAll(new bool[max + 1], _ => new List<int>());

		for (int i = 0; i < a.Length; i++)
		{
			d[a[i]].Add(i);
		}
		return d;
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
