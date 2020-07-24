using System;
using System.Linq;

class D
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		var n = h[0];
		var ab = new int[h[1]].Select(_ => Read()).ToArray();
		var cd = new int[h[2]].Select(_ => Read()).ToArray();

		var uf = new UF(n + 1);
		foreach (var x in ab)
			uf.Unite(x[0], x[1]);

		var roots = Enumerable.Range(0, n + 1).GroupBy(uf.GetRoot).ToDictionary(g => g.Key, g => g.Count());
		var c = Enumerable.Range(0, n + 1).Select(uf.GetRoot).Select(x => roots[x] - 1).ToArray();

		foreach (var x in ab)
		{
			c[x[0]]--;
			c[x[1]]--;
		}
		foreach (var x in cd)
		{
			if (!uf.AreUnited(x[0], x[1])) continue;
			c[x[0]]--;
			c[x[1]]--;
		}
		Console.WriteLine(string.Join(" ", c.Skip(1)));
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
