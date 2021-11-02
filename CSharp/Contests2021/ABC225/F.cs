using System;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, k) = Read2();
		var ss = Array.ConvertAll(new bool[n], _ => Console.ReadLine());

		var sc = StringComparer.Ordinal;
		Array.Sort(ss, (s, t) => sc.Compare(s + t, t + s));

		var max = "~";
		var dp = Array.ConvertAll(new bool[k + 1], _ => max);
		dp[0] = "";

		for (int i = n - 1; i >= 0; i--)
		{
			for (int j = k; j > 0; j--)
			{
				if (dp[j - 1] == max) continue;
				var ns = ss[i] + dp[j - 1];
				if (sc.Compare(ns, dp[j]) < 0)
					dp[j] = ns;
			}
		}

		return dp[k];
	}
}
