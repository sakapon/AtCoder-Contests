using System;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var p = Read();

		var dp = new double[n + 1];
		Array.Fill(dp, -1);
		dp[0] = 0;

		for (int i = 0; i < n; i++)
		{
			for (int j = n - 1; j >= 0; j--)
			{
				if (dp[j] == -1) continue;
				Chmax(ref dp[j + 1], dp[j] * 0.9 + p[i]);
			}
		}

		var r = double.MinValue;
		var s = 0D;
		var p9 = 1D;
		for (int k = 1; k <= n; k++)
		{
			s += p9;
			p9 *= 0.9;
			Chmax(ref r, dp[k] / s - 1200 / Math.Sqrt(k));
		}
		return r;
	}

	public static double Chmax(ref double x, double v) => x < v ? x = v : x;
}
