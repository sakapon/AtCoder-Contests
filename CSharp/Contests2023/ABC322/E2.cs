using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib8.Values;

class E2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k, p) = Read3();
		var ps = Array.ConvertAll(new bool[n], _ => ReadL());

		var dp = new Dictionary<IntVector, long>();
		dp[new IntVector(k)] = 0;

		foreach (var ca in ps)
		{
			var c = ca[0];
			IntVector a = ca[1..];

			var dt = new Dictionary<IntVector, long>(dp);

			foreach (var (da, dc) in dp)
			{
				var na = da + a;
				var nc = dc + c;

				na = Array.ConvertAll(na.Raw, x => Math.Min(x, p));

				if (dt.TryGetValue(na, out var nc0))
					dt[na] = Math.Min(nc0, nc);
				else
					dt[na] = nc;
			}

			dp = dt;
		}

		var pa = new long[k];
		Array.Fill(pa, p);
		return dp.TryGetValue(pa, out var pc) ? pc : -1;
	}
}
