using System;

class Q056
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, s) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Read());

		var dp = NewArray2(n + 1, s + 100000, -1);
		dp[0][0] = 0;

		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < s; j++)
			{
				if (dp[i][j] == -1) continue;

				dp[i + 1][j + ps[i][0]] = 0;
				dp[i + 1][j + ps[i][1]] = 1;
			}
		}

		if (dp[n][s] == -1) return "Impossible";

		var r = "";
		var t = s;
		for (int i = n - 1; i >= 0; i--)
		{
			var ab = dp[i + 1][t];
			r = (ab == 0 ? 'A' : 'B') + r;
			t -= ps[i][ab];
		}
		return r;
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
