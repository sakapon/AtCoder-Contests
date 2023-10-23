using System;
using System.Collections.Generic;
using System.Linq;

class D
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

		var c10 = new int[11];
		c10[0] = 1;
		for (int k = 0; k < 10; k++)
			for (int i = 10; i > 0; i--)
				c10[i] += c10[i - 1];

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
			var dp = new HashMap<long, long>();
			var dt = new HashMap<long, long>();
			dp[0] = 1;

			foreach (var (a, b) in ps)
			{
				foreach (var (s, v) in dp)
				{
					for (int k = 0; k <= 10; k++)
					{
						dt[s + k * a] += v * c10[k];
						dt[s + k * a] %= M;
					}
				}
				(dp, dt) = (dt, dp);
				dt.Clear();

				foreach (var (s, v) in dp)
				{
					for (int k = 0; k <= 1; k++)
					{
						dt[s + k * b] += v;
						dt[s + k * b] %= M;
					}
				}
				(dp, dt) = (dt, dp);
				dt.Clear();
			}
			return dp;
		}
	}

	const long M = 998244353;
}

class HashMap<TK, TV> : Dictionary<TK, TV>
{
	TV _v0;
	public HashMap(TV v0 = default(TV)) { _v0 = v0; }

	public new TV this[TK key]
	{
		get { return ContainsKey(key) ? base[key] : _v0; }
		set { base[key] = value; }
	}
}
