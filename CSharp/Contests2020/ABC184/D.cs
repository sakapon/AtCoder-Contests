using System;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main()
	{
		var (a, b, c) = Read3();

		var dp = NewArray3<double>(101, 101, 101);

		for (int i = 99; i >= 0; i--)
			for (int j = 99; j >= 0; j--)
				for (int k = 99; k >= 0; k--)
				{
					var all = i + j + k;
					if (all == 0) continue;
					dp[i][j][k] += (dp[i + 1][j][k] + 1) * i / all;
					dp[i][j][k] += (dp[i][j + 1][k] + 1) * j / all;
					dp[i][j][k] += (dp[i][j][k + 1] + 1) * k / all;
				}
		Console.WriteLine(dp[a][b][c]);
	}

	static T[][][] NewArray3<T>(int n1, int n2, int n3, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => Array.ConvertAll(new bool[n3], ___ => v)));
}
