using System;
using System.Linq;

class E
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var s = read();
		int h = s[0], w = s[1];
		var a = new int[h].Select(_ => read()).ToArray();
		var b = new int[h].Select(_ => read()).ToArray();
		var m = a.Zip(b, (u, v) => u.Zip(v, (x, y) => x - y).ToArray()).ToArray();

		var M = 80 * (h + w);
		var dp = new bool[h, w, M];
		dp[0, 0, Math.Abs(m[0][0])] = true;
		for (int k = 0; k < h + w - 2; k++)
		{
			for (int i = 0, j = k; i <= k; i++, j--)
			{
				if (i >= h || j >= w) continue;

				for (int v = 0; v < M; v++)
				{
					if (!dp[i, j, v]) continue;

					if (j + 1 < w)
					{
						dp[i, j + 1, Math.Abs(v + m[i][j + 1])] = true;
						dp[i, j + 1, Math.Abs(v - m[i][j + 1])] = true;
					}
					if (i + 1 < h)
					{
						dp[i + 1, j, Math.Abs(v + m[i + 1][j])] = true;
						dp[i + 1, j, Math.Abs(v - m[i + 1][j])] = true;
					}
				}
			}
		}
		Console.WriteLine(Enumerable.Range(0, M).First(i => dp[h - 1, w - 1, i]));
	}
}
