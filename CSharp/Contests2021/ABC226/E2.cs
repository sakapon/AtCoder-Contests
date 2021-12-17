using System;
using System.Collections.Generic;
using System.Linq;

class E2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int u, int v) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read2());

		var uf = new UF(n + 1);
		var vs = new List<int>();

		foreach (var e in es)
		{
			if (uf.AreUnited(e.u, e.v))
			{
				vs.Add(e.u);
			}
			else
			{
				uf.Unite(e.u, e.v);
			}
		}

		if (vs.Select(uf.GetRoot).Distinct().Count() != vs.Count) return 0;
		if (uf.GroupsCount - 1 != vs.Count) return 0;
		return MPow(2, vs.Count);
	}

	const long M = 998244353;
	static long MPow(long b, long i)
	{
		long r = 1;
		for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
		return r;
	}
}
