using System;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k, p) = Read3();
		var ps = Array.ConvertAll(new bool[n], _ => Read());

		var dp = new long[(int)Math.Pow(p + 1, k)];
		Array.Fill(dp, 1L << 60);
		dp[0] = 0;

		foreach (var ca in ps)
		{
			var c = ca[0];

			for (int x = dp.Length - 1; x >= 0; x--)
			{
				if (dp[x] == 1L << 60) continue;

				var ak = new int[k];
				var nx = x;
				for (int i = 0; i < k; i++)
				{
					ak[i] = nx % (p + 1);
					ak[i] += ca[i + 1];
					if (ak[i] > p) ak[i] = p;
					nx /= p + 1;
				}

				nx = 0;
				for (int i = k - 1; i >= 0; i--)
				{
					nx *= p + 1;
					nx += ak[i];
				}

				Chmin(ref dp[nx], dp[x] + c);
			}
		}

		var r = dp[^1];
		if (r == 1L << 60) return -1;
		return r;
	}

	public static long Chmin(ref long x, long v) => x > v ? x = v : x;
}
