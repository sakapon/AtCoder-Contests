using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

class L
{
	struct R
	{
		public int i, j;
		public double Cost;
	}

	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var h = read();
		n = h[0]; m = h[1];
		ps1 = new int[n].Select(_ => read()).ToArray();
		ps2 = new int[m].Select(_ => read()).ToArray();

		Console.WriteLine(Enumerable.Range(0, (int)Math.Pow(2, m)).Min(i => Build(i)));
	}

	static int n, m;
	static int[][] ps1, ps2;

	static double Build(int f)
	{
		var fs = new BitArray(new[] { f });
		var ps = ps1.Concat(ps2.Where((x, i) => fs[i])).ToArray();

		var rs = new List<R>();
		for (int i = 0; i < ps.Length; i++)
			for (int j = i + 1; j < ps.Length; j++)
				rs.Add(new R { i = i, j = j, Cost = Norm(ps[i], ps[j]) });
		rs.Sort((x, y) => Math.Sign(x.Cost - y.Cost));

		var uf = new UF(ps.Length);
		var c = 0.0;
		foreach (var r in rs)
		{
			if (uf.AreUnited(r.i, r.j)) continue;
			uf.Unite(r.i, r.j);
			c += r.Cost;
		}
		return c;
	}

	static double Norm(int[] p, int[] q)
	{
		int dx = p[0] - q[0], dy = p[1] - q[1];
		return (p[2] == q[2] ? 1 : 10) * Math.Sqrt(dx * dx + dy * dy);
	}
}

class UF
{
	int[] p;
	public UF(int n) { p = Enumerable.Range(0, n).ToArray(); }

	public void Unite(int a, int b) { if (!AreUnited(a, b)) p[p[b]] = p[a]; }
	public bool AreUnited(int a, int b) => GetRoot(a) == GetRoot(b);
	int GetRoot(int a) => p[a] == a ? a : p[a] = GetRoot(p[a]);
}
