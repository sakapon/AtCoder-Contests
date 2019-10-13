using System;
using System.Linq;

class B
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		var ps = new int[h[0]].Select(_ => read()).ToArray();
		var cs = new int[h[1]].Select(_ => read()).ToArray();

		var uf = new UF(h[0]);
		foreach (var c in cs) uf.Unite(c[0] - 1, c[1] - 1);
		ps = uf.ToGroups().Select(g => new[] { g.Sum(x => ps[x][0]), g.Sum(x => ps[x][1]) }).ToArray();

		var v = Enumerable.Repeat(-1, h[2] + 1).ToArray();
		v[0] = 0;
		foreach (var p in ps)
			for (int i = v.Length - 1; i >= 0; i--)
				if (v[i] != -1 && i + p[0] < v.Length)
					v[i + p[0]] = Math.Max(v[i + p[0]], v[i] + p[1]);
		Console.WriteLine(v.Max());
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
