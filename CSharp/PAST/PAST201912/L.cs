using System;
using System.Collections.Generic;
using System.Linq;

class L
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var ps1 = Array.ConvertAll(new bool[n], _ => Read());
		var ps2 = Array.ConvertAll(new bool[m], _ => Read());

		var r = double.MaxValue;
		AllCombination(ps2, ps3 =>
		{
			var ps = ps1.Concat(ps3).ToArray();
			var nm = ps.Length;

			var es = new List<Edge>();
			for (int i = 0; i < nm; i++)
				for (int j = i + 1; j < nm; j++)
					es.Add(new Edge { i = i, j = j, cost = Norm(ps[i], ps[j]) });

			var mes = Kruskal(nm, es.ToArray());
			r = Math.Min(r, mes.Sum(e => e.cost));

			return false;
		});

		return r;
	}

	struct Edge
	{
		public int i, j;
		public double cost;
	}

	static void AllCombination<T>(T[] values, Func<T[], bool> action)
	{
		var n = values.Length;
		if (n > 30) throw new InvalidOperationException();
		var pn = 1 << n;

		var rn = new int[n];
		for (int i = 0; i < n; ++i) rn[i] = i;

		for (int x = 0; x < pn; ++x)
		{
			var indexes = Array.FindAll(rn, i => (x & (1 << i)) != 0);
			if (action(Array.ConvertAll(indexes, i => values[i]))) break;
		}
	}

	static double Norm(int[] p, int[] q)
	{
		int dx = p[0] - q[0], dy = p[1] - q[1];
		return (p[2] == q[2] ? 1 : 10) * Math.Sqrt(dx * dx + dy * dy);
	}

	static Edge[] Kruskal(int n, Edge[] ues)
	{
		var uf = new UF(n);
		var mes = new List<Edge>();

		foreach (var e in ues.OrderBy(e => e.cost))
		{
			if (uf.AreUnited(e.i, e.j)) continue;
			uf.Unite(e.i, e.j);
			mes.Add(e);
		}
		return mes.ToArray();
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
