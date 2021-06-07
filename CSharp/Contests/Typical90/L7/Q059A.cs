using System;
using System.Collections.Generic;

class Q059A
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static (int, int, int) Read3() { var a = Read(); return (a[0], a[1], a[2]); }
	static void WriteYesNo(bool b) => Console.WriteLine(b ? "Yes" : "No");
	static void Main()
	{
		var (n, m, qc) = Read3();
		var es = Array.ConvertAll(new bool[m], _ => Read());

		var map = ToMap(n + 1, es, true);

		Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false });
		while (qc-- > 0)
		{
			var (a, b) = Read2();

			var u = new bool[n + 1];
			u[a] = true;

			for (int v = 1; v < n; v++)
			{
				if (!u[v]) continue;
				foreach (var nv in map[v])
				{
					u[nv] = true;
				}
			}
			WriteYesNo(u[b]);
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
