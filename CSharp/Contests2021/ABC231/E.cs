using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long, long) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, x) = Read2L();
		var a = ReadL();

		const long max = 1L << 60;
		var dp = Array.ConvertAll(new bool[n + 1], _ => new Map<long, long>(max));
		dp[0][x] = 0;

		for (int i = 0; i < n; i++)
		{
			foreach (var (y, v) in dp[i])
			{
				if (i < n - 1)
				{
					var rem = y % a[i + 1];
					dp[i + 1][y - rem] = Math.Min(dp[i + 1][y - rem], v + rem / a[i]);
					dp[i + 1][y - rem + a[i + 1]] = Math.Min(dp[i + 1][y - rem + a[i + 1]], v + (a[i + 1] - rem) / a[i]);
				}
				else
				{
					dp[n][0] = Math.Min(dp[n][0], v + y / a[i]);
				}
			}
		}

		return dp[n][0];
	}
}

class Map<TK, TV> : Dictionary<TK, TV>
{
	TV _v0;
	public Map(TV v0 = default(TV)) { _v0 = v0; }

	public new TV this[TK key]
	{
		get { return ContainsKey(key) ? base[key] : _v0; }
		set { base[key] = value; }
	}
}
