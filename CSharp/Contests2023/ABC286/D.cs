using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int a, int b) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (n, x) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Read2())
			.Select(p => Enumerable.Range(1, p.b).Select(j => p.a * j).ToArray())
			.ToArray();

		var dp = new bool[x + 1];
		dp[0] = true;

		foreach (var p in ps)
		{
			for (int i = x - 1; i >= 0; i--)
			{
				if (!dp[i]) continue;

				foreach (var v in p)
				{
					var ni = i + v;
					if (ni > x) break;
					dp[ni] = true;
				}
			}
		}
		return dp[x];
	}
}
