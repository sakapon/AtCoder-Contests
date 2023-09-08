using System;
using V2 = System.ValueTuple<long, long>;

class F
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static V2 Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new bool[n], _ => Read2());

		const int K = 30;
		const double max = 1 << 30;

		// 点 i を通り、j 個を通らなかった場合の距離の最小値
		var dp = NewArray2(n, K, max);
		dp[0][0] = 0;

		for (int i = 1; i < n; i++)
		{
			for (int j = 0; j < K && j < i; j++)
			{
				for (int d = 0; d <= j && d <= i - 1; d++)
				{
					Chmin(ref dp[i][j], dp[i - 1 - d][j - d] + Distance(ps[i], ps[i - 1 - d]));
				}
			}
		}

		var r = dp[^1][0];
		for (int j = 1; j < K; j++)
		{
			Chmin(ref r, dp[^1][j] + (1 << j - 1));
		}
		return r;
	}

	public static double Chmin(ref double x, double v) => x > v ? x = v : x;
	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));

	public static V2 Subtract(V2 v1, V2 v2) => (v1.Item1 - v2.Item1, v1.Item2 - v2.Item2);
	public static double NormL2(V2 v) => Math.Sqrt(v.Item1 * v.Item1 + v.Item2 * v.Item2);
	public static double Distance(V2 v1, V2 v2) => NormL2(Subtract(v1, v2));
}
