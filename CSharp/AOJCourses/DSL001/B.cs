using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var r = new List<int>();
		var h = Read();
		var n = h[0];

		var uf = new UFW(n);

		for (int i = 0; i < h[1]; i++)
		{
			var q = Read();
			if (q[0] == 0)
				uf.Unite(q[1], q[2], q[3]);
			else
				r.Add(uf.Diff(q[1], q[2]));
		}
		Console.WriteLine(string.Join("\n", r.Select(d => d == int.MaxValue ? "?" : $"{d}")));
	}
}

class UFW
{
	int[] p, d;
	public UFW(int n)
	{
		p = Enumerable.Range(0, n).ToArray();
		d = new int[n];
	}

	public void Unite(int a, int b, int delta)
	{
		if (AreUnited(a, b)) return;
		d[p[b]] = d[a] - d[b] + delta;
		p[p[b]] = p[a];
	}

	public bool AreUnited(int a, int b) => GetRoot(a) == GetRoot(b);
	public int GetRoot(int a)
	{
		if (p[a] == a) return a;
		var oldP = p[a];
		p[a] = GetRoot(p[a]);
		d[a] += d[oldP] - d[p[a]];
		return p[a];
	}

	public int Diff(int a, int b) => AreUnited(a, b) ? d[b] - d[a] : int.MaxValue;
}
