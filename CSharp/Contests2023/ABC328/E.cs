using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Linq;
using Num = System.Int64;

class E
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m, k) = ReadL().ToTuple3<int, int, long>();
		var es = Array2.Create1ByFunc(m, () => ReadL().ToTuple3<int, int, long>());

		var r = 1L << 60;
		Combination(m, n - 1, p =>
		{
			if (!IsConnectedTree(p)) return false;
			Chmin(ref r, p.Sum(i => es[i].Item3) % k);
			return false;
		});
		return r;

		bool IsConnectedTree(int[] p)
		{
			var uf = new UF(n);
			foreach (var i in p)
			{
				uf.Unite(es[i].Item1 - 1, es[i].Item2 - 1);
			}
			return uf.GroupsCount == 1;
		}
	}

	public static long Chmin(ref long x, long v) => x > v ? x = v : x;

	public static void Combination(int n, int r, Func<int[], bool> action)
	{
		var p = new int[r];
		DFS(0, 0);

		bool DFS(int v, int si)
		{
			if (v == r) return action(p);

			for (int i = si; r - v <= n - i; ++i)
			{
				p[v] = i;
				if (DFS(v + 1, i + 1)) return true;
			}
			return false;
		}
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
