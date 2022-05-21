using System;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve() ? "Yes" : "No");
	static bool Solve()
	{
		var (n, x) = Read2();
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		const int max = 10000;

		var dp = new bool[max + 1];
		dp[0] = true;
		var t = new bool[max + 1];

		foreach (var (a, b) in ps)
		{
			for (int j = 0; j <= max; j++)
			{
				if (!dp[j]) continue;

				t[j + a] = true;
				t[j + b] = true;
			}

			(dp, t) = (t, dp);
			Array.Clear(t, 0, t.Length);
		}

		return dp[x];
	}
}
