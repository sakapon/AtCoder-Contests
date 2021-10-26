using System;

class D2
{
	const long max = 1L << 60;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (h, w, c) = Read3();
		var a = Array.ConvertAll(new bool[h], _ => ReadL());

		var r1 = Solve2();
		Array.ForEach(a, Array.Reverse);
		var r2 = Solve2();
		return Math.Min(r1, r2);

		long Solve2()
		{
			var r = max;
			var dp = NewArray2(h, w, max);
			dp[0][0] = a[0][0];

			for (int i = 0; i < h; i++)
			{
				for (int j = 0; j < w; j++)
				{
					if (dp[i][j] != max) continue;

					var nv = max;
					if (i > 0)
					{
						nv = Math.Min(nv, dp[i - 1][j]);
					}
					if (j > 0)
					{
						nv = Math.Min(nv, dp[i][j - 1]);
					}

					r = Math.Min(r, a[i][j] + (long)c * (i + j) + nv);
					dp[i][j] = Math.Min(nv, a[i][j] - (long)c * (i + j));
				}
			}

			return r;
		}
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
