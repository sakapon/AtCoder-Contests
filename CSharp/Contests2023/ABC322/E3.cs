using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Values;

class E3
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k, p) = Read3();
		var ps = Array.ConvertAll(new bool[n], _ => ReadL());

		var dp = new Dictionary<EquatableArray<long>, long>();
		dp[new EquatableArray<long>(k)] = 0;

		foreach (var ca in ps)
		{
			var c = ca[0];
			var a = new EquatableArray<long>(ca[1..]);

			foreach (var (da, dc) in dp.ToArray())
			{
				var na = new EquatableArray<long>(Add(da.a, a.a));
				var nc = dc + c;

				for (int i = 0; i < k; i++)
					if (na[i] > p) na[i] = p;

				if (!dp.TryGetValue(na, out var nc0) || nc0 > nc)
					dp[na] = nc;
			}
		}

		return dp.GetValueOrDefault(new EquatableArray<long>(k, p), -1);
	}

	static long[] Add(long[] v1, long[] v2)
	{
		var r = new long[v1.Length];
		for (int i = 0; i < v1.Length; ++i)
			r[i] = v1[i] + v2[i];
		return r;
	}
}
