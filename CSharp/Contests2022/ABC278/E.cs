using System;
using System.Collections.Generic;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var z = Read();
		var H = z[0];
		var W = z[1];
		var n = z[2];
		var h = z[3];
		var w = z[4];
		var a = Array.ConvertAll(new bool[H], _ => Read());

		// 値が v である座標
		var map = Array.ConvertAll(new bool[n + 1], _ => new int[H, W]);
		var vCounts = new int[n + 1];

		for (int i = 0; i < H; i++)
		{
			for (int j = 0; j < W; j++)
			{
				map[a[i][j]][i, j] += 1;
				vCounts[a[i][j]]++;
			}
		}

		var r = NewArray2(H - h + 1, W - w + 1, n);

		for (int v = 1; v <= n; v++)
		{
			var rsq = new StaticRSQ2(map[v]);

			for (int k = 0; k < H - h + 1; k++)
			{
				for (int l = 0; l < W - w + 1; l++)
				{
					var c = rsq.GetSum(k, l, k + h, l + w);
					if (c == vCounts[v])
						r[k][l]--;
				}
			}
		}

		foreach (var l in r)
		{
			Console.WriteLine(string.Join(" ", l));
		}
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => v));
}

public class StaticRSQ2
{
	long[,] s;
	public StaticRSQ2(int[,] a)
	{
		var n1 = a.GetLength(0);
		var n2 = a.GetLength(1);
		s = new long[n1 + 1, n2 + 1];
		for (int i = 0; i < n1; ++i)
		{
			for (int j = 0; j < n2; ++j) s[i + 1, j + 1] = s[i + 1, j] + a[i, j];
			for (int j = 1; j <= n2; ++j) s[i + 1, j] += s[i, j];
		}
	}

	public long GetSum(int l1, int l2, int r1, int r2) => s[r1, r2] - s[l1, r2] - s[r1, l2] + s[l1, l2];
}
