using System;
using System.Collections.Generic;
using System.Linq;
using CoderLib6.Trees;

class G
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static long[] ReadL() => Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
	static void Main()
	{
		var (n, m, qc) = Read3();
		var es = Array.ConvertAll(new bool[m], _ => Read());
		var x = Read();

		var map = ToMap(n + 1, es, false);
		var sv = 1;

		var r = 1;
		var u = new bool[n + 1];
		var q = PQ<int[]>.Create(e => e[2]);
		var l = new List<int>();

		u[sv] = true;
		q.PushRange(map[sv]);

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		for (int i = 0; i < qc; i++)
		{
			while (q.Count > 0 && q.First[2] <= x[i])
			{
				var e = q.Pop();
				var to = e[1];
				if (u[to]) continue;

				r++;
				u[to] = true;
				l.Add(to);
			}

			foreach (var v in l)
			{
				foreach (var e in map[v])
				{
					var to = e[1];
					if (u[to]) continue;
					q.Push(e);
				}
			}
			l.Clear();

			Console.WriteLine(r);
		}
		Console.Out.Flush();
	}

	public static int[][][] ToMap(int n, int[][] es, bool directed) => Array.ConvertAll(ToMapList(n, es, directed), l => l.ToArray());
	public static List<int[]>[] ToMapList(int n, int[][] es, bool directed)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
		foreach (var e in es)
		{
			map[e[0]].Add(e);
			if (!directed) map[e[1]].Add(new[] { e[1], e[0], e[2] });
		}
		return map;
	}
}
