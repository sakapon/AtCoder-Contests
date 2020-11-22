using System;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		int a = h[0], b = h[1], c = h[2];

		var dp = NewArray3<double>(101, 101, 101);

		for (int i = 99; i >= 0; i--)
		{
			for (int j = 99; j >= 0; j--)
			{
				for (int k = 99; k >= 0; k--)
				{
					var all = i + j + k;
					if (all == 0) continue;
					dp[i][j][k] += (dp[i + 1][j][k] + 1) * i / all;
					dp[i][j][k] += (dp[i][j + 1][k] + 1) * j / all;
					dp[i][j][k] += (dp[i][j][k + 1] + 1) * k / all;
				}
			}
		}
		Console.WriteLine(dp[a][b][c]);
	}

	static T[] NewArray1<T>(int n, T v = default) => Array.ConvertAll(new bool[n], _ => v);
	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
	static T[][][] NewArray3<T>(int n1, int n2, int n3, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => Array.ConvertAll(new bool[n3], ___ => v)));
}
