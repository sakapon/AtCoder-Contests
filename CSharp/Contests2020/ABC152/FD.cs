using System;
using System.Collections.Generic;
using System.Numerics;

class FD
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

		var dp = new ulong[1 << m];
		for (int x = 0; x < 1 << m; x++)
		{
			for (int j = 0; j < m; j++)
			{
				dp[x | (1 << j)] = dp[x] | fs[j];
			}
		}
		return InclusionExclusion(m, x => 1L << n - 1 - BitOperations.PopCount(dp[x]));
	}

	public static long InclusionExclusion(int n, Func<uint, long> getCount)
	{
		if (n > 30) throw new InvalidOperationException();
		var pn = 1 << n;

		var r = 0L;
		for (uint x = 0; x < pn; ++x)
		{
			var sign = BitOperations.PopCount(x) % 2 == 0 ? 1 : -1;
			r += sign * getCount(x);
		}
		return r;
	}
}
