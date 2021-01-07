using System;

class B3
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (h, w) = Read2();
		var s = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		var dp = NewArray2(h, w, 1 << 30);
		dp[h - 1][w - 1] = s[h - 1][w - 1] == 'E' ? 0 : 1;

		for (int i = h - 1; i >= 0; i--)
			for (int j = w - 1; j >= 0; j--)
			{
				if (i > 0) dp[i - 1][j] = Math.Min(dp[i - 1][j], dp[i][j] + (s[i - 1][j] == 'S' ? 0 : 1));
				if (j > 0) dp[i][j - 1] = Math.Min(dp[i][j - 1], dp[i][j] + (s[i][j - 1] == 'E' ? 0 : 1));
			}
		Console.WriteLine(dp[0][0]);
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
