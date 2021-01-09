using System;
using System.Linq;

class A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var z = Read();
		int h = z[0], w = z[1];
		var dp = Array.ConvertAll(new int[h], _ => Array.ConvertAll(Read(), x => 1 - x));

		for (int i = 1; i < h; i++)
			for (int j = 1; j < w; j++)
				if (dp[i][j] > 0)
					dp[i][j] = Math.Min(Math.Min(dp[i - 1][j], dp[i][j - 1]), dp[i - 1][j - 1]) + 1;

		var M = dp.Max(r => r.Max());
		Console.WriteLine(M * M);
	}
}
