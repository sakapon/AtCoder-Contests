using System;
using System.Linq;

class E
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(s => int.Parse(s) - 1).ToArray();

		var uf = new UF(n);
		for (int i = 0; i < n; i++)
			uf.Unite(i, a[i]);
		var c = Enumerable.Range(0, n).GroupBy(uf.GetRoot).ToDictionary(g => g.Key, g => g.Count());
		Console.WriteLine(string.Join(" ", Enumerable.Range(0, n).Select(i => c[uf.GetRoot(i)])));
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
