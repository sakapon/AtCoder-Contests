using System;

class E
{
	const long M = 1000000007;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var z = Read();
		int h = z[0], w = z[1];
		var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		var dp = NewArray3<long>(h + 1, w + 1, 4);

		for (int i = 1; i <= h; i++)
		{
			for (int j = 1; j <= w; j++)
			{
				if (s[i - 1][j - 1] == '#') continue;

				if (i == 1 && j == 1)
				{
					dp[1][1][0] = 1;
					dp[1][1][1] = 1;
					dp[1][1][2] = 1;
					dp[1][1][3] = 1;
					continue;
				}

				dp[i][j][0] = (dp[i][j - 1][1] + dp[i - 1][j][2] + dp[i - 1][j - 1][3]) % M;
				dp[i][j][1] = (dp[i][j - 1][1] + dp[i][j][0]) % M;
				dp[i][j][2] = (dp[i - 1][j][2] + dp[i][j][0]) % M;
				dp[i][j][3] = (dp[i - 1][j - 1][3] + dp[i][j][0]) % M;
			}
		}
		Console.WriteLine(dp[h][w][0]);
	}

	static T[][][] NewArray3<T>(int n1, int n2, int n3) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => new T[n3]));
}
