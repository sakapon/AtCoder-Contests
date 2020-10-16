using System;
using System.Collections.Generic;

class A
{
	const int max = 1 << 30;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0];
		var es = Array.ConvertAll(new int[h[1]], _ => Read());

		var map = Array.ConvertAll(new int[n], _ => new List<int[]>());
		foreach (var e in es)
		{
			map[e[0]].Add(e);
		}

		var dp = NewArray2(1 << n, n, max);
		dp[0][0] = 0;

		for (int x = 0; x < 1 << n; x++)
		{
			for (int v = 0; v < n; v++)
			{
				if (dp[x][v] == max) continue;
				foreach (var e in map[v])
				{
					var nv = e[1];
					if ((x & (1 << nv)) != 0) continue;
					dp[x | (1 << nv)][nv] = Math.Min(dp[x | (1 << nv)][nv], dp[x][v] + e[2]);
				}
			}
		}
		var r = dp[(1 << n) - 1][0];
		Console.WriteLine(r == max ? -1 : r);
	}

	static T[][] NewArray2<T>(int n1, int n2, T v = default(T)) => NewArrayF(n1, () => NewArray1(n2, v));
	static T[] NewArray1<T>(int n, T v = default(T))
	{
		var a = new T[n];
		for (int i = 0; i < n; ++i) a[i] = v;
		return a;
	}

	static T[] NewArrayF<T>(int n, Func<T> newItem)
	{
		var a = new T[n];
		for (int i = 0; i < n; ++i) a[i] = newItem();
		return a;
	}
}
