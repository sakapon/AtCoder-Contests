using System;
using System.Linq;

class E
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).Select(x => x - 1).ToArray();

		var uf = new UF(n);
		for (int i = 0; i < n; i++)
			uf.Unite(i, a[i]);
		var q = uf.ToGroups().SelectMany(g => g.Select(x => (x, c: g.Length))).OrderBy(_ => _.x).Select(_ => _.c);
		Console.WriteLine(string.Join(" ", q));
	}
}

class UF
{
	int[] p;
	public UF(int n) { p = Enumerable.Range(0, n).ToArray(); }

	public void Unite(int a, int b) { if (!AreUnited(a, b)) p[p[b]] = p[a]; }
	public bool AreUnited(int a, int b) => GetRoot(a) == GetRoot(b);
	public int GetRoot(int a) => p[a] == a ? a : p[a] = GetRoot(p[a]);
	public int[][] ToGroups() => Enumerable.Range(0, p.Length).GroupBy(GetRoot).Select(g => g.ToArray()).ToArray();
}
