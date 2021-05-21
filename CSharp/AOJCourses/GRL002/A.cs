using System;
using System.Collections.Generic;
using System.Linq;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		var es = Array.ConvertAll(new bool[h[1]], _ => Read());

		Console.WriteLine(Kruskal(h[0], es).Sum(e => e[2]));
	}

	static int[][] Kruskal(int n, int[][] es)
	{
		var uf = new UF(n);
		var minEdges = new List<int[]>();

		foreach (var e in es.OrderBy(e => e[2]))
		{
			if (uf.AreUnited(e[0], e[1])) continue;
			uf.Unite(e[0], e[1]);
			minEdges.Add(e);
		}
		return minEdges.ToArray();
	}
}

class UF
{
	int[] p;
	public UF(int n) { p = Enumerable.Range(0, n).ToArray(); }

	public void Unite(int a, int b) { if (!AreUnited(a, b)) p[p[b]] = p[a]; }
	public bool AreUnited(int a, int b) => GetRoot(a) == GetRoot(b);
	public int GetRoot(int a) => p[a] == a ? a : p[a] = GetRoot(p[a]);
}
