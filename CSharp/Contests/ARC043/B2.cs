using System;
using System.Linq;

class B2
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var d = Array.ConvertAll(new bool[n], _ => int.Parse(Console.ReadLine()));

		const int dMax = 100000;
		var c = new long[dMax + 1];
		foreach (var v in d)
		{
			c[v]++;
		}

		var dp = new long[dMax + 1];
		var t = new long[dMax + 1];
		Array.Fill(dp, 1);

		for (int k = 1; k < 4; k++)
		{
			Array.Clear(t, 0, t.Length);

			// imos
			for (int i = 0; i <= dMax; i++)
			{
				if (c[i] == 0) continue;
				if (i << 1 > dMax) continue;
				t[i << 1] += dp[i] * c[i] % M;
			}
			for (int i = 0; i < dMax; i++)
			{
				t[i + 1] += t[i];
				t[i + 1] %= M;
			}

			(dp, t) = (t, dp);
		}
		return Enumerable.Range(0, dMax + 1).Sum(i => dp[i] * c[i] % M) % M;
	}

	const long M = 1000000007;
}
