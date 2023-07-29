﻿using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read2());
		var k = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[k], _ => Read2());
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		var uf = new UF(n + 1);
		foreach (var (u, v) in es)
		{
			uf.Unite(u, v);
		}

		var set = new HashSet<(int, int)>();
		foreach (var (x, y) in ps)
		{
			set.Add((uf.GetRoot(x), uf.GetRoot(y)));
			set.Add((uf.GetRoot(y), uf.GetRoot(x)));
		}

		var r = new List<bool>();
		foreach (var (x, y) in qs)
		{
			r.Add(!set.Contains((uf.GetRoot(x), uf.GetRoot(y))));
		}

		return string.Join("\n", r.Select(b => b ? "Yes" : "No"));
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
