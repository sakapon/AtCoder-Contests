using System;

class AB
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (h, w) = Read2();
		var (si, sj) = Read2();
		si--; sj--;
		var (gi, gj) = Read2();
		gi--; gj--;
		var c = Array.ConvertAll(new bool[h], _ => Console.ReadLine());

		var dp = NewArray2(h, w, int.MaxValue);
		dp[si][sj] = 0;

		for (int k = 0; k < h * w; k++)
			for (int i = 0; i < h; i++)
				for (int j = 0; j < w; j++)
				{
					if (c[i][j] == '#') continue;
					if (dp[i][j] == int.MaxValue) continue;

					var nv = dp[i][j] + 1;
					if (i - 1 >= 0) dp[i - 1][j] = Math.Min(dp[i - 1][j], nv);
					if (i + 1 < h) dp[i + 1][j] = Math.Min(dp[i + 1][j], nv);
					if (j - 1 >= 0) dp[i][j - 1] = Math.Min(dp[i][j - 1], nv);
					if (j + 1 < w) dp[i][j + 1] = Math.Min(dp[i][j + 1], nv);
				}

		Console.WriteLine(dp[gi][gj]);
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
