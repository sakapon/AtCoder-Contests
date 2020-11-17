using System;
using System.Collections.Generic;
using System.Linq;

class B
{
	const int max = 1 << 30;
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main()
	{
		var h = Read();
		int n = h[0], m = h[1];
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var map = EdgesToMap(n, es, false);

		var sum = es.Sum(e => e[2]);
		var odds = Enumerable.Range(0, n).Where(v => map[v].Count % 2 == 1).ToArray();
		if (odds.Length == 0) { Console.WriteLine(sum); return; }

		var d = WarshallFloyd(n - 1, es);
		var oddsF = odds.Aggregate(0, (p, v) => p | (1 << v));

		var dp = NewArray3(1 << n, n, 2, max);
		dp[0][odds[0]][0] = 0;

		for (int x = 0; x < 1 << n; x++)
		{
			for (int v = 0; v < n; v++)
			{
				if ((oddsF & (1 << v)) == 0) continue;
				foreach (var nv in odds)
				{
					if ((x & (1 << nv)) != 0) continue;

					if (dp[x][v][0] < max)
						dp[x | (1 << nv)][nv][1] = Math.Min(dp[x | (1 << nv)][nv][1], dp[x][v][0]);
					else
						dp[x | (1 << nv)][nv][0] = Math.Min(dp[x | (1 << nv)][nv][0], dp[x][v][1] + (int)d[v][nv]);
				}
			}
		}
		Console.WriteLine(sum + dp[oddsF][odds[0]][0]);
	}

	static T[][][] NewArray3<T>(int n1, int n2, int n3, T v = default(T)) => Array.ConvertAll(new bool[n1], _ => Array.ConvertAll(new bool[n2], __ => Array.ConvertAll(new bool[n3], ___ => v)));

	static List<int[]>[] EdgesToMap(int n, int[][] es, bool directed)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
		foreach (var e in es)
		{
			map[e[0]].Add(new[] { e[0], e[1], e[2] });
			if (!directed) map[e[1]].Add(new[] { e[1], e[0], e[2] });
		}
		return map;
	}

	// es: { from, to, weight }
	// 負閉路が存在する場合、null を返します。
	// 到達不可能のペアの値は MaxValue です。
	static long[][] WarshallFloyd(int n, int[][] es)
	{
		var d = new long[n + 1][];
		for (int i = 0; i <= n; i++)
		{
			d[i] = Array.ConvertAll(d, _ => long.MaxValue);
			d[i][i] = 0;
		}

		foreach (var e in es)
		{
			// 多重辺の場合
			d[e[0]][e[1]] = Math.Min(d[e[0]][e[1]], e[2]);
			// 有向グラフの場合、ここを削除します。
			d[e[1]][e[0]] = Math.Min(d[e[1]][e[0]], e[2]);
		}

		for (int k = 0; k <= n; k++)
			for (int i = 0; i <= n; i++)
				for (int j = 0; j <= n; j++)
					if (d[i][k] < long.MaxValue && d[k][j] < long.MaxValue)
						d[i][j] = Math.Min(d[i][j], d[i][k] + d[k][j]);

		if (Enumerable.Range(0, n + 1).Any(i => d[i][i] < 0)) return null;
		return d;
	}
}
