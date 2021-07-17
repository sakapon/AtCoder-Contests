using System;
using System.Linq;

class C
{
	static void Main() => Console.WriteLine(string.Join("\n", new int[int.Parse(Console.ReadLine())].Select((_, i) => $"Case #{i + 1}: {Solve()}")));
	static object Solve()
	{
		var h = Console.ReadLine().Split();
		if (h[0] == h[1]) return 0;

		var s = Convert.ToInt32(h[0], 2);
		var e = Convert.ToInt32(h[1], 2);

		var dp = Array.ConvertAll(new bool[1 << 16], _ => 1 << 30);
		dp[s] = 0;
		var fmax = (1 << 16) - 1;

		var fs = new int[1 << 16];
		for (int k = 16; k > 0; k--)
		{
			var f = (1 << k) - 1;

			for (int x = 0; x <= f; x++)
			{
				fs[x] = f;
			}
		}

		for (int k = 0; k < 1024; k++)
		{
			var nk = k + 1;

			for (int x = 0; x < 1 << 16; x++)
			{
				if (dp[x] != k) continue;

				var nx = x << 1;
				if (nx <= fmax)
					dp[nx] = Math.Min(dp[nx], nk);

				nx = ~x & fs[x];
				dp[nx] = Math.Min(dp[nx], nk);
			}

			if (dp[e] == nk) return nk;
		}

		return "IMPOSSIBLE";
	}
}
