using System;
using System.Collections.Generic;
using System.Linq;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();

		// 0 <= v < m
		var dp = Array.ConvertAll(new bool[n], _ => new Dictionary<string, long>());

		for (int k = 0; k < m; k++)
		{
			dp[0][$"{k}"] = 1;
		}

		for (int i = 1; i < n; i++)
		{
			var d = dp[i - 1];
			var nd = dp[i];

			foreach (var (j, v) in d)
			{
				for (int k = 0; k < m; k++)
				{
					var nj = NextLis(j, $"{k}"[0]);
					if (nj.Length > 3) continue;

					if (!nd.ContainsKey(nj)) nd[nj] = 0;
					nd[nj] += v;
					nd[nj] %= M;
				}
			}
		}

		string NextLis(string s, char k)
		{
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] <= k)
				{
					return s.Replace(s[i], k);
				}
			}
			return s + k;
		}

		return dp[^1].Where(p => p.Key.Length == 3).Sum(p => p.Value) % M;
	}

	const long M = 998244353;
}
