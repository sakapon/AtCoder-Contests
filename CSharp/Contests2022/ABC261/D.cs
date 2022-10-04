using System;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int c, int y) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var x = Read();
		var bonus = Array.ConvertAll(new bool[m], _ => Read2()).ToDictionary(p => p.c, p => p.y);

		var dp = new long[n + 1];
		Array.Fill(dp, -1);
		dp[0] = 0;
		var t = new long[n + 1];

		for (int i = 0; i < n; i++)
		{
			Array.Fill(t, -1);

			for (int j = 0; j < n; j++)
			{
				if (dp[j] == -1) continue;

				var nj = j + 1;
				var nv = dp[j] + x[i];
				if (bonus.ContainsKey(nj)) nv += bonus[nj];

				t[nj] = Math.Max(t[nj], nv);
				t[0] = Math.Max(t[0], dp[j]);
			}

			(dp, t) = (t, dp);
		}
		return dp.Max();
	}
}
