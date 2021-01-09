using System;
using System.Linq;

class D
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		var n = h[0];
		var uf = new UF(n);

		foreach (var r in new int[h[1]].Select(_ => Read())) uf.Unite(r[0], r[1]);

		var q = int.Parse(Console.ReadLine());
		Console.WriteLine(string.Join("\n", new int[q].Select(_ => Read()).Select(r => uf.AreUnited(r[0], r[1]) ? "yes" : "no")));
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
