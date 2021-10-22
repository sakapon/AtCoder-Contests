using System;
using System.Collections.Generic;

class V
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[n - 1], _ => Read());

		var map = ToMap(n + 1, es, false);

		// v が黒の場合
		var dpu = new long[n + 1];
		var dpl = new long[n + 1];
		dpu[1] = 1;

		var r = new long[n + 1];

		Dfs1(1, -1);
		Dfs2(1, -1);

		return string.Join("\n", r[1..]);

		void Dfs1(int v, int pv)
		{
			dpl[v] = 1;

			foreach (var nv in map[v])
			{
				if (nv == pv) continue;

				Dfs1(nv, v);

				dpl[v] *= dpl[nv] + 1;
				dpl[v] %= m;
			}
		}

		void Dfs2(int v, int pv)
		{
			r[v] = dpu[v] * dpl[v] % m;

			var nexts = map[v];
			var cp0 = new long[nexts.Length + 1];
			var cp1 = new long[nexts.Length + 1];
			cp0[0] = 1;
			cp1[^1] = 1;

			for (int i = 0; i < nexts.Length; i++)
			{
				var nv = nexts[i];
				if (nv == pv) cp0[i + 1] = cp0[i];
				else cp0[i + 1] = cp0[i] * (dpl[nv] + 1) % m;
			}
			for (int i = nexts.Length - 1; i >= 0; i--)
			{
				var nv = nexts[i];
				if (nv == pv) cp1[i] = cp1[i + 1];
				else cp1[i] = cp1[i + 1] * (dpl[nv] + 1) % m;
			}

			for (int i = 0; i < nexts.Length; i++)
			{
				var nv = nexts[i];
				if (nv == pv) continue;

				dpu[nv] = dpu[v] * cp0[i] % m * cp1[i + 1] + 1;
				dpu[nv] %= m;

				Dfs2(nv, v);
			}
		}
	}

	public static int[][] ToMap(int n, int[][] es, bool directed) => Array.ConvertAll(ToMapList(n, es, directed), l => l.ToArray());
	public static List<int>[] ToMapList(int n, int[][] es, bool directed)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int>());
		foreach (var e in es)
		{
			map[e[0]].Add(e[1]);
			if (!directed) map[e[1]].Add(e[0]);
		}
		return map;
	}
}
