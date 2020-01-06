using System;
using System.Linq;

class H
{
	static void Main()
	{
		var a = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
		int h = a[0], w = a[1];
		var dp = new int[h].Select(_ => Console.ReadLine().Select(x => x == '.' ? 0 : -1).ToArray()).ToArray();

		dp[0][0] = 1;
		for (int i = 1; i < h; i++)
			if (dp[i][0] == 0) dp[i][0] = 1;
			else break;
		for (int j = 1; j < w; j++)
			if (dp[0][j] == 0) dp[0][j] = 1;
			else break;

		for (int i = 1; i < h; i++)
			for (int j = 1; j < w; j++)
				if (dp[i][j] != -1)
					dp[i][j] = ((dp[i][j - 1] != -1 ? dp[i][j - 1] : 0) + (dp[i - 1][j] != -1 ? dp[i - 1][j] : 0)) % 1000000007;
		Console.WriteLine(dp[h - 1][w - 1]);
	}
}
