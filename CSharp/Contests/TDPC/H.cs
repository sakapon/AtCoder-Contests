using System;
using System.Collections.Generic;
using System.Linq;

class H
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int w, int v, int c) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void Main()
	{
		var (n, w, c) = Read3();
		var psByC = Array.ConvertAll(new bool[n], _ => Read3()).GroupBy(p => p.c).ToArray();

		var vsByC = NewArray2(psByC.Length, w + 1, -1);
		var vsByC2 = Array.ConvertAll(new bool[psByC.Length], _ => new List<(int w, int v)>());
		for (int i = 0; i < psByC.Length; i++)
		{
			vsByC[i][0] = 0;
			foreach (var (cw, cv, _) in psByC[i])
			{
				for (int j = w - 1; j >= 0; j--)
				{
					if (vsByC[i][j] == -1) continue;
					var nw = j + cw;
					if (nw > w) continue;
					vsByC[i][nw] = Math.Max(vsByC[i][nw], vsByC[i][j] + cv);
				}
			}

			for (int j = 0; j <= w; j++)
			{
				if (vsByC[i][j] == -1) continue;
				vsByC2[i].Add((j, vsByC[i][j]));
			}
		}

		var dp = NewArray2(c + 1, w + 1, -1);
		dp[0][0] = 0;

		for (int ci = 0; ci < psByC.Length; ci++)
		{
			for (int i = Math.Min(c - 1, ci); i >= 0; i--)
			{
				foreach (var (cw, cv) in vsByC2[ci])
				{
					for (int j = w - 1; j >= 0; j--)
					{
						if (dp[i][j] == -1) continue;
						var nw = j + cw;
						if (nw > w) continue;
						dp[i + 1][nw] = Math.Max(dp[i + 1][nw], dp[i][j] + cv);
					}
				}
			}
		}
		Console.WriteLine(dp.Max(cs => cs.Max()));
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}
