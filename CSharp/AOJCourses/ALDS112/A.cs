using System;
using System.Collections.Generic;
using System.Linq;

class A
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = new int[n].Select(_ => Console.ReadLine().Trim().Split().Select(int.Parse).ToArray()).ToArray();

		var es = new List<int[]>();
		for (int i = 0; i < n; i++)
			for (int j = i + 1; j < n; j++)
				if (a[i][j] != -1)
					es.Add(new[] { i, j, a[i][j] });

		var r = Kruskal(n, es.ToArray());
		Console.WriteLine(r.Sum(e => e[2]));
	}

	static int[][] Kruskal(int n, int[][] es)
	{
		var uf = new UF(n + 1);
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
