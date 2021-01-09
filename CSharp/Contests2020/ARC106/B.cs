using System;
using System.Linq;

class B
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0], m = h[1];
		var a = ReadL();
		var b = ReadL();
		var es = Array.ConvertAll(new int[m], _ => Read());

		var uf = new UF(n);
		foreach (var e in es)
		{
			uf.Unite(e[0] - 1, e[1] - 1);
		}

		Console.WriteLine(uf.ToGroups().All(g => g.Sum(i => a[i]) == g.Sum(i => b[i])) ? "Yes" : "No");
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
