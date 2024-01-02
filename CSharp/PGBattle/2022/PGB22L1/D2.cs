using System;
using System.Collections.Generic;
using System.Linq;

class D2
{
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static (long a, long b) Read2L() { var a = ReadL(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2L());

		var all = ps.Sum(p => 10 * p.a + p.b);
		if (all % 2 == 1) return 0;

		if (n <= 4)
		{
			var dp = DoDP(ps);
			return dp[all / 2];
		}
		else
		{
			var dp0 = DoDP(ps[..4]);
			var dp1 = DoDP(ps[4..]);
			return dp1.Sum(p => p.Value * dp0[all / 2 - p.Key] % M) % M;
		}

		HashMap<long, long> DoDP((long a, long b)[] ps)
		{
			var cs = ps.SelectMany(p => Enumerable.Repeat(p.a, 10).Append(p.b)).ToArray();
			Array.Sort(cs);

			var dp = new HashMap<long, long>();
			dp[0] = 1;
			var l = new List<(long, long)>();

			foreach (var c in cs)
			{
				foreach (var (s, v) in dp)
				{
					l.Add((s + c, v));
				}

				foreach (var (s, v) in l)
				{
					dp[s] += v;
					dp[s] %= M;
				}

				l.Clear();
			}
			return dp;
		}
	}

	const long M = 998244353;
}
