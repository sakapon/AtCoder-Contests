using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Values;

class E4
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k, p) = Read3();
		var ps = Array.ConvertAll(new bool[n], _ => ReadL());

		var sa = new ZobristArray<int>(k);
		var ea = new ZobristArray<int>(k);
		ea.Fill(p);

		var dp = new Dictionary<ZobristArray<int>, long>();
		dp[sa] = 0;

		foreach (var ca in ps)
		{
			var c = ca[0];
			var a = new ZobristArray<int>(Array.ConvertAll(ca[1..], v => (int)v));

			foreach (var (da, dc) in dp.ToArray())
			{
				var na = new ZobristArray<int>(Add(da.a, a.a));
				var nc = dc + c;

				for (int i = 0; i < k; i++)
					if (na[i] > p) na[i] = p;

				if (!dp.TryGetValue(na, out var nc0) || nc0 > nc)
					dp[na] = nc;
			}
		}

		return dp.GetValueOrDefault(ea, -1);
	}

	static int[] Add(int[] v1, int[] v2)
	{
		var r = new int[v1.Length];
		for (int i = 0; i < v1.Length; ++i)
			r[i] = v1[i] + v2[i];
		return r;
	}
}
