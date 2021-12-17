using System;
using System.Collections.Generic;
using System.Numerics;

class F2
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

		var white = new bool[n + 1];

		var tree = new Tree(n + 1, 1, es);
		return InclusionExclusion(m, GetCount);

		long GetCount(bool[] b)
		{
			Array.Clear(white, 0, white.Length);

			for (int j = 0; j < m; j++)
			{
				if (b[j])
				{
					for (var (u, v) = ps[j]; u != v;)
					{
						if (tree.Depths[u] >= tree.Depths[v])
						{
							white[u] = true;
							u = tree.Parents[u];
						}
						else if (tree.Depths[u] <= tree.Depths[v])
						{
							white[v] = true;
							v = tree.Parents[v];
						}
						else
						{
							white[u] = true;
							white[v] = true;
							u = tree.Parents[u];
							v = tree.Parents[v];
						}
					}
				}
			}

			var r = 1L << n - 1;
			for (int i = 1; i <= n; i++)
			{
				if (white[i])
				{
					r >>= 1;
				}
			}
			return r;
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
