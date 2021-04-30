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

		var xMax = x.Max();
		es = Array.FindAll(es, e => e[2] <= xMax);
		var map = EdgesToMap2(n + 1, es, false);

		var cumMax = new int[qc];
		cumMax[^1] = x[^1];
		for (int i = qc - 2; i >= 0; i--)
		{
			cumMax[i] = Math.Max(cumMax[i + 1], x[i]);
		}

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });

		var r = 1;
		var d = Array.ConvertAll(new bool[n + 1], _ => int.MaxValue);
		var q = PQ<int[]>.Create(e => e[2]);

		d[1] = -1;
		foreach (var e in map[1])
		{
			q.Push(e);
		}

		for (int i = 0; i < qc; i++)
		{
			var l = new List<int[]>();

			while (q.Count > 0 && q.First[2] <= x[i])
			{
				var e = q.Pop();
				var nv = e[1];
				var c = e[2];

				if (d[nv] <= i) continue;
				//if (x[i] < c)
				//{
				//	if (i + 1 < qc && cumMax[i + 1] >= c) q.Push(e);
				//	continue;
				//}
				r++;
				d[nv] = i;
				foreach (var ne in map[nv])
				{
					l.Add(ne);
				}
			}

			q.PushRange(l.ToArray());
			Console.WriteLine(r);
		}

		Console.Out.Flush();
	}

	// weighted
	static List<int[]>[] EdgesToMap2(int n, int[][] es, bool directed)
	{
		var map = Array.ConvertAll(new bool[n], _ => new List<int[]>());
		foreach (var e in es)
		{
			map[e[0]].Add(new[] { e[0], e[1], e[2] });
			if (!directed) map[e[1]].Add(new[] { e[1], e[0], e[2] });
		}
		return map;
	}
}
