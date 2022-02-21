using System;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var s = Console.ReadLine().Select(c => c - '0').ToArray();
		var n = s.Length;
		var m = int.Parse(Console.ReadLine());
		var c = Read();

		var pm = 1 << 10;
		var dp = NewArray2(n + 1, pm, min);

		var sx = 0;
		var sv = 0L;
		long v;

		for (int i = 0; i < n; i++)
		{
			for (int x = 0; x < pm; x++)
			{
				if ((v = dp[i][x]) != min)
				{
					for (int k = 0; k < 10; k++)
					{
						var nx = (x, k) == (0, 0) ? 0 : (x | (1 << k));
						var nv = v * 10 + k;
						if (dp[i + 1][nx] == min) dp[i + 1][nx] = 0;
						dp[i + 1][nx] += nv;
					}
				}
			}

			for (int k = 0; k < s[i]; k++)
			{
				var nx = (sx, k) == (0, 0) ? 0 : (sx | (1 << k));
				var nv = sv * 10 + k;
				if (dp[i + 1][nx] == min) dp[i + 1][nx] = 0;
				dp[i + 1][nx] += nv;
			}

			sx |= 1 << s[i];
			sv = sv * 10 + s[i];
			sv %= M;

			for (int x = 0; x < pm; x++)
			{
				dp[i + 1][x] %= M;
			}
		}

		var f = c.Select(x => 1 << x).Aggregate((x, y) => x | y);
		var r = Enumerable.Range(0, pm).Where(x => (x & f) == f).Where(x => dp[n][x] != min).Sum(x => dp[n][x]);
		if ((sx & f) == f) r += sv;
		return r % M;
	}

	const long min = -1;
	const long M = 998244353;
	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
