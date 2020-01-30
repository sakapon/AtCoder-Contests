using System;
using System.Linq;

class K
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = new int[n].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray();

		var d = a.SelectMany(x => x).Distinct().OrderBy(x => x).Select((x, i) => new { x, i }).ToDictionary(_ => _.x, _ => _.i);
		var u = new int[d.Count];
		var uf = new UF(d.Count);

		foreach (var x in a)
		{
			uf.Unite(d[x[0]], d[x[1]]);
			u[d[x[0]]]++;
		}
		Console.WriteLine(uf.ToGroups().Sum(g => Math.Min(g.Length, g.Sum(x => u[x]))));
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
