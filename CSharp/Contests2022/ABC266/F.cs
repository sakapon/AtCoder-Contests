using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n], _ => Read());
		var qc = int.Parse(Console.ReadLine());
		var sb = new StringBuilder();

		var map = Array.ConvertAll(new bool[n + 1], _ => new HashSet<int>());
		foreach (var e in es)
		{
			map[e[0]].Add(e[1]);
			map[e[1]].Add(e[0]);
		}

		var q = new Queue<int>();
		for (int v = 1; v <= n; v++)
		{
			if (map[v].Count == 1) q.Enqueue(v);
		}

		var uf = new UF(n + 1);
		while (q.TryDequeue(out var v))
		{
			var nv = map[v].First();
			uf.Unite(v, nv);
			map[nv].Remove(v);
			if (map[nv].Count == 1) q.Enqueue(nv);
		}

		while (qc-- > 0)
		{
			var (x, y) = Read2();
			sb.AppendLine(uf.AreUnited(x, y) ? "Yes" : "No");
		}
		Console.Write(sb);
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
