using System;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		const int all = 1 << 11;

		var dp = new long[all];
		var dt = new long[all];
		dp[1] = 1;

		for (int i = 0; i < n; i++)
		{
			for (int x = 0; x < all; x++)
			{
				if (dp[x] == 0) continue;

				for (int v = Math.Min(10, a[i]); v > 0; v--)
				{
					var nx = x;
					for (int s = 0; s <= 10; s++)
					{
						if ((x & (1 << s)) == 0) continue;
						var ns = s + v;
						if (ns > 10) continue;
						nx |= 1 << ns;
					}
					dt[nx] += dp[x];
					dt[nx] %= M;
				}

				if (a[i] > 10)
				{
					dt[x] += dp[x] * (a[i] - 10);
					dt[x] %= M;
				}
			}

			(dp, dt) = (dt, dp);
			Array.Clear(dt, 0, dt.Length);
		}

		var r = dp[(1 << 10)..].Sum() % M;
		foreach (int v in a)
		{
			r *= MInv(v);
			r %= M;
		}
		return r;
	}

	const long M = 998244353;
	static long MPow(long b, long i)
	{
		long r = 1;
		for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
		return r;
	}
	static long MInv(long x) => MPow(x, M - 2);
}
