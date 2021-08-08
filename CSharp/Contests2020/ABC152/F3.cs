using System;
using System.Collections.Generic;
using System.Numerics;

class F3
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int u, int v) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var es = Array.ConvertAll(new bool[n - 1], _ => Read());
		var m = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[m], _ => Read2());

		var tree = new Tree(n + 1, 1, es);
		var bllca = new BLLca(tree);

		var fs = new ulong[m];
		for (int j = 0; j < m; j++)
		{
			var (u, v) = ps[j];
			var lca = bllca.GetLca(u, v);

			for (int x = u; x != lca; x = tree.Parents[x])
			{
				fs[j] |= 1UL << x;
			}
			for (int x = v; x != lca; x = tree.Parents[x])
			{
				fs[j] |= 1UL << x;
			}
		}

		return InclusionExclusion(m, GetCount);

		long GetCount(bool[] b)
		{
			var or = 0UL;
			for (int j = 0; j < m; j++)
			{
				if (b[j])
				{
					or |= fs[j];
				}
			}
			return 1L << n - 1 - BitOperations.PopCount(or);
		}
	}

	public static long InclusionExclusion(int n, Func<bool[], long> getCount)
	{
		if (n > 30) throw new InvalidOperationException();
		var pn = 1 << n;
		var b = new bool[n];

		var r = 0L;
		for (uint x = 0; x < pn; ++x)
		{
			for (int i = 0; i < n; ++i) b[i] = (x & (1 << i)) != 0;

			var sign = BitOperations.PopCount(x) % 2 == 0 ? 1 : -1;
			r += sign * getCount(b);
		}
		return r;
	}
}
