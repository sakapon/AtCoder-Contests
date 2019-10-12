using System;
using System.Linq;

class F
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();
		var dx = ps.Select((p, i) => new { p, i }).ToLookup(_ => _.p[0]);
		var dy = ps.Select((p, i) => new { p, i }).ToLookup(_ => _.p[1]);

		var uf = new UF(n);
		foreach (var g in dx)
		{
			if (g.Count() == 1) continue;
			var p0 = g.First();
			foreach (var p in g) uf.Unite(p0.i, p.i);
		}
		foreach (var g in dy)
		{
			if (g.Count() == 1) continue;
			var p0 = g.First();
			foreach (var p in g) uf.Unite(p0.i, p.i);
		}

		var r = 0L;
		foreach (var g in uf.ToGroups())
		{
			if (g.Count() == dx[ps[g.First()][0]].Count()) continue;
			r += (long)g.Select(i => ps[i][0]).Distinct().Count() * g.Select(i => ps[i][1]).Distinct().Count() - g.Count();
		}
		Console.WriteLine(r);
	}
}

class UF
{
	int[] p;
	public UF(int n) { p = Enumerable.Range(0, n).ToArray(); }

	public void Unite(int a, int b) { if (!AreUnited(a, b)) p[p[b]] = p[a]; }
	public bool AreUnited(int a, int b) => GetRoot(a) == GetRoot(b);
	int GetRoot(int a) => p[a] == a ? a : p[a] = GetRoot(p[a]);
	public int[][] ToGroups() => Enumerable.Range(0, p.Length).GroupBy(GetRoot).Select(g => g.ToArray()).ToArray();
}
