using System;
using System.Linq;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = Array.ConvertAll(new int[n], _ => Array.ConvertAll(Console.ReadLine().Split(), double.Parse));

		// i in [1, n-2]
		var dp = NewArray2(n, n, double.MaxValue);
		dp[1][0] = Distance(ps[0], ps[1]) + Distance(ps[n - 2], ps[n - 1]);

		for (int i = 1; i < n - 2; i++)
			for (int j = 0; j < i; j++)
			{
				dp[i + 1][i] = Math.Min(dp[i + 1][i], dp[i][j] + Distance(ps[i + 1], ps[j]));
				dp[i + 1][j] = Math.Min(dp[i + 1][j], dp[i][j] + Distance(ps[i + 1], ps[i]));
			}
		Console.WriteLine(n == 2 ? dp[1][0] : Enumerable.Range(0, n - 2).Min(i => dp[n - 2][i] + Distance(ps[i], ps[n - 1])));
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default(T)) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
	static double Distance(double[] p, double[] q) => Math.Sqrt((p[0] - q[0]) * (p[0] - q[0]) + (p[1] - q[1]) * (p[1] - q[1]));
}
