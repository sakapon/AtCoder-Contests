using System;
using System.Collections.Generic;

class Q059
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int a, int b) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void WriteYesNo(bool b) => Console.WriteLine(b ? "Yes" : "No");
	static void Main()
	{
		var (n, m, qc) = Read3();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var map = ToMap(n + 1, es, true);
		var u = new ulong[n + 1];
		var bs = new int[64];

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		while (qc > 0)
		{
			var c = Math.Min(qc, 64);
			qc -= c;

			Array.Clear(u, 0, u.Length);
			for (int i = 0; i < c; i++)
			{
				var (a, b) = Read2();
				u[a] |= 1UL << i;
				bs[i] = b;
			}

			for (int v = 1; v < n; v++)
			{
				foreach (var nv in map[v])
				{
					u[nv] |= u[v];
				}
			}

			for (int i = 0; i < c; i++)
			{
				var b = bs[i];
				WriteYesNo((u[b] & (1UL << i)) != 0);
			}
		}
		Console.Out.Flush();
	}

	static List<int>[] ToMap(int n, int[][] es, bool directed)
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
