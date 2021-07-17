using System;
using System.Collections.Generic;

class O
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		var (n, m) = Read2();
		var es = Array.ConvertAll(new bool[m], _ => Read());
		var qc = int.Parse(Console.ReadLine());
		var qs = Array.ConvertAll(new bool[qc], _ => Read2());

		var c = (int)Math.Sqrt(m);
		var map = EdgesToMap1(n + 1, es, false);
		var map_stars = Array.ConvertAll(new bool[n + 1], _ => new List<int>());

		for (int v = 1; v <= n; v++)
		{
			if (map[v].Count <= c) continue;
			foreach (var nv in map[v])
				map_stars[nv].Add(v);
		}

		var lazy = new int[n + 1];
		var notes = new int[n + 1];
		var ends = new int[n + 1];

		foreach (var (t, x) in qs)
		{
			if (t == 1)
			{
				if (map[x].Count <= c)
				{
					foreach (var y in map[x])
						notes[y]++;
				}
				else
				{
					lazy[x]++;
				}
			}
			else
			{
				var sum = 0;
				foreach (var y in map_stars[x])
					sum += lazy[y];
				Console.WriteLine(notes[x] + sum - ends[x]);

				notes[x] = 0;
				ends[x] = sum;
			}
		}
		Console.Out.Flush();
	}

	public static List<int>[] EdgesToMap1(int n, int[][] es, bool directed)
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
