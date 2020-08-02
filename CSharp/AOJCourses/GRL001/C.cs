using System;
using System.Linq;

class C
{
	static int[] Read() => Console.ReadLine().Split().Select(int.Parse).ToArray();
	static void Main()
	{
		var h = Read();
		var es = new int[h[1]].Select(_ => Read()).ToArray();

		var d = WarshallFloyd(h[0] - 1, es);
		if (d == null) { Console.WriteLine("NEGATIVE CYCLE"); return; }
		Console.WriteLine(string.Join("\n", d.Select(a => string.Join(" ", a.Select(x => x == long.MaxValue ? "INF" : $"{x}")))));
	}

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
			d[e[0]][e[1]] = e[2];
			// 有向グラフの場合、ここを削除します。
			//d[e[1]][e[0]] = e[2];
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
