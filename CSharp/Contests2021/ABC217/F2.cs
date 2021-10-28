using System;

class F2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read2());

		var u = new bool[2 * n, 2 * n];
		foreach (var (a, b) in es)
		{
			u[a - 1, b - 1] = true;
			u[b - 1, a - 1] = true;
		}

		var mc = new MCombination(n);

		var dp = NewArray2(2 * n + 1, 2 * n + 1, -1L);
		return Rec(0, 2 * n);

		long Rec(int l, int r)
		{
			if (dp[l][r] != -1) return dp[l][r];
			if (l == r) return dp[l][r] = 1;

			var v = 0L;

			for (int c = l + 2; c <= r; c += 2)
			{
				if (u[l, c - 1])
				{
					v += Rec(l + 1, c - 1) * Rec(c, r) % M * mc.MNcr((r - l) / 2, (r - c) / 2);
					v %= M;
				}
			}
			return dp[l][r] = v;
		}
	}

	const long M = 998244353;
	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
